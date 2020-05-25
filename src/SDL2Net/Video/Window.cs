using System;
using System.Drawing;
using System.Runtime.InteropServices;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL_WindowFlags;
using static SDL2Net.Utilities.Util;

namespace SDL2Net.Video
{
    /// <summary>
    ///     SDL Window object. https://wiki.libSDL.Impl.GetFunction<org/CategoryVideo
    /// </summary>
    public class Window : IDisposable
    {
        internal readonly IntPtr WindowPtr;

        public Window(string title, int x, int y, int w, int h)
        {
            var result = SDL.Impl.GetFunction<SDL_InitSubSystem>()(SDL_InitFlags.SDL_INIT_VIDEO);
            if (result != default) throw new SDLException();

            WindowPtr = SDL.Impl.GetFunction<SDL_CreateWindow>()(title, x, y, w, h, 0);
            if (WindowPtr == default) throw new SDLException();
        }

        /// <summary>
        ///     Gets or sets the title of this window.
        /// </summary>
        public string Title
        {
            get => Marshal.PtrToStringAnsi(SDL.Impl.GetFunction<SDL_GetWindowTitle>()(WindowPtr));
            set => SDL.Impl.GetFunction<SDL_SetWindowTitle>()(WindowPtr, value);
        }

        /// <summary>
        ///     Gets or sets the screen position of this window.
        /// </summary>
        public Point Position
        {
            get
            {
                SDL.Impl.GetFunction<SDL_GetWindowPosition>()(WindowPtr, out var x, out var y);
                return new Point(x, y);
            }
            set => SDL.Impl.GetFunction<SDL_SetWindowPosition>()(WindowPtr, value.X, value.Y);
        }

        /// <summary>
        ///     Gets ot sets the size of this window.
        /// </summary>
        public Size Size
        {
            get
            {
                SDL.Impl.GetFunction<SDL_GetWindowSize>()(WindowPtr, out var w, out var h);
                return new Size(w, h);
            }
            set => SDL.Impl.GetFunction<SDL_SetWindowSize>()(WindowPtr, value.Width, value.Height);
        }

        /// <summary>
        ///     Gets ot sets whether this window is resizable.
        /// </summary>
        public bool Resizable
        {
            get => SDL.Impl.GetFunction<SDL_GetWindowFlags>()(WindowPtr).HasFlag(SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            set => SDL.Impl.GetFunction<SDL_SetWindowResizable>()(WindowPtr, Convert.ToInt32(value));
        }

        /// <summary>
        ///     Gets or sets whether this window is visible or hidden.
        /// </summary>
        public bool IsVisible
        {
            get => SDL.Impl.GetFunction<SDL_GetWindowFlags>()(WindowPtr).HasFlag(SDL_WindowFlags.SDL_WINDOW_SHOWN);
            set
            {
                if (value)
                    SDL.Impl.GetFunction<SDL_ShowWindow>()(WindowPtr);
                else
                    SDL.Impl.GetFunction<SDL_HideWindow>()(WindowPtr);
            }
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            OutputDebugString("Disposing {0}: disposing = {1}", nameof(Window), disposing);

            if (_disposed) return;

            SDL.Impl.GetFunction<SDL_DestroyWindow>()(WindowPtr);
            SDL.Impl.GetFunction<SDL_QuitSubSystem>()(SDL_InitFlags.SDL_INIT_VIDEO);

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Window()
        {
            Dispose(false);
        }

        #endregion
    }
}