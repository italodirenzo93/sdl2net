
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using SDL2Net.Input.Events;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_scancode;

namespace SDL2Net.Input
{
    public static class Keyboard
    {
        internal static readonly Subject<KeypressEvent> Subject = new Subject<KeypressEvent>();

        public static IObservable<KeypressEvent> Keypresses => Subject.AsObservable();

        public static KeyboardState GetState()
        {
            var state = SDL_GetKeyboardState(out _);
            const int arraySize = (int) SDL_NUM_SCANCODES;
            var keys = new int[arraySize];
            Marshal.Copy(state, keys, 0, arraySize);
            return new KeyboardState(keys);
        }
    }
}