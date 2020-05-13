namespace SDL2Net.Input.Events
{
    public class KeypressEvent
    {
        public int Key { get; internal set; }
        public bool IsRepeat { get; internal set; }
    }
}