using System;
using System.Drawing;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_RendererFlags;

namespace SDL2Net
{
    public class SDLRenderer : IDisposable
    {
        internal readonly IntPtr Renderer;

        public SDLRenderer(SDLWindow window)
        {
            Renderer = SDL_CreateRenderer(window.Window, -1, SDL_RENDERER_ACCELERATED);
            if (Renderer == IntPtr.Zero)
            {
                throw new SDLException();
            }
        }

        public Color DrawColor
        {
            get
            {
                byte r = 0, g = 0, b = 0, a = 0;
                SDL_GetRenderDrawColor(Renderer, ref r, ref g, ref b, ref a);
                return Color.FromArgb(a, r, g, b);
            }
            set
            {
                SDL_SetRenderDrawColor(Renderer, value.R, value.G, value.B, value.A);
            }
        }

        public void Dispose()
        {
            SDL_DestroyRenderer(Renderer);
        }

        public void Clear()
        {
            SDL_RenderClear(Renderer);
        }

        public void Present()
        {
            SDL_RenderPresent(Renderer);
        }
    }
}
