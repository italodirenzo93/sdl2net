using System;
using System.Runtime.InteropServices;
using static SDL2Net.Internal.SDL;

namespace SDL2Net
{
    public class SDLException : Exception
    {
        public override string Message => Marshal.PtrToStringAnsi(SDL_GetError());
    }
}