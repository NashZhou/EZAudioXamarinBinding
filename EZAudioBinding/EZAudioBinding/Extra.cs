using System;
using System.Runtime.InteropServices;
using Foundation;
namespace EZAudioKit
{
    public partial class EZAudioPlotGL
    {
        public void UpdateBuffer(float[] buffer, uint bufferSize)
        {
            IntPtr ptr = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, ptr, (int)bufferSize);
            UpdateBuffer(ptr, bufferSize);
            Marshal.FreeHGlobal(ptr);
        }
    }
}
