using System;
using System.Runtime.InteropServices;

namespace SDL2Net.Internal
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

        [DllImport(SDL2Lib)]
        public static extern int SDL_RenderClear(IntPtr renderer);

        [DllImport(SDL2Lib)]
        public static extern void SDL_RenderPresent(IntPtr renderer);

        public const byte SDL_ALPHA_OPAQUE = 255;
        public const byte SDL_ALPHA_TRANSPARENT = 0;

        [DllImport(SDL2Lib)]
        public static extern int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);
    }
}