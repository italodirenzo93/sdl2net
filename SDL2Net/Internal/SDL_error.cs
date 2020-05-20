using System;
using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        public delegate void SDL_ClearError();

        public delegate IntPtr SDL_GetError();

        public static readonly SDL_ClearError ClearError =
            Util.LoadFunction<SDL_ClearError>(NativeLibrary, nameof(SDL_ClearError));

        public static readonly SDL_GetError GetError =
            Util.LoadFunction<SDL_GetError>(NativeLibrary, nameof(SDL_GetError));
    }
}