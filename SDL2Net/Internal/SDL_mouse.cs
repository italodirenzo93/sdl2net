using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        public delegate int SDL_GetMouseState(out int x, out int y);

        public static readonly SDL_GetMouseState GetMouseState =
            Util.LoadFunction<SDL_GetMouseState>(NativeLibrary, nameof(SDL_GetMouseState));
    }
}