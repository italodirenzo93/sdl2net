using System;
using System.Drawing;
using SDL2Net.Internal;

namespace SDL2Net.Video
{
    /// <summary>
    ///     A set of pixel data in video memory.
    /// </summary>
    public class Texture : IDisposable
    {
        internal readonly IntPtr TexturePtr;

        public Texture(Renderer renderer, int width, int height)
        {
            TexturePtr = SDL.CreateTexture(renderer.RendererPtr, 0, 0, width, height);
            Width = width;
            Height = height;
        }

        /// <summary>
        ///     The width of this texture in pixels.
        /// </summary>
        public int Width { get; }

        /// <summary>
        ///     The height of this texture in pixels.
        /// </summary>
        public int Height { get; }

        /// <summary>
        ///     Whether this texture is currently locked for writing.
        /// </summary>
        public bool IsLocked { get; private set; }

        /// <summary>
        ///     Locks this texture for write-only access.
        /// </summary>
        /// <param name="rectangle">The region within this texture to lock</param>
        /// <returns>The region of this texture that was locked</returns>
        /// <exception cref="SDLException">The texture region could not be locked for writing</exception>
        public TextureLockedRegion Lock(Rectangle rectangle)
        {
            var result = SDL.LockTexture(TexturePtr, rectangle.ToSdlRect(), out var pixelsPtr, out var pitch);
            if (result != 0) throw new SDLException();
            IsLocked = true;
            return new TextureLockedRegion(pixelsPtr, rectangle, pitch);
        }

        /// <summary>
        ///     Unlocks this texture when it as locked previously for writing.
        /// </summary>
        public void Unlock()
        {
            SDL.UnlockTexture(TexturePtr);
            IsLocked = false;
        }

        public static Texture CreateFromFile(string filename)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
            }

            SDL.DestroyTexture(TexturePtr);

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Texture()
        {
            Dispose(false);
        }

        #endregion
    }
}