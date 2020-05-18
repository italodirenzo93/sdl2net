using System;
using System.Runtime.InteropServices;
using SDL2Net.Input;
using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_Keysym
    {
        public Key scancode;
        public uint keycode;
        public ushort mod;
        public uint unused;
    }

    internal static partial class SDL
    {
        public delegate IntPtr SDL_GetKeyboardState(out int numkeys);

        public static readonly SDL_GetKeyboardState GetKeyboardState =
            Util.LoadFunction<SDL_GetKeyboardState>(NativeLibrary, nameof(SDL_GetKeyboardState));
    }
}