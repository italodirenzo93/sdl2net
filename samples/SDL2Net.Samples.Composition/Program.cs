﻿using System;
using System.Drawing;
using System.Reactive.Linq;
using SDL2Net.Input;
using SDL2Net.Input.Events;
using SDL2Net.Samples.Common;
using SDL2Net.Video;

// ReSharper disable AccessToDisposedClosure

namespace SDL2Net.Samples.Composition
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create app and requested subsystems
            using var app = new SDLApplication();
            using var inputSystem = new InputSystem(app);
            using var gamepadSystem = new GamePadSystem(app);

            // Create window and 2D renderer
            using var window = new Window("Hello!", 200, 200, 800, 600);
            using var renderer = new Renderer(window);

            // Put something on the screen
            using var triangle = new Triangle(400, 300, inputSystem);

            // Perform initialization logic like loading assets
            inputSystem.Keyboard
                .Where(e => e.Key == Key.Escape && e.ButtonState == ButtonState.Pressed)
                .Subscribe(e => app.Quit());

            // Load bitmap image from argv
            Texture? texture = null;
            if (args.Length > 0)
            {
                using var surface = new Surface(args[0]);
                Console.WriteLine($"Surface width: {surface.Width}");
                Console.WriteLine($"Surface height: {surface.Height}");
                texture = new Texture(renderer, surface);
            }

            // Define update function
            var lastTime = app.Elapsed;
            app.OnUpdate = () =>
            {
                var elapsed = app.Elapsed;
                var deltaTime = elapsed - lastTime;
                lastTime = elapsed;

                // Convert milliseconds to seconds
                var deltaSeconds = (float) deltaTime / 1000;
                triangle.Update(deltaSeconds);

                // Draw stuff
                renderer.DrawColor = Color.Black;
                renderer.Clear();

                if (texture != null)
                    renderer.CopyTexture(texture);
                else
                    triangle.Draw(renderer);

                renderer.Present();
            };

            // Do some kind of cleanup at app exit like stopping playing music
            app.OnExit = () =>
            {
                texture?.Dispose();
                Console.WriteLine("exiting composed app...");
            };

            // Knock down the dominoes
            app.Run();
        }
    }
}