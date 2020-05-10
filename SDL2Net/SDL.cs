using System;
using System.Runtime.InteropServices;

namespace SDL2Net
{
    [Flags]
    public enum SDL_Init : uint
    {
        SDL_INIT_TIMER = 0x00000001u,
        SDL_INIT_AUDIO = 0x00000010u,
        SDL_INIT_VIDEO = 0x00000020u,

        /**< SDL_INIT_VIDEO implies SDL_INIT_EVENTS */
        SDL_INIT_JOYSTICK = 0x00000200u,

        /**< SDL_INIT_JOYSTICK implies SDL_INIT_EVENTS */
        SDL_INIT_HAPTIC = 0x00001000u,
        SDL_INIT_GAMECONTROLLER = 0x00002000u,

        /**< SDL_INIT_GAMECONTROLLER implies SDL_INIT_JOYSTICK */
        SDL_INIT_EVENTS = 0x00004000u,
        SDL_INIT_SENSOR = 0x00008000u,

        SDL_INIT_EVERYTHING = SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO | SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC |
                              SDL_INIT_GAMECONTROLLER | SDL_INIT_EVENTS | SDL_INIT_SENSOR
    }

    public static partial class SDL
    {
#if WINDOWS
        private const string SDL2Lib = "SDL.dll";
#elif MACOS
        //private const string SDL2Lib = "SDL2.framework/SDL2";
        private const string SDL2Lib = "libsdl2";
#else
        #error Platform not defined
#endif

        public delegate int MainFunction(int argc, string[] argv);

        [DllImport(SDL2Lib)]
        public static extern int SDL_Init(SDL_Init flags);

        [DllImport(SDL2Lib)]
        public static extern int SDL_InitSubSystem(SDL_Init flags);

        [DllImport(SDL2Lib)]
        public static extern void SDL_Quit();

        [DllImport(SDL2Lib)]
        public static extern void SDL_QuitSubSystem(SDL_Init flags);

        [DllImport(SDL2Lib)]
        public static extern void SDL_SetMainReady();

        [DllImport(SDL2Lib)]
        public static extern SDL_Init SDL_WasInit(SDL_Init flags);

        [DllImport(SDL2Lib)]
        public static extern int SDL_WinRTRunApp(MainFunction mainFunction, IntPtr reserved);
    }
}