using System.Runtime.InteropServices;

namespace SDL2Net.Internal
{
    internal static partial class SDL
    {
        [DllImport(SDL2Lib)]
        public static extern uint SDL_GetTicks();
    }
}