using System;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using SDL2Net.Input.Events;
using SDL2Net.Internal;
using SDL2Net.Utilities;

namespace SDL2Net.Input
{
    public class InputSystem : IDisposable
    {
        private readonly SDLApplication _app;

        public InputSystem(SDLApplication app)
        {
            _app = app;

            var result = SDL.InitSubSystem(SDL_InitFlags.SDL_INIT_EVENTS);
            if (result != 0) throw new SDLException();
        }

        public IObservable<KeyPressEvent> Keyboard => _app.OfType<KeyPressEvent>().AsObservable();

        public KeyboardState KeyboardState
        {
            get
            {
                var state = SDL.GetKeyboardState(out var numkeys);
                var keys = new byte[numkeys];
                Marshal.Copy(state, keys, 0, numkeys);
                return new KeyboardState(keys);
            }
        }

        public MouseState MouseState
        {
            get
            {
                var mask = SDL.GetMouseState(out var x, out var y);
                return new MouseState(x, y, mask);
            }
        }

        public bool ShowCursor
        {
            get => SDL.ShowCursor(-1) == 1;
            set => SDL.ShowCursor(value ? 1 : 0);
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            Util.OutputDebugString("Disposing {0}: disposing = {1}", nameof(InputSystem), disposing);
            if (_disposed) return;
            SDL.QuitSubSystem(SDL_InitFlags.SDL_INIT_EVENTS);
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~InputSystem()
        {
            Dispose(false);
        }

        #endregion
    }
}