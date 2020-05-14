using System.Drawing;

namespace SDL2Net.TestApp
{
    public struct Vector2
    {
        public readonly float X;
        public readonly float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Point ToPoint()
        {
            return new Point((int) X, (int) Y);
        }
    }
}