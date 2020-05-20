namespace SDL2Net.Input.Events
{
    public class MouseButtonEvent : MouseEvent
    {
        public MouseButton Button { get; set; }

        public ButtonState ButtonState { get; set; }

        public int Clicks { get; set; }
    }
}