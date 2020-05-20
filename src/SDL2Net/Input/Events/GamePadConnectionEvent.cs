namespace SDL2Net.Input.Events
{
    public enum GamePadConnection
    {
        Connected,
        Disconnected
    }

    public class GamePadConnectionEvent : GamePadEvent
    {
        public GamePadConnectionEvent(int playerIndex, GamePadConnection connection) : base(playerIndex)
        {
            Connection = connection;
        }

        public GamePadConnection Connection { get; }
    }
}