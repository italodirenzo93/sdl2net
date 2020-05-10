using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static SDL2Net.SDL;
using static SDL2Net.SDL_Init;
using static SDL2Net.SDL_WindowFlags;

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
                SDL_WINDOW_SHOWN);
            if (pWindow == IntPtr.Zero)
            {
                stderr.WriteLine("Could not create window: {0}", Marshal.PtrToStringAnsi(SDL_GetError()));
            }
            
            Thread.Sleep(3000);

            SDL_DestroyWindow(pWindow);
            pWindow = IntPtr.Zero;

            SDL_Quit();
        }
    }
}