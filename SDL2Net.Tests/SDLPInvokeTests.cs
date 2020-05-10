using System;
using Xunit;

using static SDL2Net.SDL;
using static SDL2Net.SDL_Init;

namespace SDL2Net.Tests
{
    public class SDLPInvokeTests
    {
        [Fact]
        public void SDL_Init_InitializedTheRequestedSubsystems()
        {
            const SDL_Init flags = SDL_INIT_VIDEO | SDL_INIT_EVENTS;
            SDL_Init(flags);
            Assert.Equal(flags, SDL_WasInit(0));
            SDL_Quit();
        }
    }
}