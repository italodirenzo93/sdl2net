using System;
using System.Runtime.InteropServices;
using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_PixelFormat
    {
        public uint format;
        public IntPtr palette;
        public byte bitsPerPixel, bytesPerPixel;
        public uint rMask, gMask, bMask, aMask;
        public byte rLoss, gLoss, bLoss, aLoss;
        public byte rShift, gShift, bShift, aShift;
        public int refCount;
        public IntPtr next;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_Palette
    {
        public int nColors;
        public IntPtr colors;
        public uint version;
        public int refCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_Color
    {
        public byte r, g, b, a;
    }

    internal static partial class SDL
    {
        public delegate IntPtr SDL_CreateTexture(IntPtr renderer, uint format, int access, int w, int h);

        public delegate IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

        public delegate void SDL_DestroyTexture(IntPtr texture);

        public delegate int SDL_LockTexture(IntPtr texture, [In] SDL_Rect rect, out IntPtr pixels, out int pitch);

        public delegate int SDL_MapRGB(IntPtr pixelFormat, int r, int g, int b);

        public delegate int SDL_MapRGBA(IntPtr pixelFormat, int r, int g, int b, int a);

        public delegate void SDL_UnlockTexture(IntPtr texture);

        public static readonly SDL_CreateTexture CreateTexture =
            Util.LoadFunction<SDL_CreateTexture>(NativeLibrary, nameof(SDL_CreateTexture));

        public static readonly SDL_CreateTextureFromSurface CreateTextureFromSurface =
            Util.LoadFunction<SDL_CreateTextureFromSurface>(NativeLibrary, nameof(SDL_CreateTextureFromSurface));

        public static readonly SDL_DestroyTexture DestroyTexture =
            Util.LoadFunction<SDL_DestroyTexture>(NativeLibrary, nameof(SDL_DestroyTexture));

        public static readonly SDL_LockTexture LockTexture =
            Util.LoadFunction<SDL_LockTexture>(NativeLibrary, nameof(SDL_LockTexture));

        public static readonly SDL_UnlockTexture UnlockTexture =
            Util.LoadFunction<SDL_UnlockTexture>(NativeLibrary, nameof(SDL_UnlockTexture));

        public static readonly SDL_MapRGB MapRgb = Util.LoadFunction<SDL_MapRGB>(NativeLibrary, nameof(SDL_MapRGB));
        public static readonly SDL_MapRGBA MapRgba = Util.LoadFunction<SDL_MapRGBA>(NativeLibrary, nameof(SDL_MapRGBA));
    }
}