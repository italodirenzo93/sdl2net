using System;
using System.Reactive.Subjects;

namespace SDL2Net.Events
{
    public abstract class Event
    {
        internal static readonly Subject<Event> Subject = new Subject<Event>();
        
        public DateTimeOffset Timestamp { get; }  = DateTimeOffset.UtcNow;
    }
}