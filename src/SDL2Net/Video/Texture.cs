using System;
using System.Drawing;
using SDL2Net.Internal;
using SDL2Net.Utilities;

namespace SDL2Net.Video
{
    public enum TextureAccess
    {
        Static,
        Streaming,
        Target
    }

    /// <summary>
    ///     A set of pixel data in video memory.
    /// </summary>
    public class Texture : IDisposable
    {
        internal readonly IntPtr TexturePtr;

        public Texture(Renderer renderer, PixelFormat format, TextureAccess access, int width, int height)
        {
            TexturePtr = SDL.CreateTexture(renderer.RendererPtr, (uint)format, (int)access, width, height);
            Util.ThrowIfFailed(TexturePtr);
        }

        public Texture(Renderer renderer, Surface surface)
        {
            TexturePtr = SDL.CreateTextureFromSurface(renderer.RendererPtr, surface.SurfacePtr);
            Util.ThrowIfFailed(TexturePtr);
        }

        /// <summary>
        ///     The width of this texture in pixels.
        /// </summary>
        public int Width
        {
            get
            {
                var result = SDL.QueryTexture(TexturePtr, out IntPtr format, out var access, out var w, out var h);
                Util.ThrowIfFailed(result);
                return w;
            }
        }

        /// <summary>
        ///     The height of this texture in pixels.
        /// </summary>
        public int Height
        {
            get
            {
                var result = SDL.QueryTexture(TexturePtr, out IntPtr format, out var access, out var w, out var h);
                Util.ThrowIfFailed(result);
                return h;
            }
        }

        /// <summary>
        ///     The access pattern used for this texture.
        /// </summary>
        public TextureAccess TextureAccess
        {
            get
            {
                var result = SDL.QueryTexture(TexturePtr, out IntPtr format, out var access, out var w, out var h);
                Util.ThrowIfFailed(result);
                return (TextureAccess)access;
            }
        }

        /// <summary>
        ///     Whether this texture is currently locked for writing.
        /// </summary>
        public bool IsLocked { get; private set; }

        /// <summary>
        ///     Locks this texture for write-only access. Only works with texture created with
        ///     <code>TextureAccess.Streaming</code>.
        /// </summary>
        /// <param name="rectangle">The region within this texture to lock</param>
        /// <returns>The region of this texture that was locked</returns>
        /// <exception cref="InvalidOperationException">Attempted to invoke on a non-streaming texture</exception>
        /// <exception cref="SDLException">The texture region could not be locked for writing</exception>
        public TextureLockedRegion Lock(Rectangle rectangle)
        {
            if (TextureAccess != TextureAccess.Streaming)
                throw new InvalidOperationException("Only textures with streaming access can be locked");
            var result = SDL.LockTexture(TexturePtr, rectangle.ToSdlRect(), out var pixelsPtr, out var pitch);
            if (result != 0) throw new SDLException();
            IsLocked = true;
            return new TextureLockedRegion(pixelsPtr, rectangle, pitch);
        }

        /// <summary>
        ///     Unlocks this texture when it as locked previously for writing.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempted to invoke on a non-streaming texture</exception>
        public void Unlock()
        {
            if (TextureAccess != TextureAccess.Streaming)
                throw new InvalidOperationException("Only textures with streaming access can be unlocked");
            SDL.UnlockTexture(TexturePtr);
            IsLocked = false;
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Util.OutputDebugString($"Disposing Texture: disposing = {disposing}");

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