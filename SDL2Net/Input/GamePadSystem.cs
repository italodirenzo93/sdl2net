using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SDL2Net.Input.Events;
using SDL2Net.Internal;
using SDL2Net.Utilities;

namespace SDL2Net.Input
{
    public class GamePadSystem : IDisposable
    {
        private readonly SDLApplication _app;
        private readonly HashSet<GamePad> _gamepads = new HashSet<GamePad>();

        public GamePadSystem(SDLApplication app)
        {
            _app = app;

            if (SDL.InitSubSystem(SDL_InitFlags.SDL_INIT_GAMECONTROLLER) != 0)
                throw new SDLException();

            var numConnectedPads = SDL.NumJoysticks();
            for (var i = 0; i < numConnectedPads; i++) _gamepads.Add(new GamePad(this, i));

            Events.OfType<GamePadConnectionEvent>().Subscribe(GamePadAddedOrRemoved);
        }

        public int NumConnected => SDL.NumJoysticks();

        public IObservable<GamePad> ConnectedPads => _gamepads.ToObservable();

        public IObservable<GamePadEvent> Events => _app.OfType<GamePadEvent>();

        private void GamePadAddedOrRemoved(GamePadConnectionEvent e)
        {
            if (e.Connection == GamePadConnection.Connected)
            {
                _gamepads.Add(new GamePad(this, e.PlayerIndex));
            }
            else
            {
                var pad = _gamepads.First(p => p.PlayerIndex == e.PlayerIndex);
                pad.Dispose();
                _gamepads.Remove(pad);
            }
        }

        #region IDisposable Support

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Util.OutputDebugString("Disposing {0}: disposing = {1}", nameof(GamePadSystem), disposing);

            if (_disposed) return;

            if (disposing)
                foreach (var pad in _gamepads)
                    pad.Dispose();

            SDL.QuitSubSystem(SDL_InitFlags.SDL_INIT_GAMECONTROLLER);

            _disposed = true;
        }

        ~GamePadSystem()
        {
            Dispose(false);
        }

        #endregion
    }
}