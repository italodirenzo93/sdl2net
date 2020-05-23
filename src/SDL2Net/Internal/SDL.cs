using System;
using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    [Flags]
    internal enum SDL_InitFlags : uint
    {
        SDL_INIT_TIMER = 0x00000001u,
        SDL_INIT_AUDIO = 0x00000010u,
        SDL_INIT_VIDEO = 0x00000020u,

        /**
         * < SDL_INIT_VIDEO implies SDL_INIT_EVENTS
         */
        SDL_INIT_JOYSTICK = 0x00000200u,

        /**
         * < SDL_INIT_JOYSTICK implies SDL_INIT_EVENTS
         */
        SDL_INIT_HAPTIC = 0x00001000u,
        SDL_INIT_GAMECONTROLLER = 0x00002000u,

        /**
         * < SDL_INIT_GAMECONTROLLER implies SDL_INIT_JOYSTICK
         */
        SDL_INIT_EVENTS = 0x00004000u,
        SDL_INIT_SENSOR = 0x00008000u,

        SDL_INIT_EVERYTHING = SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO | SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC |
                              SDL_INIT_GAMECONTROLLER | SDL_INIT_EVENTS | SDL_INIT_SENSOR
    }

    internal static partial class SDL
    {
        public delegate int SDL_Init(SDL_InitFlags flags);

        public delegate int SDL_InitSubSystem(SDL_InitFlags flags);

        public delegate void SDL_Quit();

        public delegate void SDL_QuitSubSystem(SDL_InitFlags flags);

        public delegate SDL_InitFlags SDL_WasInit(SDL_InitFlags flags);

        public delegate IntPtr SDL_GetHint(string name);

        public delegate bool SDL_SetHint(string name, string value);

        public delegate IntPtr SDL_RWFromFile(string file, string mode);

        public static readonly SDL_Init Init = Util.LoadFunction<SDL_Init>(NativeLibrary, nameof(SDL_Init));

        public static readonly SDL_InitSubSystem InitSubSystem =
            Util.LoadFunction<SDL_InitSubSystem>(NativeLibrary, nameof(SDL_InitSubSystem));

        public static readonly SDL_Quit Quit = Util.LoadFunction<SDL_Quit>(NativeLibrary, nameof(SDL_Quit));

        public static readonly SDL_QuitSubSystem QuitSubSystem =
            Util.LoadFunction<SDL_QuitSubSystem>(NativeLibrary, nameof(SDL_QuitSubSystem));

        public static readonly SDL_WasInit WasInit = Util.LoadFunction<SDL_WasInit>(NativeLibrary, nameof(SDL_WasInit));

        public static readonly SDL_GetHint GetHint = Util.LoadFunction<SDL_GetHint>(NativeLibrary, nameof(SDL_GetHint));

        public static readonly SDL_SetHint SetHint = Util.LoadFunction<SDL_SetHint>(NativeLibrary, nameof(SDL_SetHint));

        public static readonly SDL_RWFromFile RwFromFile = Util.LoadFunction<SDL_RWFromFile>(NativeLibrary, nameof(SDL_RWFromFile));

        internal static IntPtr NativeLibrary => Util.CurrentPlatform switch
        {
            Platform.Windows => Util.LoadLibrary("SDL2.dll"),
            Platform.MacOS => Util.LoadLibrary("/usr/local/Cellar/sdl2/2.0.12_1/lib/libSDL2-2.0.0.dylib"),
            _ => throw new NotImplementedException("Haven't determined how to locate SDL on this platform")
        };
    }
}