using System;
using System.Drawing;
using SDL2Net.Input;
using SDL2Net.Video;

namespace SDL2Net.TestApp
{
    public sealed class TestApplication : SDLApplication
    {
        private const int speed = 98;
        private readonly Renderer _renderer;
        private readonly Triangle _triangle = new Triangle(400, 250);
        private readonly Window _window;
        private double prevTime;

        // private static readonly Point[] _points = {
        //     new Point(320, 200), 
        //     new Point(300, 240), 
        //     new Point(340, 240),
        //     new Point(320, 200)
        // };

        public TestApplication()
        {
            _window = new Window("Hello!", 100, 100, 800, 600)
            {
                Resizable = true
            };
            _renderer = new Renderer(_window);
        }

        protected override void Initialize()
        {
            Keyboard.KeyPresses.Subscribe(x =>
            {
                if (x.Key == (int) Key.Escape) Quit();

                _triangle.Color = (Key) x.Key switch
                {
                    Key.Home => Color.Gold,
                    Key.End => Color.Aqua,
                    _ => _triangle.Color
                };
            });
        }

        protected override void Update(double elapsed)
        {
            var frameTime = elapsed - prevTime;
            prevTime = elapsed;

            var deltaTime = (float) frameTime / 1000;

            // Update the triangle's position
            _triangle.Update(deltaTime);

            _renderer.DrawColor = Color.Black;
            _renderer.Clear();

            // Draw our triangle to the screen
            _triangle.Draw(_renderer);

            _renderer.Present();
        }

        public override void Dispose()
        {
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