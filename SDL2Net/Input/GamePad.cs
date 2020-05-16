using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using SDL2Net.Events;
using SDL2Net.Input.Events;
using SDL2Net.Internal;

namespace SDL2Net.Input
{
    public class GamePad : IDisposable, IObservable<GamePadEvent>
    {
        internal readonly IntPtr GamePadPtr;

        public GamePad(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Util.ThrowIfFailed(SDL.InitSubSystem(SDL_InitFlags.SDL_INIT_GAMECONTROLLER));
            GamePadPtr = SDL.GameControllerOpen(playerIndex);
            Util.ThrowIfFailed(GamePadPtr);
        }

        public static IObservable<GamePadEvent> Events => Event.Subject.OfType<GamePadEvent>().AsObservable();
        
        public int PlayerIndex { get; }

        public bool IsButtonDown(GamePadButton button)
        {
            return SDL.GameControllerGetButton(GamePadPtr, button) == 1;
        }
        
        public IDisposable Subscribe(IObserver<GamePadEvent> observer)
        {
            return Events.Where(e => e.PlayerIndex == PlayerIndex).Subscribe(observer);
        }

        #region IDisposable Support
        
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
            }
            SDL.GameControllerClose(GamePadPtr);
            SDL.QuitSubSystem(SDL_InitFlags.SDL_INIT_GAMECONTROLLER);
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~GamePad()
        {
            Dispose(false);
        }

        #endregion
    }
}