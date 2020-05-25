using System;
using System.Runtime.InteropServices;
using SDL2Net.Video;

namespace SDL2Net.Internal
{
    public delegate IntPtr SDL_GetRenderTarget(IntPtr renderer);
    public delegate int SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

    public delegate int SDL_GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);
    public delegate int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

    public delegate IntPtr SDL_CreateRenderer(IntPtr window, int index, RendererFlags flags);
    public delegate void SDL_DestroyRenderer(IntPtr renderer);

    public delegate int SDL_RenderClear(IntPtr renderer);

    public delegate int SDL_RenderCopy(IntPtr renderer, IntPtr texture, [In] SDL_Rect source, [In] SDL_Rect dest);

    public delegate int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, [In] SDL_Rect source, [In] SDL_Rect dest,
        double angle, [In] SDL_Point center, RenderFlip flip);

    public delegate int SDL_RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);
    public delegate int SDL_RenderDrawLines(IntPtr renderer, [In] SDL_Point[] points, int count);

    public delegate int SDL_RenderDrawPoint(IntPtr render, int x, int y);
    public delegate int SDL_RenderDrawPoints(IntPtr renderer, [In] SDL_Point[] points, int count);

    public delegate int SDL_RenderDrawRect(IntPtr render, ref SDL_Rect rect);
    public delegate int SDL_RenderDrawRects(IntPtr render, [In] SDL_Rect[] rect, int count);

    public delegate int SDL_RenderFillRect(IntPtr render, ref SDL_Rect rect);
    public delegate int SDL_RenderFillRects(IntPtr render, [In] SDL_Rect[] rect, int count);

    public delegate void SDL_RenderPresent(IntPtr renderer);

    internal static partial class SDL
    {
        public const byte SDL_ALPHA_OPAQUE = 255;
        public const byte SDL_ALPHA_TRANSPARENT = 0;
    }
}