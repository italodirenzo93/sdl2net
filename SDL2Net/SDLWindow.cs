using System;
using System.Runtime.InteropServices;
using static SDL2Net.Internal.SDL;

namespace SDL2Net
{
    public class SDLWindow : IDisposable
    {
        internal readonly IntPtr Window;

        public SDLWindow(string title, int x, int y, int w, int h)
        {
            Window = SDL_CreateWindow(title, x, y, w, h, 0);
            if (Window == IntPtr.Zero)
            {
                throw new SDLException();
            }

            IsVisible = true;
        }
        
        public bool IsVisible { get; private set; }

        public string Title
        {
            get => Marshal.PtrToStringAnsi(SDL_GetWindowTitle(Window));
            set => SDL_SetWindowTitle(Window, value);
        }

        public void Show()
        {
            if (IsVisible) return;
            SDL_ShowWindow(Window);
            IsVisible = true;
        }

        public void Hide()
        {
            if (!IsVisible) return;
            SDL_HideWindow(Window);
            IsVisible = false;
        }

        public void Dispose()
        {
            SDL_DestroyWindow(Window);
        }
    }
}