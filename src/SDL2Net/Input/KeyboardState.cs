using System.Collections.Generic;
using System.Linq;

namespace SDL2Net.Input
{
    /// <summary>
    ///     Snapshot of the current state of the keyboard.
    /// </summary>
    public readonly struct KeyboardState
    {
        private readonly byte[] _keys;

        public KeyboardState(byte[] keys)
        {
            _keys = keys;
        }

        /// <summary>
        ///     Detect if the provided key is currently pressed down on the keyboard.
        /// </summary>
        /// <param name="key">The key to query</param>
        /// <returns>True if the key is currently down. False otherwise</returns>
        public bool IsKeyDown(Key key)
        {
            return _keys[(int) key] == 1;
        }

        /// <summary>
        ///     Detect if the specified keys are currently pressed down on the keyboard.
        /// </summary>
        /// <param name="keys">A selection of keys to query</param>
        /// <returns>A sequence of keys from the source enumerable that are down</returns>
        public IEnumerable<Key> IsKeyDown(IEnumerable<Key> keys)
        {
            return keys.Intersect(_keys.Where(k => k == 1).Cast<Key>());
        }
    }
}