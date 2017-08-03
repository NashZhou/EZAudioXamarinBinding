using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UIKit;
using Foundation;

using EZAudioKit;
using AVFoundation;

/*
    todo Implement UI control functions
*/
namespace XamarinTest
{
    public partial class ViewController : UIViewController
    {
        #region Variables
        EZAudioFile audioFile;
        EZAudioPlayer player;
        #endregion

        #region Dealloc
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            NSNotificationCenter.DefaultCenter.RemoveObserver(this);
        }
        #endregion

        #region Status bar style
        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
        #endregion

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
                Debug.WriteLine("Error setting up audio session active: " + error.LocalizedDescription);
            }

            //
            // Customizing the audio plot's look
            //
            this.audioPlot.BackgroundColor = new UIColor(0.816f, 0.349f, 0.255f, 1);
            this.audioPlot.Color = new UIColor(1, 1, 1, 1);
            this.audioPlot.PlotType = EZPlotType.Buffer;
            this.audioPlot.ShouldFill = true;
            this.audioPlot.ShouldMirror = true;

            Debug.WriteLine("outputs: " + EZAudioDevice.OutputDevices());

            string bundle = NSBundle.MainBundle.PathForResource("simple-drum-beat", "wav");
            audioFile = new EZAudioFile(NSUrl.FromFilename(bundle));
            player = new EZAudioPlayer()
            {
                ShouldLoop = true
            };
            player.WeakDelegate = this;
            //Right now this test only plays the audio file (No pause button)
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
        void PlayedAudio(EZAudioPlayer audioPlayer, IntPtr buffer, uint bufferSize, uint numberOfChannels, EZAudioFile audioFile)
		{
            BeginInvokeOnMainThread(() =>
            {
                //todo Get the value for buffer
				//float[] buffers = new float[bufferSize * 2];
                //Marshal.Copy(buffer, buffers, 0, (int)bufferSize * 2);
            });

            /* (Objective C method)
			 
            - (void)  audioPlayer:(EZAudioPlayer *)audioPlayer
			          playedAudio:(float **)buffer
			       withBufferSize:(UInt32)bufferSize
			 withNumberOfChannels:(UInt32)numberOfChannels
			          inAudioFile:(EZAudioFile *)audioFile
			{
			    __weak typeof (self) weakSelf = self;
			    dispatch_async(dispatch_get_main_queue(), ^{
			        [weakSelf.audioPlot updateBuffer:buffer[0]
			                          withBufferSize:bufferSize];
			    });
			}
			
            */
		}
    }
}
