using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL_RendererFlags;
using static SDL2Net.Utilities.Util;

namespace SDL2Net.Video
{
    [Flags]
    public enum RenderFlip
    {
        None,
        Horizontal,
        Vertical
    }

    /// <summary>
    ///     SDL Renderer object. https://wiki.libsdl.org/CategoryRender
    /// </summary>
    public class Renderer : IDisposable
    {
        internal readonly IntPtr RendererPtr;

        /// <summary>
        ///     Creates a new hardware-accelerated 2D renderer.
        /// </summary>
        /// <param name="window">The window to attach this rendering context to</param>
        public Renderer(Window window)
        {
            RendererPtr = SDL.CreateRenderer(window.WindowPtr, -1, SDL_RENDERER_ACCELERATED);
            ThrowIfFailed(RendererPtr);
        }

        /// <summary>
        ///     Gets or sets the current drawing color of this renderer.
        /// </summary>
        public Color DrawColor
        {
            get
            {
                ThrowIfFailed(SDL.GetRenderDrawColor(RendererPtr, out var r, out var g, out var b, out var a));
                return Color.FromArgb(a, r, g, b);
            }
            set => ThrowIfFailed(SDL.SetRenderDrawColor(RendererPtr, value.R, value.G, value.B, value.A));
        }

        /// <summary>
        ///     Clears the display area and fills with the color of <see cref="DrawColor" />.
        /// </summary>
        public void Clear()
        {
            ThrowIfFailed(SDL.RenderClear(RendererPtr));
        }

        /// <summary>
        ///     Flushes all queued rendering commands to the video card.
        /// </summary>
        public void Present()
        {
            SDL.RenderPresent(RendererPtr);
        }

        /// <summary>
        ///     Draw a line.
        /// </summary>
        /// <param name="x1">Starting X position</param>
        /// <param name="y1">Starting Y position</param>
        /// <param name="x2">Destination X position</param>
        /// <param name="y2">Destination Y position</param>
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            ThrowIfFailed(SDL.RenderDrawLine(RendererPtr, x1, y1, x2, y2));
        }

        /// <summary>
        ///     Draw a line.
        /// </summary>
        /// <param name="from">Starting point</param>
        /// <param name="to">Destination point</param>
        public void DrawLine(Point from, Point to)
        {
            DrawLine(from.X, from.Y, to.X, to.Y);
        }

        /// <summary>
        ///     Draw a sequence of lines.
        /// </summary>
        /// <param name="points">Sequence points defining the positions of the individual lines</param>
        public void DrawLines(IEnumerable<Point> points)
        {
            var sdlPoints = points.Select(p => new SDL_Point {x = p.X, y = p.Y}).ToArray();
            ThrowIfFailed(SDL.RenderDrawLines(RendererPtr, sdlPoints, sdlPoints.Length));
        }

        public void CopyTexture(Texture texture, Rectangle? dest = null, Rectangle? source = null)
        {
            var result = SDL.RenderCopy(RendererPtr, texture.TexturePtr, GetRectOrDefault(texture, source),
                GetRectOrDefault(texture, dest));
            if (result != 0) throw new SDLException();
        }

        public void CopyTexture(Texture texture, Rectangle? dest, Rectangle? source, double angle, Point? origin,
            RenderFlip flip)
        {
            var result = SDL.RenderCopyEx(RendererPtr, texture.TexturePtr, GetRectOrDefault(texture, source),
                GetRectOrDefault(texture, dest),
                angle, GetPointOrDefault(texture, origin), flip);
            if (result != 0) throw new SDLException();
        }

        private static SDL_Rect GetRectOrDefault(Texture texture, Rectangle? rectangle)
        {
            return rectangle?.ToSdlRect() ?? new SDL_Rect {x = 0, y = 0, w = texture.Width, h = texture.Height};
        }

        private static SDL_Point GetPointOrDefault(Texture texture, Point? point)
        {
            return point?.ToSdlPoint() ?? new SDL_Point {x = texture.Width / 2, y = texture.Height / 2};
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