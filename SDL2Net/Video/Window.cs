using System;
using System.Drawing;
using System.Runtime.InteropServices;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL_WindowFlags;
using static SDL2Net.Util;

namespace SDL2Net.Video
{
    public class Window : IDisposable
    {
        internal readonly IntPtr WindowPtr;

        public Window(string title, int x, int y, int w, int h)
        {
            ThrowIfFailed(SDL.InitSubSystem(SDL_InitFlags.SDL_INIT_VIDEO));
            WindowPtr = SDL.CreateWindow(title, x, y, w, h, 0);
            ThrowIfFailed(WindowPtr);
            IsVisible = true;
        }

        public bool IsVisible { get; }

        public string Title
        {
            get => Marshal.PtrToStringAnsi(SDL.GetWindowTitle(WindowPtr));
            set => SDL.SetWindowTitle(WindowPtr, value);
        }

        public Point Position
        {
            get
            {
                SDL.GetWindowPosition(WindowPtr, out var x, out var y);
                return new Point(x, y);
            }
            set => SDL.SetWindowPosition(WindowPtr, value.X, value.Y);
        }

        public Size Size
        {
            get
            {
                SDL.GetWindowSize(WindowPtr, out var w, out var h);
                return new Size(w, h);
            }
            set => SDL.SetWindowSize(WindowPtr, value.Width, value.Height);
        }

        public bool Resizable
        {
            get => SDL.GetWindowFlags(WindowPtr).HasFlag(SDL_WINDOW_RESIZABLE);
            set => SDL.SetWindowResizable(WindowPtr, Convert.ToInt32(value));
        }

        public bool IsShown
        {
            get => SDL.GetWindowFlags(WindowPtr).HasFlag(SDL_WINDOW_SHOWN);
            set
            {
                if (value)
                    SDL.ShowWindow(WindowPtr);
                else
                    SDL.HideWindow(WindowPtr);
            }
        }

        #region IDisposable Support

        private bool _disposed;
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            
            if (disposing)
            {
            }

            SDL.DestroyWindow(WindowPtr);
            SDL.QuitSubSystem(SDL_InitFlags.SDL_INIT_VIDEO);
            
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