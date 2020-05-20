namespace SDL2Net.Input.Events
{
    public class MouseMoveEvent : MouseEvent
    {
        public int RelativeX { get; set; }

        public int RelativeY { get; set; }
    }
}