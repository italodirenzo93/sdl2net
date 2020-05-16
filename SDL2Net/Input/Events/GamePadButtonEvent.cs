namespace SDL2Net.Input.Events
{
    public class GamePadButtonEvent : GamePadEvent
    {
        public GamePadButtonEvent(int playerIndex) : base(playerIndex)
        {
        }

        public GamePadButton Button { get; internal set; }

        public ButtonState ButtonState { get; internal set; }
    }
}