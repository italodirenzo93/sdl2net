using System;
using System.Drawing;
using System.Reactive.Linq;
using SDL2Net.Input;
using SDL2Net.Input.Events;
using SDL2Net.Video;

namespace SDL2Net.Samples.Common
{
    public sealed class Triangle : IDisposable
    {
        private readonly InputSystem _inputSystem;
        private GamePad? _gamePad = null;
        private float _speed = 125f;

        public Triangle(InputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _inputSystem.Keyboard
                .Where(e => e.ButtonState == ButtonState.Pressed && !e.IsRepeat)
                .Subscribe(e =>
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

        public Triangle(float x, float y, InputSystem inputSystem) : this(new Vector2(x, y), inputSystem)
        {
        }

        public Triangle(Vector2 position, InputSystem inputSystem) : this(inputSystem)
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

        public void Dispose()
        {
            _gamePad?.Dispose();
        }

        public void Update(float deltaTime)
        {
            var speedFactor = deltaTime * _speed;
            var dx = Position.X;
            var dy = Position.Y;

            KeyboardMovement(ref dx, ref dy, speedFactor);
            GamePadMovement(ref dx, ref dy, speedFactor);
            //MouseMovement(ref dx, ref dy);

            Position = new Vector2(dx, dy);
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawColor = Color;
            renderer.DrawLines(Points);
        }

        private void KeyboardMovement(ref float dx, ref float dy, float speed)
        {
            // Read the keyboard for movement
            var x = Position.X;
            var y = Position.Y;
            var state = _inputSystem.KeyboardState;

            if (state.IsKeyDown(Key.Left)) dx = x - speed;

            if (state.IsKeyDown(Key.Right)) dx = x + speed;

            if (state.IsKeyDown(Key.Up)) dy = y - speed;

            if (state.IsKeyDown(Key.Down)) dy = y + speed;
        }

        private void MouseMovement(ref float dx, ref float dy)
        {
            var mouseState = _inputSystem.MouseState;
            if (mouseState.X == 0 && mouseState.Y == 0) return;

            dx = mouseState.X;
            dy = mouseState.Y;

            if (mouseState.IsButtonDown(MouseButton.Left))
                Color = Color.Magenta;
            if (mouseState.IsButtonDown(MouseButton.Right))
                Color = Color.GreenYellow;
            if (mouseState.IsButtonDown(MouseButton.Middle))
                Color = Color.Crimson;
        }

        private void GamePadMovement(ref float dx, ref float dy, float speed)
        {
            if (_gamePad == null) return;

            // Read the d-pad for movement
            var x = Position.X;
            var y = Position.Y;

            if (_gamePad.IsButtonDown(GamePadButton.DPadLeft)) dx = x - speed;

            if (_gamePad.IsButtonDown(GamePadButton.DPadRight)) dx = x + speed;

            if (_gamePad.IsButtonDown(GamePadButton.DPadUp)) dy = y - speed;

            if (_gamePad.IsButtonDown(GamePadButton.DPadDown)) dy = y + speed;
        }
    }
}