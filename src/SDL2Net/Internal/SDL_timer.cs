using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        public delegate uint SDL_GetTicks();

        public static readonly SDL_GetTicks GetTicks =
            Util.LoadFunction<SDL_GetTicks>(NativeLibrary, nameof(SDL_GetTicks));
    }
}