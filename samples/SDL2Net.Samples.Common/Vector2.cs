using System.Drawing;

namespace SDL2Net.Samples.Common
{
    public readonly struct Vector2
    {
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public float X { get; }
        public float Y { get; }

        public Point ToPoint()
        {
            return new Point((int) X, (int) Y);
        }
    }
}