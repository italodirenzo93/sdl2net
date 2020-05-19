using System;
using System.Drawing;
using SDL2Net.Input;
using SDL2Net.Video;

// ReSharper disable AccessToDisposedClosure

namespace SDL2Net.TestApp
{
    internal class Program
    {
        private static void Main()
        {
            InheritedApp();
            //ComposedApp();
        }

        private static void InheritedApp()
        {
            using var app = new TestApplication();
            app.Run();
        }

        private static void ComposedApp()
        {
            using var app = new SDLApplication();
            using var inputSystem = new InputSystem(app);
            using var gamepadSystem = new GamePadSystem(app);

            using var window = new Window("Hello!", 200, 200, 800, 600);
            using var renderer = new Renderer(window);

            app.OnUpdate = () =>
            {
                if (inputSystem.KeyboardState.IsKeyDown(Key.Escape)) app.Quit();
                renderer.DrawColor = Color.CornflowerBlue;
                renderer.Clear();
                renderer.Present();
            };

            app.OnExit = () => Console.WriteLine("exiting composed app...");

            app.Run();
        }
    }
}