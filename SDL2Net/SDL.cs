using System;
using System.Runtime.InteropServices;

namespace SDL2Net
{
    [Flags]
    public enum SDLInit : uint
    {
        Timer = 0x00000001u,
        Audio = 0x00000010u,
        Video = 0x00000010u,
        Joystick = 0x00000200u,
        Haptic = 0x00001000u,
        GameController = 0x00002000u,
        Events = 0x00004000u,
        Sensor = 0x00008000u,
        Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events | Sensor
    }

    public static class SDL
    {
        private const string SDL2Lib = "SDL2.framework/SDL2";

        public delegate int MainFunction(int argc, string[] argv);

        [DllImport(SDL2Lib)]
        public static extern int SDL_Init(SDLInit flags);

        [DllImport(SDL2Lib)]
        public static extern int SDL_InitSubSystem(SDLInit flags);

        [DllImport(SDL2Lib)]
        public static extern void SDL_Quit();

        [DllImport(SDL2Lib)]
        public static extern void SDL_QuitSubSystem(SDLInit flags);

        [DllImport(SDL2Lib)]
        public static extern void SDL_SetMainReady();

        [DllImport(SDL2Lib)]
        public static extern SDLInit SDL_WasInit(SDLInit flags);

        [DllImport(SDL2Lib)]
        public static extern int SDL_WinRTRunApp(MainFunction mainFunction, IntPtr reserved);
    }
}