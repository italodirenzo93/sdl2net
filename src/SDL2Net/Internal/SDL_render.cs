using System;
using System.Runtime.InteropServices;
using SDL2Net.Utilities;
using SDL2Net.Video;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        public delegate IntPtr SDL_CreateRenderer(IntPtr window, int index, RendererFlags flags);

        public delegate void SDL_DestroyRenderer(IntPtr renderer);

        public delegate int SDL_GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

        public delegate int SDL_RenderClear(IntPtr renderer);

        public delegate int SDL_RenderCopy(IntPtr renderer, IntPtr texture, [In] SDL_Rect source, [In] SDL_Rect dest);

        public delegate int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, [In] SDL_Rect source, [In] SDL_Rect dest,
            double angle, [In] SDL_Point center, RenderFlip flip);

        public delegate int SDL_RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);

        public delegate int SDL_RenderDrawLines(IntPtr renderer, [In] SDL_Point[] points, int count);

        public delegate void SDL_RenderPresent(IntPtr renderer);

        public delegate int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

        public const byte SDL_ALPHA_OPAQUE = 255;
        public const byte SDL_ALPHA_TRANSPARENT = 0;

        public static readonly SDL_CreateRenderer CreateRenderer =
            Util.LoadFunction<SDL_CreateRenderer>(NativeLibrary, nameof(SDL_CreateRenderer));

        public static readonly SDL_DestroyRenderer DestroyRenderer =
            Util.LoadFunction<SDL_DestroyRenderer>(NativeLibrary, nameof(SDL_DestroyRenderer));

        public static readonly SDL_RenderClear RenderClear =
            Util.LoadFunction<SDL_RenderClear>(NativeLibrary, nameof(SDL_RenderClear));

        public static readonly SDL_RenderPresent RenderPresent =
            Util.LoadFunction<SDL_RenderPresent>(NativeLibrary, nameof(SDL_RenderPresent));

        public static readonly SDL_GetRenderDrawColor GetRenderDrawColor =
            Util.LoadFunction<SDL_GetRenderDrawColor>(NativeLibrary, nameof(SDL_GetRenderDrawColor));

        public static readonly SDL_SetRenderDrawColor SetRenderDrawColor =
            Util.LoadFunction<SDL_SetRenderDrawColor>(NativeLibrary, nameof(SDL_SetRenderDrawColor));

        public static readonly SDL_RenderDrawLine RenderDrawLine =
            Util.LoadFunction<SDL_RenderDrawLine>(NativeLibrary, nameof(SDL_RenderDrawLine));

        public static readonly SDL_RenderDrawLines RenderDrawLines =
            Util.LoadFunction<SDL_RenderDrawLines>(NativeLibrary, nameof(SDL_RenderDrawLines));

        public static readonly SDL_RenderCopy RenderCopy =
            Util.LoadFunction<SDL_RenderCopy>(NativeLibrary, nameof(SDL_RenderCopy));

        public static readonly SDL_RenderCopyEx RenderCopyEx =
            Util.LoadFunction<SDL_RenderCopyEx>(NativeLibrary, nameof(SDL_RenderCopyEx));
    }
}