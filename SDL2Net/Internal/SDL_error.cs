using System;
using System.Runtime.InteropServices;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        [DllImport(SDL2Lib)]
        public static extern void SDL_ClearError();

        [DllImport(SDL2Lib)]
        public static extern IntPtr SDL_GetError();
    }
}