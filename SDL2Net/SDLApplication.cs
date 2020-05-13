using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using SDL2Net.Input;
using SDL2Net.Input.Events;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_InitFlags;
using static SDL2Net.Util;

namespace SDL2Net
{
    public abstract class SDLApplication : IDisposable
    {
        private bool _running = true;
        private readonly Subject<string> _events = new Subject<string>(); 

        protected SDLApplication()
        {
            var status = SDL_Init(SDL_INIT_EVERYTHING);
            ThrowIfFailed(status);
        }

        public IObservable<string> Events => _events.AsObservable();

        protected virtual void Initialize() { }
        
        protected virtual void Update(uint elapsed) { }

        public void Run()
        {
            Initialize();
            var @event = new SDL_Event();
            while (_running)
            {
                var elapsed = SDL_GetTicks();
                Update(elapsed);
                HandleEvent(ref @event);
            }
        }

        private void HandleEvent(ref SDL_Event @event)
        {
            while (SDL_PollEvent(ref @event) != 0)
            {
                switch (@event.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        _events.OnCompleted();
                        _running = false;
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        Keyboard.Subject.OnNext(new KeypressEvent
                        {
                            Key = (int) @event.key.keysym.scancode,
                            IsRepeat = @event.key.repeat == 1
                        });
                        break;
                }
            }
        }

        public virtual void Dispose()
        {
            _events.Dispose();
            SDL_Quit();
        }
    }
}