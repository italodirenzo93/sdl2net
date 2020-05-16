using System;
using System.Diagnostics;
using System.Reactive;
using SDL2Net.Events;
using SDL2Net.Input;
using SDL2Net.Input.Events;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL_InitFlags;
using static SDL2Net.Util;

namespace SDL2Net
{
    public abstract class SDLApplication : IDisposable
    {
        private static readonly IObserver<Event> Events = Event.Subject.AsObserver();
        
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private bool _running = true;

        protected SDLApplication()
        {
            var status = SDL.Init(SDL_INIT_EVENTS);
            ThrowIfFailed(status);
        }

        /// <summary>
        ///     Perform initialization logic that requires the framework to be initialized first (i.e. loading assets)
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        ///     Code to run on every iteration of the main application loop such as rendering graphics or animations.
        /// </summary>
        /// <param name="elapsed">The number of milliseconds elapsed since the application was initialized</param>
        protected virtual void Update(long elapsed)
        {
        }

        /// <summary>
        ///     Signals the game loop to exit.
        /// </summary>
        protected void Quit()
        {
            Events.OnCompleted();
            _running = false;
        }

        /// <summary>
        ///     Starts the main application loop. This should only be called once in your program.
        /// </summary>
        public void Run()
        {
            Initialize();
            _stopwatch.Start();
            var @event = new SDL_Event();
            while (_running)
            {
                // Interesting example below of getting the best of both worlds
                // var elapsed = SDL.GetTicks();
                Update(_stopwatch.ElapsedMilliseconds);
                HandleEvent(ref @event);
            }

            _stopwatch.Stop();
        }

        private void HandleEvent(ref SDL_Event @event)
        {
            while (SDL.PollEvent(ref @event) != 0)
            {
                switch (@event.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Quit();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        Events.OnNext(new KeyPressEvent
                        {
                            Key = @event.key.keysym.scancode,
                            IsRepeat = @event.key.repeat == 1,
                            ButtonState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_KEYUP:
                        Events.OnNext(new KeyPressEvent
                        {
                            Key = @event.key.keysym.scancode,
                            IsRepeat = @event.key.repeat == 1,
                            ButtonState = ButtonState.Released
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        Events.OnNext(new GamePadButtonEvent(@event.cbutton.which)
                        {
                            Button = GamePadButton.A,
                            ButtonState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        Events.OnNext(new GamePadButtonEvent(@event.cbutton.which)
                        {
                            Button = GamePadButton.A,
                            ButtonState = ButtonState.Released
                        });
                        break;
                }
            }
        }

        #region IDisposable Support
        
        private bool _disposed;
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            
            if (disposing && !Event.Subject.IsDisposed)
            {
                Event.Subject.Dispose();
            }
            
            SDL.Quit();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SDLApplication()
        {
            Dispose(false);
        }
        
        #endregion
    }
}