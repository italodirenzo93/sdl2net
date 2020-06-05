using System;
using Xunit;
using Moq;
using SDL2Net.Video;
using SDL2Net.Internal;
using Xunit.Abstractions;

namespace SDL2Net.Tests
{
    public class RendererTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public RendererTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void SetupAndTearDown()
        {
            Renderer? renderer = null;
            try
            {
                var impl = new Mock<INativeLibrary>(){DefaultValue = DefaultValue.Mock};
                
                impl.Setup(x => x.GetFunction<SDL_InitSubSystem>()).Returns(() =>
                {
                    return flags => It.Is<int>(i => i != 0);
                });
                impl.Setup(x => x.GetFunction<SDL_CreateWindow>()).Returns(() =>
                {
                    return (title, x, y, w, h, flags) => new IntPtr(It.IsAny<int>());
                });

                var createFn = new Mock<SDL_CreateRenderer>();
                impl.Setup(x => x.GetFunction<SDL_CreateRenderer>())
                    .Returns(() => createFn.Object);

                var destroyFn = new Mock<SDL_DestroyRenderer>();
                impl.Setup(x => x.GetFunction<SDL_DestroyRenderer>())
                    .Returns(() => destroyFn.Object);

                SDL.Impl = impl.Object;
                using var window = new Window(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int>());
                renderer = new Renderer(window, RendererFlags.Accelerated);
                createFn.Verify(x => x(window.WindowPtr, -1, RendererFlags.Accelerated));

                var rendererPtr = renderer.RendererPtr;
                renderer.Dispose();
                destroyFn.Verify(x => x(rendererPtr));
            }
            catch
            {
                renderer?.Dispose();
                throw;
            }
        }
    }
}