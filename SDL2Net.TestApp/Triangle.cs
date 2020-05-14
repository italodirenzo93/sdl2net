using System.Drawing;
using SDL2Net.Input;
using SDL2Net.Video;

namespace SDL2Net.TestApp
{
    public class Triangle
    {
        private const float Speed = 125f;

        public Triangle()
        {
        }

        public Triangle(float x, float y) : this(new Vector2(x, y))
        {
        }

        public Triangle(Vector2 position)
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
            var speedFactor = deltaTime * Speed;

            var dx = x;
            var dy = y;

            if (state.IsKeyDown((int) Key.Left)) dx = x - speedFactor;

            if (state.IsKeyDown((int) Key.Right)) dx = x + speedFactor;

            if (state.IsKeyDown((int) Key.Up)) dy = y - speedFactor;

            if (state.IsKeyDown((int) Key.Down)) dy = y + speedFactor;

            Position = new Vector2(dx, dy);
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawColor = Color;
            renderer.DrawLines(Points);
        }
    }
}