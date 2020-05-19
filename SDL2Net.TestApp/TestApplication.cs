using System;
using System.Drawing;
using System.Reactive.Linq;
using SDL2Net.Events;
using SDL2Net.Input;
using SDL2Net.Input.Events;
using SDL2Net.Video;

namespace SDL2Net.TestApp
{
    public sealed class TestApplication : SDLApplication
    {
        private readonly InputSystem _inputSystem;
        private readonly Renderer _renderer;
        private readonly Triangle _triangle;
        private readonly Window _window;
        private long _prevTime;

        public TestApplication()
        {
            OnInitialize = Initialize;
            OnUpdate = Update;
            OnExit = Exit;

            _window = new Window("Hello!", 100, 100, 800, 600)
            {
                Resizable = true
            };

            _renderer = new Renderer(_window);

            _inputSystem = new InputSystem(this);

            _triangle = new Triangle(400, 250, _inputSystem);
        }

        private void Initialize()
        {
            _inputSystem.KeyboardEvents.OfType<KeyPressEvent>()
                .Where(e => !e.IsRepeat && e.ButtonState == ButtonState.Pressed)
                .Subscribe(e =>
                {
                    if (e.Key == Key.Escape) Quit();
                    LogEvent(e, $"{e.Key} key pressed!");
                });
        }

        private void Update()
        {
            var frameTime = Elapsed - _prevTime;
            _prevTime = Elapsed;

            var deltaTime = (float) frameTime / 1000;

            // Update the triangle's position
            _triangle.Update(deltaTime);

            _renderer.DrawColor = Color.Black;
            _renderer.Clear();

            // Draw our triangle to the screen
            _triangle.Draw(_renderer);

            _renderer.Present();
        }

        private void Exit()
        {
            Console.WriteLine("Exiting app...");
        }

        private static void LogEvent(Event @event, string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{@event.Timestamp.ToLocalTime().TimeOfDay}] ");
            Console.ResetColor();
            Console.Write($"{message}\n");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _renderer.Dispose();
                _triangle.Dispose();
                _inputSystem.Dispose();
                _window.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}