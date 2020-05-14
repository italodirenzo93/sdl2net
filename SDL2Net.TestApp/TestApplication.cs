using System;
using System.Drawing;
using SDL2Net.Input;
using SDL2Net.Video;

namespace SDL2Net.TestApp
{
    public sealed class TestApplication : SDLApplication
    {
        private readonly GamePad _gamePad;
        private readonly Renderer _renderer;
        private readonly Triangle _triangle = new Triangle(400, 250);
        private readonly Window _window;
        private long _prevTime;

        public TestApplication()
        {
            _window = new Window("Hello!", 100, 100, 800, 600)
            {
                Resizable = true
            };
            _renderer = new Renderer(_window);
            _gamePad = new GamePad(0);
        }

        protected override void Initialize()
        {
            Keyboard.KeyPresses.Subscribe(e =>
            {
                if (e.Key == Key.Escape) Quit();
            });
        }

        protected override void Update(long elapsed)
        {
            if (_gamePad.IsButtonDown(GamePadButton.Back)) Quit();

            var frameTime = elapsed - _prevTime;
            _prevTime = elapsed;

            var deltaTime = (float) frameTime / 1000;

            // Update the triangle's position
            _triangle.Update(deltaTime);

            if (_gamePad.IsButtonDown(GamePadButton.B))
                _renderer.DrawColor = Color.CornflowerBlue;
            else
                _renderer.DrawColor = Color.Black;
            _renderer.Clear();

            // Draw our triangle to the screen
            _triangle.Draw(_renderer);

            _renderer.Present();
        }

        public override void Dispose()
        {
            _gamePad.Dispose();
            _renderer.Dispose();
            _window.Dispose();
            base.Dispose();
        }

        public static void Main()
        {
            using var app = new TestApplication();
            try
            {
                app.Run();
            }
            catch (SDLException ex)
            {
                Console.Error.WriteLine(ex);
                throw;
            }
        }
    }
}