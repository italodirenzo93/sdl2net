using System;
using SDL2Net.Events;

namespace SDL2Net.Samples.Inheritance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var app = new TestApplication();
            app.Run();
        }

        private class MyObserver : IObserver<Event>
        {
            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(Event value)
            {
                throw new NotImplementedException();
            }
        }
    }
}