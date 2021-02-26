using System;
using System.Reactive.Linq;
using SDL2Net.Input.Events;

namespace SDL2Net.Input
{
    public static class InputSystemExtensions
    {
        public static IObservable<TEvent> NoRepeat<TEvent>(this IObservable<TEvent> observable)
            where TEvent : KeyPressEvent
        {
            return observable.Where(e => !e.IsRepeat);
        }

        public static IObservable<TEvent> IsPressed<TEvent>(this IObservable<TEvent> observable, bool pressed = true)
            where TEvent : KeyPressEvent
        {
            var buttonState = pressed ? ButtonState.Pressed : ButtonState.Released;
            return observable.Where(e => e.KeyState == buttonState);
        }

        public static IObservable<TEvent> ForPlayer<TEvent>(this IObservable<TEvent> observable, int playerIndex)
            where TEvent : GamePadEvent
        {
            return observable.Where(e => e.PlayerIndex == playerIndex);
        }
    }
}