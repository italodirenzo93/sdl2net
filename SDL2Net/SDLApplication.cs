using System;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_InitFlags;
using static SDL2Net.Util;

namespace SDL2Net
{
    public abstract class SDLApplication : IDisposable
    {
        private bool _running = true;

        protected SDLApplication()
        {
            var status = SDL_Init(SDL_INIT_EVERYTHING);
            ThrowIfFailed(status);
        }

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
                        _running = false;
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        OnKey(@event.key);
                        break;
                }
            }
        }

        private void OnKey(SDL_KeyboardEvent keyboardEvent)
        {
            Console.WriteLine("{0} key pressed!", keyboardEvent.keysym.scancode);
        }
        
        public virtual void Dispose()
        {
            SDL_Quit();
        }
    }
}