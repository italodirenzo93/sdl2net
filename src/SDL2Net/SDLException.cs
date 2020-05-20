using System;
using System.Runtime.InteropServices;
using SDL2Net.Internal;

namespace SDL2Net
{
    /// <summary>
    ///     An error from SDL wrapped as an exception.
    /// </summary>
    public class SDLException : Exception
    {
        /// <summary>
        ///     An error string from <code>SDL_GetError()</code>.
        /// </summary>
        public override string Message => Marshal.PtrToStringAnsi(SDL.GetError());
    }
}