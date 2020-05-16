using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
        private static bool _instantiated;
        private static readonly Subject<Event> Subject = new Subject<Event>();
        
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private bool _running = true;

        protected SDLApplication()
        {
            // This is kind of hack to get around the problem needing to be able to access
            // the application instance statically internally, but not being able to do a
            // singleton pattern because this class is abstract
            if (_instantiated)
                throw new InvalidOperationException($"Only one instance of {nameof(SDLApplication)} may exist at a time");
            var status = SDL.Init(SDL_INIT_EVENTS);
            ThrowIfFailed(status);
            _instantiated = true;
        }

        internal static IObservable<Event> Events => Subject.AsObservable();

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
            Subject.OnCompleted();
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
                        Subject.OnNext(new KeyPressEvent
                        {
                            Key = @event.key.keysym.scancode,
                            IsRepeat = @event.key.repeat == 1,
                            ButtonState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_KEYUP:
                        Subject.OnNext(new KeyPressEvent
                        {
                            Key = @event.key.keysym.scancode,
                            IsRepeat = @event.key.repeat == 1,
                            ButtonState = ButtonState.Released
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        Subject.OnNext(new GamePadButtonEvent(@event.cbutton.which)
                        {
                            Button = GamePadButton.A,
                            ButtonState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        Subject.OnNext(new GamePadButtonEvent(@event.cbutton.which)
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
            
            if (disposing)
            {
                Subject.Dispose();
            }
            
            SDL.Quit();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            _instantiated = false;
        }

        ~SDLApplication()
        {
            Dispose(false);
        }
        
        #endregion
    }
}