using System;
using SDL2Net.Video;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_MessageBoxFlags;
using static SDL2Net.Util;

namespace SDL2Net
{
    public static class MessageBox
    {
        public static void ShowInformation(string title, string message, Window? window = null)
        {
            var result = SDL_ShowSimpleMessageBox(
                SDL_MESSAGEBOX_INFORMATION,
                title,
                message,
                window?.WindowPtr ?? IntPtr.Zero);
            ThrowIfFailed(result);
        }
        
        public static void ShowWarning(string title, string message, Window? window = null)
        {
            var result = SDL_ShowSimpleMessageBox(
                SDL_MESSAGEBOX_WARNING,
                title,
                message,
                window?.WindowPtr ?? IntPtr.Zero);
            ThrowIfFailed(result);
        }
        
        public static void ShowError(string title, string message, Window? window = null)
        {
            var result = SDL_ShowSimpleMessageBox(
                SDL_MESSAGEBOX_ERROR,
                title,
                message,
                window?.WindowPtr ?? IntPtr.Zero);
            ThrowIfFailed(result);
        }
    }
}