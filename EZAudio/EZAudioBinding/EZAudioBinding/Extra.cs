using System;
using System.Runtime.InteropServices;
using Foundation;
namespace EZAudioKit
{
    public partial class EZAudioPlotGL
    {
        public void UpdateBuffer(float[] buffer, uint bufferSize)
        {
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = handle.AddrOfPinnedObject();
                UpdateBuffer(ptr, bufferSize);
                buffer = (float[])handle.Target;
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
