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

    /// <summary>
    ///     Snapshot of the current state of the mouse.
    /// </summary>
    public readonly struct MouseState
    {
        private readonly int _bitmask;

        public MouseState(int x, int y, int bitmask)
        {
            _bitmask = bitmask;
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Mouse X position relative to the application window.
        /// </summary>
        public int X { get; }

        /// <summary>
        ///     Mouse Y position relative to the application window.
        /// </summary>
        public int Y { get; }

        /// <summary>
        ///     Mouse position relative to the application window.
        /// </summary>
        public Point Position => new Point(X, Y);

        /// <summary>
        ///     Detect if a mouse button is currently pressed down.
        /// </summary>
        /// <param name="button">The mouse button to query</param>
        /// <returns>True if the button is currently down. False otherwise.</returns>
        public bool IsButtonDown(MouseButton button)
        {
            // See SDL_mouse.h -> SDL_BUTTON macro
            var buttonMask = 1 << ((int) button - 1);
            return (_bitmask & buttonMask) > 0;
        }
    }
}