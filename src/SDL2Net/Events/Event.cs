using System;

namespace SDL2Net.Events
{
    public abstract class Event
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
    }
}