using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using SDL2Net.Internal;
using SDL2Net.Utilities;

namespace SDL2Net.Video
{
    [Flags]
    public enum RendererFlags
    {
        Software = 1,
        Accelerated = 2,
        PresentVSync = 4,
        TargetTexture = 8
    }

    [Flags]
    public enum RenderFlip
    {
        None,
        Horizontal,
        Vertical
    }

    public enum ScaleQuality
    {
        Nearest = 0,
        Linear = 1,
        Best = 2
    }

    /// <summary>
    ///     SDL Renderer object. https://wiki.libsdl.org/CategoryRender
    /// </summary>
    public class Renderer : IDisposable
    {
        private const string HintRenderScaleQuality = "SDL_RENDER_SCALE_QUALITY";

        internal readonly IntPtr RendererPtr;

        /// <summary>
        ///     Creates a new hardware-accelerated 2D renderer.
        /// </summary>
        /// <param name="window">The window to attach this rendering context to</param>
        /// <param name="flags">Additional flags describing the capabilities of the renderer</param>
        public Renderer(Window window, RendererFlags flags = RendererFlags.Accelerated)
        {
            RendererPtr = SDL.CreateRenderer(window.WindowPtr, -1, flags);
            Util.ThrowIfFailed(RendererPtr);
        }

        /// <summary>
        ///     Gets or sets the current drawing color of this renderer.
        /// </summary>
        public Color DrawColor
        {
            get
            {
                var result = SDL.GetRenderDrawColor(RendererPtr, out var r, out var g, out var b, out var a);
                if (result != default) throw new SDLException();
                return Color.FromArgb(a, r, g, b);
            }
            set
            {
                var result = SDL.SetRenderDrawColor(RendererPtr, value.R, value.G, value.B, value.A);
                if (result != default) throw new SDLException();
            }
        }

        /// <summary>
        ///     The scaling quality of this renderer. Null if not specified.
        /// </summary>
        public ScaleQuality ScaleQuality
        {
            get
            {
                var hint = Marshal.PtrToStringAnsi(SDL.GetHint(HintRenderScaleQuality));
                if (hint == null) return ScaleQuality.Nearest;
                return (ScaleQuality) int.Parse(hint);
            }
            set
            {
                var result = SDL.SetHint(HintRenderScaleQuality, ((int)value).ToString());
                if (!result) throw new SDLException();
            }
        }

        /// <summary>
        /// The target surface for this renderer to draw to.
        /// Passing <code>null</code> sets the target to the window this renderer
        /// was createf for.
        /// </summary>
        public Texture? RenderTarget
        {
            get
            {
                var ptr = SDL.GetRenderTarget(RendererPtr);
                return ptr == IntPtr.Zero ? null : new Texture(ptr);
            }
            set
            {
                // TODO: check if this renderer supports render targets
                var result = SDL.SetRenderTarget(RendererPtr, value != null ? value.TexturePtr : IntPtr.Zero);
                if (result == default) throw new SDLException();
            }
        }

        /// <summary>
        ///     Clears the display area and fills with the color of <see cref="DrawColor" />.
        /// </summary>
        public Renderer Clear()
        {
            var result = SDL.RenderClear(RendererPtr);
            if (result != default) throw new SDLException();
            return this;
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
        public Renderer DrawLine(int x1, int y1, int x2, int y2)
        {
            var result = SDL.RenderDrawLine(RendererPtr, x1, y1, x2, y2);
            if (result != default) throw new SDLException();
            return this;
        }

        /// <summary>
        ///     Draw a line.
        /// </summary>
        /// <param name="from">Starting point</param>
        /// <param name="to">Destination point</param>
        public Renderer DrawLine(Point from, Point to) => DrawLine(from.X, from.Y, to.X, to.Y);

        /// <summary>
        ///     Draw a sequence of lines.
        /// </summary>
        /// <param name="points">Sequence points defining the positions of the individual lines</param>
        public Renderer DrawLines(IEnumerable<Point> points)
        {
            var sdlPoints = points.Select(p => p.ToSdlPoint()).ToArray();
            var result = SDL.RenderDrawLines(RendererPtr, sdlPoints, sdlPoints.Length);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer DrawPoint(int x, int y)
        {
            var result = SDL.RenderDrawPoint(RendererPtr, x, y);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer DrawPoint(Point point) => DrawPoint(point.X, point.Y);

        public Renderer DrawPoints(IEnumerable<Point> points)
        {
            var sdlPoints = points.Select(p => p.ToSdlPoint()).ToArray();
            var result = SDL.RenderDrawPoints(RendererPtr, sdlPoints, sdlPoints.Length);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer DrawRect(int x, int y, int width, int height) => DrawRect(new Rectangle(x, y, width, height));

        public Renderer DrawRect(Rectangle rect)
        {
            var sdlRect = rect.ToSdlRect();
            var result = SDL.RenderDrawRect(RendererPtr, ref sdlRect);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer DrawRects(IEnumerable<Rectangle> rectangles)
        {
            var sdlRects = rectangles.Select(r => r.ToSdlRect()).ToArray();
            var result = SDL.RenderDrawRects(RendererPtr, sdlRects, sdlRects.Length);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer FillRect(int x, int y, int width, int height) => FillRect(new Rectangle(x, y, width, height));

        public Renderer FillRect(Rectangle rect)
        {
            var sdlRect = rect.ToSdlRect();
            var result = SDL.RenderFillRect(RendererPtr, ref sdlRect);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer FillRects(IEnumerable<Rectangle> rectangles)
        {
            var sdlRects = rectangles.Select(r => r.ToSdlRect()).ToArray();
            var result = SDL.RenderFillRects(RendererPtr, sdlRects, sdlRects.Length);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer CopyTexture(Texture texture, Rectangle? dest = null, Rectangle? source = null)
        {
            var result = SDL.RenderCopy(RendererPtr, texture.TexturePtr, GetRectOrDefault(texture, source),
                GetRectOrDefault(texture, dest));
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer CopyTexture(Texture texture, Rectangle? dest, Rectangle? source, double angle, Point? origin,
            RenderFlip flip)
        {
            var result = SDL.RenderCopyEx(RendererPtr, texture.TexturePtr, GetRectOrDefault(texture, source),
                GetRectOrDefault(texture, dest),
                angle, GetPointOrDefault(texture, origin), flip);
            if (result != default) throw new SDLException();
            return this;
        }

        public Renderer SetDrawColor(Color color)
        {
            DrawColor = color;
            return this;
        }

        private static SDL_Rect GetRectOrDefault(Texture texture, Rectangle? rectangle)
        {
            return rectangle?.ToSdlRect() ?? new SDL_Rect { x = 0, y = 0, w = texture.Width, h = texture.Height };
        }

        private static SDL_Point GetPointOrDefault(Texture texture, Point? point)
        {
            return point?.ToSdlPoint() ?? new SDL_Point { x = texture.Width / 2, y = texture.Height / 2 };
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Util.OutputDebugString("Disposing {0}: disposing = {1}", nameof(Renderer), disposing);
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