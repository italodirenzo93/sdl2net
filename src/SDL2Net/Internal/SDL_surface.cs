using System;
using System.Runtime.InteropServices;
using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_Surface
    {
        public uint flags;
        public IntPtr format;
        public int w, h;
        public int pitch;
        public IntPtr pixels;
        public IntPtr userdata;
        public int locked;
        public IntPtr lockData;
        public SDL_Rect clipRect;
        public IntPtr map;
        public int refCount;
    }

    internal static partial class SDL
    {
        public delegate IntPtr SDL_CreateRGBSurface(uint flags, int width, int height, int depth, uint rmask,
            uint gmask,
            uint bmask, uint amask);

        public delegate IntPtr SDL_LoadBMP_RW(IntPtr src, int freeSrc);

        public delegate void SDL_FreeSurface(IntPtr surface);

        public static readonly SDL_CreateRGBSurface CreateRgbSurface =
            Util.LoadFunction<SDL_CreateRGBSurface>(NativeLibrary, nameof(SDL_CreateRGBSurface));

        public static readonly SDL_LoadBMP_RW LoadBmpRw =
            Util.LoadFunction<SDL_LoadBMP_RW>(NativeLibrary, nameof(SDL_LoadBMP_RW));

        public static readonly SDL_FreeSurface FreeSurface =
            Util.LoadFunction<SDL_FreeSurface>(NativeLibrary, nameof(SDL_FreeSurface));
    }
}