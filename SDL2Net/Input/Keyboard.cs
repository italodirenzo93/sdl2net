using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using SDL2Net.Input.Events;
using SDL2Net.Internal;

namespace SDL2Net.Input
{
    public static class Keyboard
    {
        internal static readonly Subject<KeypressEvent> Subject = new Subject<KeypressEvent>();

        public static IObservable<KeypressEvent> Keypresses => Subject.AsObservable();

        public static KeyboardState GetState()
        {
            var state = SDL.GetKeyboardState(out _);
            const int arraySize = (int) SDL_scancode.SDL_NUM_SCANCODES;
            var keys = new int[arraySize];
            Marshal.Copy(state, keys, 0, arraySize);
            return new KeyboardState(keys);
        }
    }
}