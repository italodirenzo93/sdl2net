using System;
using System.Runtime.InteropServices;
using System.Threading;
using static SDL2Net.SDL;
using static SDL2Net.SDL_Init;
using static SDL2Net.SDL_WindowFlags;
using static SDL2Net.SDL_RendererFlags;

namespace SDL2Net
{
    class Program
    {
        static void Main(string[] args)
        {
            var stderr = Console.Error;

            var init = SDL_Init(SDL_INIT_VIDEO);
            if (init != 0)
            {
                stderr.WriteLine($"SDL_Init failed... returned {init}.");
                stderr.WriteLine("SDL error: {0}", Marshal.PtrToStringAnsi(SDL_GetError()));
                return;
            }

            var pWindow = SDL_CreateWindow(
                "Hello world",
                0, 0,
                640, 480,
                SDL_WINDOW_SHOWN | SDL_WINDOW_ALWAYS_ON_TOP);
            if (pWindow == IntPtr.Zero)
            {
                stderr.WriteLine("Could not create window: {0}", Marshal.PtrToStringAnsi(SDL_GetError()));
            }

            // var pRenderer = SDL_CreateRenderer(pWindow, -1, SDL_RENDERER_ACCELERATED);
            // if (pRenderer == IntPtr.Zero)
            // {
            //     stderr.WriteLine("Could not create renderer: {0}", Marshal.PtrToStringAnsi(SDL_GetError()));
            // }
            //
            SDL_ShowSimpleMessageBox(SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION, "Please", "please work SDL",
                IntPtr.Zero);

            //SDL_DestroyRenderer(pRenderer);
            SDL_DestroyWindow(pWindow);

            SDL_Quit();
        }
    }
}