using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_RendererFlags;
using static SDL2Net.Util;

namespace SDL2Net.Video
{
    public class Renderer : IDisposable
    {
        internal readonly IntPtr RendererPtr;

        public Renderer(Window window)
        {
            RendererPtr = SDL_CreateRenderer(window.WindowPtr, -1, SDL_RENDERER_ACCELERATED);
            ThrowIfFailed(RendererPtr);
        }

        public Color DrawColor
        {
            get
            {
                byte r = 0, g = 0, b = 0, a = 0;
                ThrowIfFailed(SDL_GetRenderDrawColor(RendererPtr, ref r, ref g, ref b, ref a));
                return Color.FromArgb(a, r, g, b);
            }
            set => ThrowIfFailed(SDL_SetRenderDrawColor(RendererPtr, value.R, value.G, value.B, value.A));
        }

        public void Clear()
        {
            ThrowIfFailed(SDL_RenderClear(RendererPtr));
        }

        public void Present()
        {
            SDL_RenderPresent(RendererPtr);
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            ThrowIfFailed(SDL_RenderDrawLine(RendererPtr, x1, y1, x2, y2));
        }

        public void DrawLine(Point from, Point to)
        {
            DrawLine(from.X, from.Y, to.X, to.Y);
        }

        public void DrawLines(IEnumerable<Point> points)
        {
            var sdlPoints = points.Select(p => new SDL_Point {x = p.X, y = p.Y}).ToArray();
            ThrowIfFailed(SDL_RenderDrawLines(RendererPtr, sdlPoints, sdlPoints.Length));
        }
        
        public void Dispose()
        {
            SDL_DestroyRenderer(RendererPtr);
        }
    }
}
