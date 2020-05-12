using System;
using System.Runtime.InteropServices;

namespace SDL2Net.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_Keysym
    {
        public SDL_scancode scancode;
        public uint keycode;
        public ushort mod;
        public uint unused;
    }
    
    internal static partial class SDL
    {
        [DllImport(SDL2Lib)]
        public static extern IntPtr SDL_GetKeyboardState(out int numkeys);
    }
}