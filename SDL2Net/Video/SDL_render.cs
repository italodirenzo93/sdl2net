using System;
using System.Runtime.InteropServices;

namespace SDL2Net
{
    [Flags]
    public enum SDL_RendererFlags : uint
    {
        SDL_RENDERER_SOFTWARE = 0x00000001,

        /**< The renderer is a software fallback */
        SDL_RENDERER_ACCELERATED = 0x00000002,

        /**< The renderer uses hardware
                                                     acceleration */
        SDL_RENDERER_PRESENTVSYNC = 0x00000004,

        /**< Present is synchronized
                                                     with the refresh rate */
        SDL_RENDERER_TARGETTEXTURE = 0x00000008 /**< The renderer supports
                                                     rendering to texture */
    }

    public static partial class SDL
    {
        [DllImport(SDL2Lib)]
        public static extern IntPtr SDL_CreateRenderer(IntPtr window, int index, SDL_RendererFlags flags);

        [DllImport(SDL2Lib)]
        public static extern void SDL_DestroyRenderer(IntPtr renderer);
    }
}