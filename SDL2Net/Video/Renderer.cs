using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL_RendererFlags;
using static SDL2Net.Utilities.Util;

namespace SDL2Net.Video
{
    public class Renderer : IDisposable
    {
        internal readonly IntPtr RendererPtr;

        public Renderer(Window window)
        {
            RendererPtr = SDL.CreateRenderer(window.WindowPtr, -1, SDL_RENDERER_ACCELERATED);
            ThrowIfFailed(RendererPtr);
        }

        public Color DrawColor
        {
            get
            {
                ThrowIfFailed(SDL.GetRenderDrawColor(RendererPtr, out var r, out var g, out var b, out var a));
                return Color.FromArgb(a, r, g, b);
            }
            set => ThrowIfFailed(SDL.SetRenderDrawColor(RendererPtr, value.R, value.G, value.B, value.A));
        }

        public void Clear()
        {
            ThrowIfFailed(SDL.RenderClear(RendererPtr));
        }

        public void Present()
        {
            SDL.RenderPresent(RendererPtr);
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            ThrowIfFailed(SDL.RenderDrawLine(RendererPtr, x1, y1, x2, y2));
        }

        public void DrawLine(Point from, Point to)
        {
            DrawLine(from.X, from.Y, to.X, to.Y);
        }

        public void DrawLines(IEnumerable<Point> points)
        {
            var sdlPoints = points.Select(p => new SDL_Point {x = p.X, y = p.Y}).ToArray();
            ThrowIfFailed(SDL.RenderDrawLines(RendererPtr, sdlPoints, sdlPoints.Length));
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            OutputDebugString("Disposing {0}: disposing = {1}", nameof(Renderer), disposing);
            if (_disposed) return;
            if (disposing)
            {
            }

            SDL.DestroyRenderer(RendererPtr);
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Renderer()
        {
            Dispose(false);
        }

        #endregion
    }
}