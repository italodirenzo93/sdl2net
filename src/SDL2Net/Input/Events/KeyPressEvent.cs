using SDL2Net.Events;

namespace SDL2Net.Input.Events
{
    public class KeyPressEvent : Event
    {
        public Key Key { get; internal set; }
        public bool IsRepeat { get; internal set; }
        public ButtonState ButtonState { get; internal set; }
    }
}