using System;
using SDL2Net.Input.Events;
using SDL2Net.Internal;
using SDL2Net.Utilities;

namespace SDL2Net.Input
{
    public class GamePad : IDisposable, IObservable<GamePadEvent>, IEquatable<GamePad>
    {
        private readonly GamePadSystem _system;
        internal readonly IntPtr GamePadPtr;

        public GamePad(GamePadSystem system, int playerIndex)
        {
            _system = system;
            PlayerIndex = playerIndex;
            GamePadPtr = SDL.GameControllerOpen(playerIndex);
            Util.ThrowIfFailed(GamePadPtr);
        }

        public int PlayerIndex { get; }

        public IDisposable Subscribe(IObserver<GamePadEvent> observer)
        {
            return _system.Events.Subscribe(observer);
        }

        public bool IsButtonDown(GamePadButton button)
        {
            return SDL.GameControllerGetButton(GamePadPtr, button) == 1;
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            Util.OutputDebugString("Disposing {0}: disposing = {1}", nameof(GamePad), disposing);

            if (_disposed) return;
            if (disposing)
            {
            }

            SDL.GameControllerClose(GamePadPtr);
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

        #region IEquatable Support

        public bool Equals(GamePad? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GamePadPtr.Equals(other.GamePadPtr) && PlayerIndex == other.PlayerIndex;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GamePad) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (GamePadPtr.GetHashCode() * 397) ^ PlayerIndex;
            }
        }

        #endregion
    }
}