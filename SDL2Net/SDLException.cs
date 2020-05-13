using System;
using System.Runtime.InteropServices;
using SDL2Net.Internal;

namespace SDL2Net
{
    public class SDLException : Exception
    {
        public override string Message => Marshal.PtrToStringAnsi(SDL.GetError());
    }
}