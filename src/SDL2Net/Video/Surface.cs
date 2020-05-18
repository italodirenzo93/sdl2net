using System;
using SDL2Net.Internal;

namespace SDL2Net.Video
{
    public class Surface : IDisposable
    {
        internal readonly IntPtr SurfacePtr;

        public Surface(int width, int height, int depth, long rMask, long gMask, long bMask, long aMask)
        {
            SurfacePtr = SDL.CreateRgbSurface(0, width, height, depth, (uint) rMask, (uint) gMask, (uint) bMask,
                (uint) aMask);
            if (SurfacePtr == IntPtr.Zero) throw new SDLException();

            Width = width;
            Height = height;
            Depth = depth;
        }

        public int Width { get; }

        public int Height { get; }

        public int Depth { get; }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            SDL.FreeSurface(SurfacePtr);

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Surface()
        {
            Dispose(false);
        }

        #endregion
    }
}