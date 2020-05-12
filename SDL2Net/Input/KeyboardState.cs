namespace SDL2Net.Input
{
    public readonly struct KeyboardState
    {
        private readonly int[] _keys;
        
        public KeyboardState(int[] keys)
        {
            _keys = keys;
        }

        public bool IsKeyDown(int key) => _keys[key] == 1;
    }
}