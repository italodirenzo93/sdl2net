using System;
using System.Runtime.InteropServices;
using static SDL2Net.Internal.SDL;

namespace SDL2Net
{
    public class SDLWindow : IDisposable
    {
        private readonly IntPtr _window;

        public SDLWindow(string title, int x, int y, int w, int h)
        {
            _window = SDL_CreateWindow(title, x, y, w, h, 0);
            if (_window == IntPtr.Zero)
            {
                throw new SDLException();
            }

            IsVisible = true;
        }
        
        public bool IsVisible { get; private set; }

        public string Title
        {
            get => Marshal.PtrToStringAnsi(SDL_GetWindowTitle(_window));
            set => SDL_SetWindowTitle(_window, value);
        }

        public void Show()
        {
            if (IsVisible) return;
            SDL_ShowWindow(_window);
            IsVisible = true;
        }

        public void Hide()
        {
            if (!IsVisible) return;
            SDL_HideWindow(_window);
            IsVisible = false;
        }

        public void Dispose()
        {
            SDL_DestroyWindow(_window);
        }
    }
}