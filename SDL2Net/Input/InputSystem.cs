using System;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using SDL2Net.Input.Events;
using SDL2Net.Internal;

namespace SDL2Net.Input
{
    public class InputSystem
    {
        private readonly SDLApplication _app;

        public InputSystem(SDLApplication app)
        {
            _app = app;
        }

        public IObservable<KeyPressEvent> Events => _app.Events.OfType<KeyPressEvent>().AsObservable();

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
    }
}