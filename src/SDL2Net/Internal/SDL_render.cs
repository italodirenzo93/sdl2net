using System;
using System.Runtime.InteropServices;
using SDL2Net.Video;

namespace SDL2Net.Internal
{
    internal delegate IntPtr SDL_GetRenderTarget(IntPtr renderer);
    internal delegate int SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

    internal delegate int SDL_GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);
    internal delegate int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

    internal delegate IntPtr SDL_CreateRenderer(IntPtr window, int index, RendererFlags flags);
    internal delegate void SDL_DestroyRenderer(IntPtr renderer);

    internal delegate int SDL_RenderClear(IntPtr renderer);

    internal delegate int SDL_RenderCopy(IntPtr renderer, IntPtr texture, [In] SDL_Rect source, [In] SDL_Rect dest);

    internal delegate int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, [In] SDL_Rect source, [In] SDL_Rect dest,
        double angle, [In] SDL_Point center, RenderFlip flip);

    internal delegate int SDL_RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);
    internal delegate int SDL_RenderDrawLines(IntPtr renderer, [In] SDL_Point[] points, int count);

    internal delegate int SDL_RenderDrawPoint(IntPtr render, int x, int y);
    internal delegate int SDL_RenderDrawPoints(IntPtr renderer, [In] SDL_Point[] points, int count);

    internal delegate int SDL_RenderDrawRect(IntPtr render, ref SDL_Rect rect);
    internal delegate int SDL_RenderDrawRects(IntPtr render, [In] SDL_Rect[] rect, int count);

    internal delegate int SDL_RenderFillRect(IntPtr render, ref SDL_Rect rect);
    internal delegate int SDL_RenderFillRects(IntPtr render, [In] SDL_Rect[] rect, int count);

    internal delegate void SDL_RenderPresent(IntPtr renderer);

    internal interface INativeClient
    {
        TDelegate GetFunction<TDelegate>();
        TDelegate GetFunction<TDelegate>(string name);
    }

    internal sealed class SDLImpl : INativeClient
    {
        public TDelegate GetFunction<TDelegate>() => GetFunction<TDelegate>(typeof(TDelegate).Name);
        public TDelegate GetFunction<TDelegate>(string name) => Utilities.Util.LoadFunction<TDelegate>(SDL.NativeLibrary, name);
    }

    internal static partial class SDL
    {
        private static INativeClient? _impl = null;
        public static INativeClient Impl
        {
            get
            {
                if (_impl == null) _impl = new SDLImpl();
                return _impl;
            }
            set
            {
                _impl = value;
            }
        }

        public const byte SDL_ALPHA_OPAQUE = 255;
        public const byte SDL_ALPHA_TRANSPARENT = 0;
    }
}