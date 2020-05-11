using System.Drawing;

namespace SDL2Net.TestApp
{
    public class TestApplication : SDLApplication
    {
        private readonly SDLWindow _window;
        private readonly SDLRenderer _renderer;

        public TestApplication()
        {
            _window = new SDLWindow("Hello!", 100, 100, 800, 600);
            _renderer = new SDLRenderer(_window);
        }

        protected override void Update(uint elapsed)
        {
            _renderer.DrawColor = Color.Black;
            _renderer.Clear();
            _renderer.DrawColor = Color.Gold;
            _renderer.DrawLine(300, 400, 500, 100);
            _renderer.Present();
        }

        public override void Dispose()
        {
            _renderer.Dispose();
            _window.Dispose();
            base.Dispose();
        }

        public static void Main()
        {
            using var app = new TestApplication();
            app.Run();
        }
    }
}