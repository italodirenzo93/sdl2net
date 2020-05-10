using System.Runtime.InteropServices;

namespace SDL2Net.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Rect
    {
        public int x, y;
        public int w, h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_FRect
    {
        public float x;
        public float y;
        public float w;
        public float h;
    }
}
