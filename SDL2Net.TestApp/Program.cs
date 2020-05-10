using System;
using System.Runtime.InteropServices;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_Init;
using static SDL2Net.Internal.SDL_WindowFlags;
using static SDL2Net.Internal.SDL_RendererFlags;
using static SDL2Net.Internal.SDL_EventType;

namespace SDL2Net.TestApp
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
                45, 45,
                800, 600,
                SDL_WINDOW_SHOWN);
            if (pWindow == IntPtr.Zero)
            {
                stderr.WriteLine("Could not create window: {0}", Marshal.PtrToStringAnsi(SDL_GetError()));
            }

            var pRenderer = SDL_CreateRenderer(pWindow, -1, SDL_RENDERER_ACCELERATED);
            if (pRenderer == IntPtr.Zero)
            {
                stderr.WriteLine("Could not create renderer: {0}", Marshal.PtrToStringAnsi(SDL_GetError()));
            }

            var running = true;
            var @event = new SDL_Event();
            while (running)
            {
                // poll events
                while (SDL_PollEvent(ref @event) != 0)
                {
                    switch (@event.type)
                    {
                        case SDL_QUIT:
                            running = false;
                            break;
                    }
                }

                // do stuff here
                SDL_SetRenderDrawColor(pRenderer, 32, 62, 43, SDL_ALPHA_OPAQUE);
                SDL_RenderClear(pRenderer);
                SDL_RenderPresent(pRenderer);
            }

            SDL_DestroyRenderer(pRenderer);
            SDL_DestroyWindow(pWindow);

            SDL_Quit();
        }
    }
}