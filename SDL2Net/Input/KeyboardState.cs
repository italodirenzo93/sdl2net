namespace SDL2Net.Input
{
    public readonly struct KeyboardState
    {
        private readonly byte[] _keys;

        public KeyboardState(byte[] keys)
        {
            _keys = keys;
        }

        public bool IsKeyDown(int key)
        {
            return _keys[key] == 1;
        }
    }
}