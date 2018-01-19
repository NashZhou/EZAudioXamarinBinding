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

            string bundle = NSBundle.MainBundle.PathForResource("simple-drum-beat", "wav");
            audioFile = new EZAudioFile(NSUrl.FromFilename(bundle));
            player = new EZAudioPlayer()
            {
                ShouldLoop = true
            };
            player.WeakDelegate = this;

            // TODO: Add actions to all the UI controls
            player.PlayAudioFile(audioFile);
        }

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
    }
}
