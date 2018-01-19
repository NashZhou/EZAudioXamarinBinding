// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinTest
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EZAudioKit.EZAudioPlotGL audioPlot { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl bufferRoller { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel filePathLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton playBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider positionSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider rollingSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider volumeSlider { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (audioPlot != null) {
                audioPlot.Dispose ();
                audioPlot = null;
            }

            if (bufferRoller != null) {
                bufferRoller.Dispose ();
                bufferRoller = null;
            }

            if (filePathLabel != null) {
                filePathLabel.Dispose ();
                filePathLabel = null;
            }

            if (playBtn != null) {
                playBtn.Dispose ();
                playBtn = null;
            }

            if (positionSlider != null) {
                positionSlider.Dispose ();
                positionSlider = null;
            }

            if (rollingSlider != null) {
                rollingSlider.Dispose ();
                rollingSlider = null;
            }

            if (volumeSlider != null) {
                volumeSlider.Dispose ();
                volumeSlider = null;
            }
        }
    }
}