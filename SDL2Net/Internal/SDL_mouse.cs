using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        public delegate int SDL_GetMouseState(out int x, out int y);

        public delegate int SDL_ShowCursor(int toggle);

        public static readonly SDL_GetMouseState GetMouseState =
            Util.LoadFunction<SDL_GetMouseState>(NativeLibrary, nameof(SDL_GetMouseState));

        public static readonly SDL_ShowCursor ShowCursor =
            Util.LoadFunction<SDL_ShowCursor>(NativeLibrary, nameof(SDL_ShowCursor));
    }
}