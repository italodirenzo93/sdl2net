using System;
using SDL2Net.Input;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        public delegate void SDL_GameControllerClose(IntPtr gameController);

        public delegate byte SDL_GameControllerGetButton(IntPtr gameController, GamePadButton button);

        public delegate IntPtr SDL_GameControllerOpen(int joystickIndex);

        public static readonly SDL_GameControllerOpen GameControllerOpen =
            Util.LoadFunction<SDL_GameControllerOpen>(NativeLibrary, nameof(SDL_GameControllerOpen));

        public static readonly SDL_GameControllerClose GameControllerClose =
            Util.LoadFunction<SDL_GameControllerClose>(NativeLibrary, nameof(SDL_GameControllerClose));

        public static readonly SDL_GameControllerGetButton GameControllerGetButton =
            Util.LoadFunction<SDL_GameControllerGetButton>(NativeLibrary, nameof(SDL_GameControllerGetButton));
    }
}