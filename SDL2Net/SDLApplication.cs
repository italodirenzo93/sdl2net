using System;
using SDL2Net.Internal;
using static SDL2Net.Internal.SDL;
using static SDL2Net.Internal.SDL_InitFlags;

namespace SDL2Net
{
    public abstract class SDLApplication : IDisposable
    {
        private bool _running = true;

        protected SDLApplication()
        {
            var result = SDL_Init(SDL_INIT_EVERYTHING);
            if (result != 0)
            {
                throw new SDLException();
            }
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
                if (@event.type == SDL_EventType.SDL_QUIT) _running = false;   
            }
        }
        
        public virtual void Dispose()
        {
            SDL_Quit();
        }
    }
}