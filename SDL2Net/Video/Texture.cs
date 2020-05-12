using System;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Util;

namespace SDL2Net.Video
{
    public class Texture : IDisposable
    {
        internal readonly IntPtr TexturePtr;

        public Texture(Renderer renderer, int width, int height)
        {
            TexturePtr = SDL_CreateTexture(renderer.RendererPtr, 0, 0, width, height);
            ThrowIfFailed(TexturePtr);
        }

        public void Dispose()
        {
            SDL_DestroyTexture(TexturePtr);
        }
    }
}