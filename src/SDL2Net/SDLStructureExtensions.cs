using System.Drawing;
using SDL2Net.Internal;

namespace SDL2Net
{
    internal static class SDLStructureExtensions
    {
        public static Point ToPoint(this SDL_Point point) => new Point(point.x, point.y);

        public static SDL_Point ToSdlPoint(this Point point) => new SDL_Point {x = point.X, y = point.Y};
        
        public static Rectangle ToRectangle(this SDL_Rect rect) => new Rectangle(rect.x, rect.y, rect.w, rect.h);

        public static SDL_Rect ToSdlRect(this Rectangle rect) => new SDL_Rect
            {x = rect.X, y = rect.Y, w = rect.Width, h = rect.Height};
    }
}