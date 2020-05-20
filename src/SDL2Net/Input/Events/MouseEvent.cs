using SDL2Net.Events;

namespace SDL2Net.Input.Events
{
    public abstract class MouseEvent : Event
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}