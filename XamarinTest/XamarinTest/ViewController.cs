using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UIKit;
using Foundation;

using EZAudioKit;
using AVFoundation;

namespace XamarinTest
{
    public partial class ViewController : UIViewController, IEZAudioPlayerDelegate
    {
        // PlayFile example 

        EZAudioFile audioFile;
        EZAudioPlayer player;

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //
            // Setup the AVAudioSession. EZMicrophone will not work properly on iOS
            // if you don't do this!
            //
            AVAudioSession session = AVAudioSession.SharedInstance();
            NSError error;
            session.SetCategory(AVAudioSessionCategory.Playback);
            session.SetActive(true, out error);
            if (error != null)
            {
                //Debug.WriteLine("Error setting up audio session active: " + error.LocalizedDescription);
            }

            //
            // Customizing the audio plot's look
            //
            this.audioPlot.BackgroundColor = new UIColor(0.816f, 0.349f, 0.255f, 1);
            this.audioPlot.Color = new UIColor(1, 1, 1, 1);
            this.audioPlot.PlotType = EZPlotType.Buffer;
            this.audioPlot.ShouldFill = true;
            this.audioPlot.ShouldMirror = true;

            //Debug.WriteLine("outputs: " + EZAudioDevice.OutputDevices());

            //
            // Create the audio player
            //
            player = new EZAudioPlayer()
            {
                ShouldLoop = true,
                WeakDelegate = this
            };

            //
            // Override the output to the speaker
            //
            session.OverrideOutputAudioPort(AVAudioSessionPortOverride.Speaker, out NSError sessionError);
            if (sessionError != null)
            {
                //Debug.WriteLine("Error overriding output to the speaker: " + sessionError.LocalizedDescription);
            }

            //
            // Customize UI components
            //
            rollingSlider.Value = (float)audioPlot.RollingHistoryLength();

            SetupActions();

            string kAudioFileDefault = NSBundle.MainBundle.PathForResource("simple-drum-beat", "wav");
            OpenFileWithFilePathURL(NSUrl.FromFilename(kAudioFileDefault));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bufferRoller.ValueChanged -= ChangePlotType;
                rollingSlider.ValueChanged -= ChangeRollingHistoryLength;
                volumeSlider.ValueChanged -= ChangeVolume;
                positionSlider.ValueChanged -= SeekToFrame;
                playBtn.TouchUpInside -= Play;    
            }

            base.Dispose(disposing);
        }

        #region Actions

        void SetupActions()
        {
            bufferRoller.ValueChanged += ChangePlotType;
            rollingSlider.ValueChanged += ChangeRollingHistoryLength;
            volumeSlider.ValueChanged += ChangeVolume;
            positionSlider.ValueChanged += SeekToFrame;
            playBtn.TouchUpInside += Play;
        }

        void ChangePlotType(object sender, EventArgs e)
        {
            nint selectedSegment = bufferRoller.SelectedSegment;
            switch (selectedSegment)
            {
                case 0:
                    DrawBufferPlot(); 
                    break;
                case 1:
                    DrawRollingPlot();
                    break;
                default:
                    break;
            }
        }

        void ChangeRollingHistoryLength(object sender, EventArgs e)
        {
            float @value = ((UISlider)sender).Value;
            audioPlot.SetRollingHistoryLength((int)@value);
        }

        void ChangeVolume(object sender, EventArgs e)
        {
            float @value = ((UISlider)sender).Value;
            player.Volume = @value;
        }

        void OpenFileWithFilePathURL(NSUrl filePathUrl)
        {
            //
            // Create the EZAudioPlayer
            //
            audioFile = EZAudioFile.AudioFileWithURL(filePathUrl);

            //
            // Update the UI
            //
            filePathLabel.Text = filePathUrl.LastPathComponent;
            positionSlider.MaxValue = (float)audioFile.TotalFrames;
            volumeSlider.Value = player.Volume;

            //
            // Plot the whole waveform
            //
            audioPlot.PlotType = EZPlotType.Buffer;
            audioPlot.ShouldFill = true;
            audioPlot.ShouldMirror = true;
            audioFile.GetWaveformDataWithCompletionBlock((ref IntPtr waveformData, int length) => 
            {
                float[] waveformDataArray = new float[length];
                Marshal.Copy(waveformData, waveformDataArray, 0, (int)(length));

                audioPlot.UpdateBuffer(waveformDataArray, (uint)length);
            });

            //
            // Play the audio file
            //
            player.AudioFileProperty = audioFile;
        }

        void Play(object sender, EventArgs e)
        {
            if (player.IsPlaying)
            {
                player.Pause();
                playBtn.SetTitle("Play", UIControlState.Normal);
            }
            else
            {
                if (audioPlot.ShouldMirror && audioPlot.PlotType == EZPlotType.Buffer)
                {
                    audioPlot.ShouldMirror = false;
                    audioPlot.ShouldFill = false;
                }
                player.Play();
                playBtn.SetTitle("Pause", UIControlState.Normal);
            }
        }

        void SeekToFrame(object sender, EventArgs e)
        {
            player.SeekToFrame((nint)((UISlider)sender).Value);
        }

        #endregion

        #region Utility

        void DrawBufferPlot()
        {
            this.audioPlot.PlotType = EZPlotType.Buffer;
            this.audioPlot.ShouldMirror = false;
            this.audioPlot.ShouldFill = false;
        }

        void DrawRollingPlot()
        {
            this.audioPlot.PlotType = EZPlotType.Rolling;
            this.audioPlot.ShouldMirror = true;
            this.audioPlot.ShouldFill = true;
        }

        #endregion

        #region EZAudioPlayerDelegate

        [Export("audioPlayer:playedAudio:withBufferSize:withNumberOfChannels:inAudioFile:")]
        public void PlayedAudio(EZAudioPlayer audioPlayer, ref IntPtr buffer, uint bufferSize, uint numberOfChannels, EZAudioFile audioFile)
        {
            float[] bufferArrays = new float[bufferSize];
            Marshal.Copy(buffer, bufferArrays, 0, (int)(bufferSize));

            BeginInvokeOnMainThread(() =>
            {
                audioPlot?.UpdateBuffer(bufferArrays, bufferSize);
            });
        }

        [Export("audioPlayer:updatedPosition:inAudioFile:")]
        public void UpdatedPosition(EZAudioPlayer audioPlayer, long framePosition, EZAudioFile audioFile)
        {
            BeginInvokeOnMainThread(() =>
            {
                positionSlider.Value = (float)framePosition;
            });
        }

        #endregion
    }
}
