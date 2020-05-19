using System.Drawing;

namespace SDL2Net.Input
{
    public enum MouseButton
    {
        Left = 1,
        Middle,
        Right,
        X1,
        X2
    }

    public readonly struct MouseState
    {
        private readonly int _bitmask;

        public MouseState(int x, int y, int bitmask)
        {
            _bitmask = bitmask;
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public Point Position => new Point(X, Y);

        public bool IsButtonDown(MouseButton button)
        {
            // See SDL_mouse.h -> SDL_BUTTON macro
            var buttonMask = 1 << ((int) button - 1);
            return (_bitmask & buttonMask) > 0;
        }
    }
}