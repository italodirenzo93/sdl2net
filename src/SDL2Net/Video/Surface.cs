using System;
using System.IO;
using System.Runtime.InteropServices;
using SDL2Net.Internal;
using SDL2Net.Utilities;

namespace SDL2Net.Video
{
    public class Surface : IDisposable
    {
        internal readonly IntPtr SurfacePtr;

        public Surface(int width, int height, int depth, long rMask, long gMask, long bMask, long aMask)
        {
            SurfacePtr = SDL.CreateRgbSurface(0, width, height, depth, (uint) rMask, (uint) gMask, (uint) bMask,
                (uint) aMask);
            Util.ThrowIfFailed(SurfacePtr);
        }

        public Surface(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException("Bitmap file does not exist", fileName);

            SurfacePtr = SDL.LoadBmpRw(SDL.RwFromFile(fileName, "rb"), 1);
            Util.ThrowIfFailed(SurfacePtr);
        }

        public int Width => SdlSurface.w;

        public int Height => SdlSurface.h;

        private SDL_Surface SdlSurface => Marshal.PtrToStructure<SDL_Surface>(SurfacePtr);

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Util.OutputDebugString($"Disposing Surface: disposing = {disposing}");
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