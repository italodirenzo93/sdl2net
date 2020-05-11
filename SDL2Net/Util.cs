using System;

namespace SDL2Net
{
    internal static class Util
    {
        public static void ThrowIfFailed(int status)
        {
            if (status != 0) throw new SDLException();
        }

        public static void ThrowIfFailed(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) throw new SDLException();
        }
    }
}