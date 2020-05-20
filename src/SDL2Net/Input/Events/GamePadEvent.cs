using SDL2Net.Events;

namespace SDL2Net.Input.Events
{
    public abstract class GamePadEvent : Event
    {
        protected GamePadEvent(int playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public int PlayerIndex { get; }
    }
}