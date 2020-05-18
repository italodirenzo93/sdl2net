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
    public class SDLApplication : IDisposable
    {
        private static bool _instantiated;
        private static readonly Subject<Event> Subject = new Subject<Event>();

        private readonly Stopwatch _stopwatch = new Stopwatch();
        private SDL_Event _event;
        private bool _running = true;

        public SDLApplication()
        {
            // This is kind of hack to get around the problem needing to be able to access
            // the application instance statically internally, but not being able to do a
            // singleton pattern because this class is abstract
            if (_instantiated)
                throw new InvalidOperationException(
                    $"Only one instance of {nameof(SDLApplication)} may exist at a time");
            var status = SDL.Init(SDL_INIT_EVENTS);
            ThrowIfFailed(status);
            _instantiated = true;
        }

        internal static IObservable<Event> Events => Subject.AsObservable();

        /// <summary>
        ///     The number of milliseconds elapsed since the application was initialized
        /// </summary>
        public long Elapsed => _stopwatch.ElapsedMilliseconds;

        /// <summary>
        ///     Perform initialization logic that requires the framework to be initialized first (i.e. loading assets)
        /// </summary>
        public Action? OnInitialize { get; set; }

        /// <summary>
        ///     Code to run on every iteration of the main application loop such as rendering graphics or animations.
        /// </summary>
        public Action? OnUpdate { get; set; }

        /// <summary>
        ///     Code to run execute right before the <see cref="Run" /> method returns.
        /// </summary>
        public Action? OnExit { get; set; }

        /// <summary>
        ///     Signals the game loop to exit.
        /// </summary>
        public void Quit()
        {
            Subject.OnCompleted();
            _running = false;
        }

        /// <summary>
        ///     Starts the main application loop. This should only be called once in your program.
        /// </summary>
        public void Run()
        {
            // Run initialization code
            OnInitialize?.Invoke();

            // Start game timer
            _stopwatch.Start();

            // Main loop
            while (_running)
            {
                // Interesting example below of getting the best of both worlds
                // var elapsed = SDL.GetTicks();
                OnUpdate?.Invoke();
                HandleEvent();
            }

            // Stop game timer
            _stopwatch.Stop();

            // Run cleanup code
            OnExit?.Invoke();
        }

        private void HandleEvent()
        {
            while (SDL.PollEvent(ref _event) != 0)
                switch (_event.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Quit();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        Subject.OnNext(new KeyPressEvent
                        {
                            Key = _event.key.keysym.scancode,
                            IsRepeat = _event.key.repeat == 1,
                            ButtonState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_KEYUP:
                        Subject.OnNext(new KeyPressEvent
                        {
                            Key = _event.key.keysym.scancode,
                            IsRepeat = _event.key.repeat == 1,
                            ButtonState = ButtonState.Released
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        Subject.OnNext(new GamePadButtonEvent(_event.cbutton.which)
                        {
                            Button = GamePadButton.A,
                            ButtonState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        Subject.OnNext(new GamePadButtonEvent(_event.cbutton.which)
                        {
                            Button = GamePadButton.A,
                            ButtonState = ButtonState.Released
                        });
                        break;
                }
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) Subject.Dispose();

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