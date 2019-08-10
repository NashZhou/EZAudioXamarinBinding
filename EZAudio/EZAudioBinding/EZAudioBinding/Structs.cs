using System;
using System.Runtime.InteropServices;
using AudioUnit;
using ObjCRuntime;
using AudioToolbox;

namespace EZAudioKit
{
    [Native]
    public enum EZRecorderFileType : long
    {
        Aiff,
        M4a,
        Wav
    }

    [Native]
    public enum EZAudioPlayerState : long
    {
        EndOfFile,
        Paused,
        Playing,
        ReadyToPlay,
        Seeking,
        Unknown
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct TPCircularBuffer
    {
        //Was void*
        public IntPtr buffer;

        public int length;

        public int tail;

        public int head;

        public int fillCount;

        public bool atomic;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct EZPlotHistoryInfo
    {
        //Was float*
        public IntPtr buffer;

        public int bufferSize;

        public TPCircularBuffer circularBuffer;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct EZAudioNodeInfo
    {
        //Was AudioUnit*
        public AudioUnit.AudioUnit audioUnit;

        public int node;
    }

    [Native]
    public enum EZPlotType : long
    {
        Buffer,
        Rolling
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct EZAudioPlotGLPoint
    {
        public float x;

        public float y;
    }
}