using System;
using System.Drawing;
using SDL2Net.Internal;

namespace SDL2Net.Video
{
    public enum PixelFormat : uint
    {
        Unknown,
        Index1Lsb,
        Index1msb,
        Index4Lsb,
        Index4Msb,
        Index8,
        Rgb332,
        Rgb444,
        Rgb555,
        Bgr555,
        Argb4444,
        Rgba4444,
        Abgr4444,
        Bgra4444,
        Argb1555,
        Rgba5551,
        Abgr1555,
        Bgra5551,
        Rgb565,
        Bgr565,
        Rgb24,
        Bgr24,
        Rgb888,
        Rgbx8888,
        Bgr888,
        Bgrx8888,
        Argb8888,
        Rgba8888,
        Abgr8888,
        Bgra8888,
        Argb2101010,
        Rgba32,
        Argb32,
        Bgra32,
        Abgr32,
        Yv12,
        Iyuv,
        Yuy2,
        Uyuy,
        Yvyu,
        Nv12,
        Nv21
    }

    public enum PixelType
    {
        Unknown,
        Index1,
        Index4,
        Index8,
        Packed8,
        Packed16,
        Packed32,
        ArrayU8,
        ArrayU16,
        ArrayU32,
        ArrayF16,
        ArrayF32
    }

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
            TexturePtr = SDL.CreateTexture(renderer.RendererPtr, (uint) format, (int) access, width, height);
            Width = width;
            Height = height;
            TextureAccess = access;
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
        ///     The access pattern used for this texture.
        /// </summary>
        public TextureAccess TextureAccess { get; }

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