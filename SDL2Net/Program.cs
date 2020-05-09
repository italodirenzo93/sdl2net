using System;
using static SDL2Net.SDL;

namespace SDL2Net
{
    class Program
    {
        static void Main(string[] args)
        {
            var init = SDL_Init(SDLInit.Everything);
            if (init != 0)
            {
                Console.Error.WriteLine($"SDL_Init failed... returned {init}.");
                Console.Error.WriteLine($"SDL error: {SDL_GetError()}");
                return;
            }
            Console.WriteLine("Hello World!");
            SDL_Quit();
        }
    }
}