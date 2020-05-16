using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using SDL2Net.Events;
using SDL2Net.Input.Events;
using SDL2Net.Internal;

namespace SDL2Net.Input
{
    public static class Keyboard
    {
        public static IObservable<KeyPressEvent> Events => Event.Subject.OfType<KeyPressEvent>().AsObservable();

        public static KeyboardState GetState()
        {
            var state = SDL.GetKeyboardState(out var numkeys);
            var keys = new byte[numkeys];
            Marshal.Copy(state, keys, 0, numkeys);
            return new KeyboardState(keys);
        }
    }
}