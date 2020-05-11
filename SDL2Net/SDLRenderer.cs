using System;
using System.Drawing;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_RendererFlags;
using static SDL2Net.Util;

namespace SDL2Net
{
    public class SDLRenderer : IDisposable
    {
        internal readonly IntPtr RendererPtr;

        public SDLRenderer(SDLWindow window)
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
        
        public void Dispose()
        {
            SDL_DestroyRenderer(RendererPtr);
        }
    }
}
