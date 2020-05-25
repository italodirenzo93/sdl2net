using System;
using SDL2Net.Internal;
using SDL2Net.Video;
using static SDL2Net.Internal.SDL_MessageBoxFlags;

namespace SDL2Net
{
    /// <summary>
    ///     Utility class for displaying message boxes.
    /// </summary>
    public static class MessageBox
    {
        public static void ShowInformation(string title, string message, Window? window = null)
        {
            var result = SDL.Impl.GetFunction<SDL_ShowSimpleMessageBox>()(
                SDL_MESSAGEBOX_INFORMATION,
                title,
                message,
                window?.WindowPtr ?? IntPtr.Zero);
            if (result != default) throw new SDLException();
        }

        public static void ShowWarning(string title, string message, Window? window = null)
        {
            var result = SDL.Impl.GetFunction<SDL_ShowSimpleMessageBox>()(
                SDL_MESSAGEBOX_WARNING,
                title,
                message,
                window?.WindowPtr ?? IntPtr.Zero);
            if (result != default) throw new SDLException();
        }

        public static void ShowError(string title, string message, Window? window = null)
        {
            var result = SDL.Impl.GetFunction<SDL_ShowSimpleMessageBox>()(
                SDL_MESSAGEBOX_ERROR,
                title,
                message,
                window?.WindowPtr ?? IntPtr.Zero);
            if (result != default) throw new SDLException();
        }
    }
}