using System;
using System.Drawing;
using System.Runtime.InteropServices;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Util;

namespace SDL2Net
{
    public class SDLWindow : IDisposable
    {
        internal readonly IntPtr WindowPtr;

        public SDLWindow(string title, int x, int y, int w, int h)
        {
            WindowPtr = SDL_CreateWindow(title, x, y, w, h, 0);
            ThrowIfFailed(WindowPtr);
            IsVisible = true;
        }
        
        public bool IsVisible { get; private set; }

        public string Title
        {
            get => Marshal.PtrToStringAnsi(SDL_GetWindowTitle(WindowPtr));
            set => SDL_SetWindowTitle(WindowPtr, value);
        }

        public Point Position
        {
            get
            {
                int x = 0, y = 0;
                SDL_GetWindowPosition(WindowPtr, ref x, ref y);
                return new Point(x, y);
            }
            set => SDL_SetWindowPosition(WindowPtr, value.X, value.Y);
        }

        public Size Size
        {
            get
            {
                int w = 0, h = 0;
                SDL_GetWindowSize(WindowPtr, ref w, ref h);
                return new Size(w, h);
            }
            set => SDL_SetWindowPosition(WindowPtr, value.Width, value.Height);
        }

        public void Show()
        {
            if (IsVisible) return;
            SDL_ShowWindow(WindowPtr);
            IsVisible = true;
        }

        public void Hide()
        {
            if (!IsVisible) return;
            SDL_HideWindow(WindowPtr);
            IsVisible = false;
        }

        public void Dispose()
        {
            SDL_DestroyWindow(WindowPtr);
        }
    }
}