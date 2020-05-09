using System;
using Xunit;

using static SDL2Net.SDL;

namespace SDL2Net.Tests
{
    public class SDLPInvokeTests
    {
        [Fact]
        public void SDL_Init_InitializedTheRequestedSubsystems()
        {
            var flags = SDLInit.Video | SDLInit.Events;
            SDL_Init(flags);
            Assert.Equal(flags, SDL_WasInit(0));
            SDL_Quit();
        }
    }
}