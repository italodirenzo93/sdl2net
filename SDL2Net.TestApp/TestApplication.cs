using System;
using System.Drawing;
using SDL2Net.Events;
using SDL2Net.Input;
using SDL2Net.Video;

namespace SDL2Net.TestApp
{
    public sealed class TestApplication : SDLApplication
    {
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
        }

        protected override void Initialize()
        {
            Keyboard.Events.Subscribe(e =>
            {
                if (e.Key == Key.Escape) Quit();
                if (!e.IsRepeat) LogEvent(e, $"{e.Key} key pressed!");
            });
        }

        protected override void Update(long elapsed)
        {
            var frameTime = elapsed - _prevTime;
            _prevTime = elapsed;

            var deltaTime = (float) frameTime / 1000;

            // Update the triangle's position
            _triangle.Update(deltaTime);
            
            _renderer.DrawColor = Color.Black;
            _renderer.Clear();

            // Draw our triangle to the screen
            _triangle.Draw(_renderer);

            _renderer.Present();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _renderer.Dispose();
                _window.Dispose();
            }
            base.Dispose(disposing);
        }

        private static void LogEvent(Event @event, string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{@event.Timestamp.ToLocalTime().TimeOfDay}] ");
            Console.ResetColor();
            Console.Write($"{message}\n");
        }

        public static void Main()
        {
            using (var app = new TestApplication())
            {
                app.Run(); 
            }
        }
    }
}