using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using SDL2Net.Events;
using SDL2Net.Input;
using SDL2Net.Input.Events;
using SDL2Net.Internal;
using static SDL2Net.Utilities.Util;

namespace SDL2Net
{
    /// <summary>
    ///     SDL Application context. https://wiki.libsdl.org/CategoryInit
    /// </summary>
    public class SDLApplication : IDisposable, IObservable<Event>
    {
        private static bool _instantiated;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly Subject<Event> _subject = new Subject<Event>();
        private bool _running = true;

        /// <summary>
        ///     Initializes SDL and encapsulates the main runtime loop.
        /// </summary>
        /// <exception cref="InvalidOperationException">More than one <see cref="SDLApplication" /> exist at a time</exception>
        public SDLApplication()
        {
            // This is kind of hack to get around the problem needing to be able to access
            // the application instance statically internally, but not being able to do a
            // singleton pattern because this class is abstract
            if (_instantiated)
                throw new InvalidOperationException(
                    $"Only one instance of {nameof(SDLApplication)} may exist at a time");
            var status = SDL.Init(0);
            ThrowIfFailed(status);
            _instantiated = true;
        }

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

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            return _subject.Subscribe(observer);
        }

        /// <summary>
        ///     Signals the game loop to exit.
        /// </summary>
        public void Quit()
        {
            _subject.OnCompleted();
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
            var ev = new SDL_Event();
            while (SDL.PollEvent(ref ev) != 0)
                switch (ev.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Quit();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        _subject.OnNext(new KeyPressEvent
                        {
                            Key = ev.key.keysym.scancode,
                            IsRepeat = ev.key.repeat == 1,
                            KeyState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_KEYUP:
                        _subject.OnNext(new KeyPressEvent
                        {
                            Key = ev.key.keysym.scancode,
                            IsRepeat = ev.key.repeat == 1,
                            KeyState = ButtonState.Released
                        });
                        break;
                    case SDL_EventType.SDL_MOUSEMOTION:
                        _subject.OnNext(new MouseMoveEvent
                        {
                            X = ev.motion.x,
                            Y = ev.motion.y,
                            RelativeX = ev.motion.xrel,
                            RelativeY = ev.motion.yrel
                        });
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONUP:
                        _subject.OnNext(new MouseButtonEvent
                        {
                            Button = (MouseButton) ev.button.button,
                            ButtonState = ButtonState.Released,
                            Clicks = ev.button.clicks,
                            X = ev.button.x,
                            Y = ev.button.y
                        });
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        _subject.OnNext(new MouseButtonEvent
                        {
                            Button = (MouseButton) ev.button.button,
                            ButtonState = ButtonState.Pressed,
                            Clicks = ev.button.clicks,
                            X = ev.button.x,
                            Y = ev.button.y
                        });
                        break;
                    case SDL_EventType.SDL_MOUSEWHEEL:
                        _subject.OnNext(new MouseWheelEvent
                        {
                            X = ev.wheel.x,
                            Y = ev.wheel.y,
                            Direction = ev.wheel.direction == 1
                                ? MouseWheelDirection.Flipped
                                : MouseWheelDirection.Normal
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        _subject.OnNext(new GamePadButtonEvent(ev.cbutton.which)
                        {
                            Button = ev.cbutton.button,
                            ButtonState = ButtonState.Pressed
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        _subject.OnNext(new GamePadButtonEvent(ev.cbutton.which)
                        {
                            Button = ev.cbutton.button,
                            ButtonState = ButtonState.Released
                        });
                        break;
                    case SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                        _subject.OnNext(new GamePadConnectionEvent(ev.cdevice.which, GamePadConnection.Connected));
                        break;
                    case SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                        _subject.OnNext(new GamePadConnectionEvent(ev.cdevice.which,
                            GamePadConnection.Disconnected));
                        break;
                }
        }

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            OutputDebugString("Disposing {0}: disposing = {1}", nameof(SDLApplication), disposing);

            if (_disposed) return;

            if (disposing) _subject.Dispose();

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