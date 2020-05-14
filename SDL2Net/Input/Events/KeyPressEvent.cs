namespace SDL2Net.Input.Events
{
    public class KeyPressEvent
    {
        public Key Key { get; internal set; }
        public bool IsRepeat { get; internal set; }
    }
}