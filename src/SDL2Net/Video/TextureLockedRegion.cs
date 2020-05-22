using System;
using System.Drawing;
using System.Runtime.InteropServices;
using SDL2Net.Internal;

namespace SDL2Net.Video
{
    public class TextureLockedRegion
    {
        private readonly int[] _pixels;
        private readonly IntPtr _pixelsPtr;

        internal TextureLockedRegion(IntPtr pixelsPtr, Rectangle rect, int pitch)
        {
            _pixelsPtr = pixelsPtr;
            var numPixels = pitch / 4 * rect.Height;
            _pixels = new int[numPixels];
            Marshal.Copy(pixelsPtr, _pixels, 0, numPixels);
        }

        public TextureLockedRegion SetPixel(int x, int y, Color color)
        {
            _pixels[x * y] = SDL.MapRgba(IntPtr.Zero, color.R, color.G, color.B, color.A);
            return this;
        }

        public void Commit()
        {
            Marshal.Copy(_pixels, 0, _pixelsPtr, _pixels.Length);
        }
    }
}