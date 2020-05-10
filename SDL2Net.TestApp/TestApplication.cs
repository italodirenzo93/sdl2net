namespace SDL2Net.TestApp
{
    public class TestApplication : SDLApplication
    {
        private readonly SDLWindow _window = new SDLWindow("Hello!", 100, 100, 800, 600);

        protected override void Initialize()
        {
            _window.Show();
        }

        protected override void Update(uint elapsed)
        {
            _window.Title = $"Elapsed: {elapsed} ms";
        }

        public override void Dispose()
        {
            _window.Dispose();
            base.Dispose();
        }

        // public static void Main()
        // {
        //     using var app = new TestApplication();
        //     app.Run();
        // }
    }
}