using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using SDL2Net.Input;
using SDL2Net.Input.Events;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL_InitFlags;
using static SDL2Net.Util;

namespace SDL2Net
{
    public abstract class SDLApplication : IDisposable
    {
        private readonly Subject<string> _events = new Subject<string>();
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private bool _running = true;

        protected SDLApplication()
        {
            var status = SDL.Init(SDL_INIT_VIDEO);
            ThrowIfFailed(status);
        }

        public virtual void Dispose()
        {
            _events.Dispose();
            SDL.Quit();
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
            _events.OnCompleted();
            _running = false;
        }

        /// <summary>
        ///     Starts the main application loop. This should only be called once in your program.
        /// </summary>
        public void Run()
        {
            Initialize();
            var @event = new SDL_Event();
            _stopwatch.Start();
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
                switch (@event.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Quit();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        Keyboard.Subject.OnNext(new KeyPressEvent
                        {
                            Key = @event.key.keysym.scancode,
                            IsRepeat = @event.key.repeat == 1
                        });
                        break;
                }
        }
    }
}