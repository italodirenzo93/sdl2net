namespace SDL2Net.Input.Events
{
    public enum MouseWheelDirection
    {
        Normal,
        Flipped
    }

    public class MouseWheelEvent : MouseEvent
    {
        public MouseWheelDirection Direction { get; set; }
    }
}