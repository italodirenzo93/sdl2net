using System;
using System.Drawing;
using System.Reactive.Linq;
using SDL2Net;
using SDL2Net.Input;
using SDL2Net.Samples.Common;
using SDL2Net.Video;

// ReSharper disable AccessToDisposedClosure

// Create app and requested subsystems
using var app = new SDLApplication();
using var inputSystem = new InputSystem(app);

// Create window and 2D renderer
using var window = new Window("Hello!", 200, 200, 800, 600)
{
    Resizable = true
};
using var renderer = new Renderer(window);

// Put something on the screen
using var triangle = new Triangle(400, 300, inputSystem);
using var surface = Surface.CreateFromBitmapFile("LAND2.bmp");
Console.WriteLine($"Surface width: {surface.Width}");
Console.WriteLine($"Surface height: {surface.Height}");
using var texture = new Texture(renderer, surface);
var showTexture = true;

// Perform initialization logic like loading assets
inputSystem.Keyboard
    .IsPressed()
    .Where(e => e.Key == Key.Escape)
    .Subscribe(e => app.Quit());
inputSystem.Keyboard
    .NoRepeat()
    .IsPressed()
    .Where(e => e.Key == Key.Z)
    .Subscribe(e => showTexture = !showTexture);

// Define update function
var lastTime = app.Elapsed;
app.OnUpdate = () =>
{
    var elapsed = app.Elapsed;

    // Convert milliseconds to seconds
    var deltaSeconds = (float) (elapsed - lastTime) / 1000;
    triangle.Update(deltaSeconds);

    // Draw stuff
    renderer.DrawColor = Color.Black;
    renderer.Clear();

    if (showTexture)
        renderer.CopyTexture(texture);
    else
        triangle.Draw(renderer);

    renderer.Present();

    lastTime = elapsed;
};

// Do some kind of cleanup at app exit like stopping playing music
app.OnExit = () => { Console.WriteLine("exiting composed app..."); };

// Knock down the dominoes
app.Run();