using System;
using System.Drawing;
using SDL2Net.Input;
using SDL2Net.Video;

namespace SDL2Net.TestApp
{
    public sealed class Triangle
    {
        private readonly GamePad _gamePad = new GamePad(0);
        private float _speed = 125f;

        public Triangle()
        {
            Keyboard.KeyPresses.Subscribe(e =>
            {
                Color = e.Key switch
                {
                    Key.Home => Color.Gold,
                    Key.End => Color.Aqua,
                    _ => Color
                };

                _speed = e.Key switch
                {
                    Key.Pageup => _speed + 25f,
                    Key.Pagedown => _speed - 25f,
                    _ => _speed
                };
            });
        }

        public Triangle(float x, float y) : this(new Vector2(x, y))
        {
        }

        public Triangle(Vector2 position) : this()
        {
            Position = position;
        }

        public Vector2 Position { get; set; }

        public Color Color { get; set; } = Color.Lime;

        public int SizeFactor { get; set; } = 40;

        public Point[] Points
        {
            get
            {
                var x = (int) Position.X;
                var y = (int) Position.Y;
                return new[]
                {
                    new Point(x, y - SizeFactor), // top
                    new Point(x - SizeFactor, y + SizeFactor), // left 
                    new Point(x + SizeFactor, y + SizeFactor), // right
                    new Point(x, y - SizeFactor) // top again
                };
            }
        }

        public void Update(float deltaTime)
        {
            // Read the keyboard for movement
            var state = Keyboard.GetState();
            var x = Position.X;
            var y = Position.Y;
            var speedFactor = deltaTime * _speed;

            var dx = x;
            var dy = y;

            if (state.IsKeyDown((int) Key.Left) || _gamePad.IsButtonDown(GamePadButton.DPadLeft)) dx = x - speedFactor;

            if (state.IsKeyDown((int) Key.Right) || _gamePad.IsButtonDown(GamePadButton.DPadRight))
                dx = x + speedFactor;

            if (state.IsKeyDown((int) Key.Up) || _gamePad.IsButtonDown(GamePadButton.DPadUp)) dy = y - speedFactor;

            if (state.IsKeyDown((int) Key.Down) || _gamePad.IsButtonDown(GamePadButton.DPadDown)) dy = y + speedFactor;

            Position = new Vector2(dx, dy);
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawColor = Color;
            renderer.DrawLines(Points);
        }
    }
}