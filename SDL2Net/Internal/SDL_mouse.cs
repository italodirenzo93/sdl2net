using System.Runtime.InteropServices;
using SDL2Net.Utilities;

namespace SDL2Net.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_MouseMoveEvent
    {
        public SDL_EventType type;
        public uint timestamp, windowId, which, state;
        public int x, y, xrel, yrel;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_MouseButtonEvent
    {
        public SDL_EventType type;
        public uint timestamp, windowId, which;
        public byte button, state, clicks;
        public int x, y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SDL_MouseWheelEvent
    {
        public SDL_EventType type;
        public uint timestamp, windowId, which;
        public int x, y;
        public uint direction;
    }

    internal static partial class SDL
    {
        public delegate int SDL_GetMouseState(out int x, out int y);

        public delegate int SDL_ShowCursor(int toggle);

        public static readonly SDL_GetMouseState GetMouseState =
            Util.LoadFunction<SDL_GetMouseState>(NativeLibrary, nameof(SDL_GetMouseState));

        public static readonly SDL_ShowCursor ShowCursor =
            Util.LoadFunction<SDL_ShowCursor>(NativeLibrary, nameof(SDL_ShowCursor));
    }
}