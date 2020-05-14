using System;
using SDL2Net.Internal;

namespace SDL2Net.Input
{
    public class GamePad : IDisposable
    {
        internal readonly IntPtr GamePadPtr;

        public GamePad(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Util.ThrowIfFailed(SDL.InitSubSystem(SDL_InitFlags.SDL_INIT_GAMECONTROLLER));
            GamePadPtr = SDL.GameControllerOpen(playerIndex);
            Util.ThrowIfFailed(GamePadPtr);
        }

        public int PlayerIndex { get; }

        public void Dispose()
        {
            SDL.GameControllerClose(GamePadPtr);
            SDL.QuitSubSystem(SDL_InitFlags.SDL_INIT_GAMECONTROLLER);
        }

        public bool IsButtonDown(GamePadButton button)
        {
            return SDL.GameControllerGetButton(GamePadPtr, button) == 1;
        }
    }
}