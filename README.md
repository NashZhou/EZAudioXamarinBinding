# EZAudio Xamarin Binding
A Xamarin iOS binding of syedHali's [EZAudio](https://github.com/syedhali/EZAudio) library
#### Notes
* Add the [Nuget package](https://www.nuget.org/packages/EZAudio.iOS/) to your iOS project
* The test sample only demos how to play a file with EZAudioPlotGL.
* Many functions still need wrappers in order to remove the IntPtrs provided by the library.

#### EZAudioPlayer
* This provides some example conversions from Objective C to C# based off the original documentation
* [Creating an Audio Player](https://github.com/syedhali/EZAudio#creating-an-audio-player)
``` cs
EZAudioFile audioFile;
...
audioFile = EZAudioFile.AudioFileWithURL(filePathUrl);
```
* [Playing an Audio File](https://github.com/syedhali/EZAudio#playing-an-audio-file)
``` cs
audioFile = EZAudioFile.AudioFileWithURL(filePathUrl);
// AudioFileProperty was used to avoid naming conflicts
player.AudioFileProperty = audioFile;

// Using PlayAudioFile when audio is already playing
audioFile = EZAudioFile.AudioFileWithURL(filePathUrl);
player.PlayAudioFile(audioFile);
```
* Using the IEZAudioPlayerDelegate
``` cs
[Export("audioPlayer:playedAudio:withBufferSize:withNumberOfChannels:inAudioFile:")]
public void PlayedAudio(EZAudioPlayer audioPlayer, ref IntPtr buffer, uint bufferSize, uint numberOfChannels, EZAudioFile audioFile)
{
    // These 2 lines convert the float pointer into a C# float array
    float[] bufferArrays = new float[bufferSize];
    Marshal.Copy(buffer, bufferArrays, 0, (int)(bufferSize));

    BeginInvokeOnMainThread(() =>
    {
        audioPlot?.UpdateBuffer(bufferArrays, bufferSize);
    });
}
//------------------------------------------------------------------------------
[Export("audioPlayer:updatedPosition:inAudioFile:")]
public void UpdatedPosition(EZAudioPlayer audioPlayer, long framePosition, EZAudioFile audioFile)
{
    BeginInvokeOnMainThread(() =>
    {
        positionSlider.Value = (float)framePosition;
    });
}
```
