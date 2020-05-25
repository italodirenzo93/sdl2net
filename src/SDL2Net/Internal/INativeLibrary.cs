namespace SDL2Net.Internal
{
    /// <summary>
    /// Internal use only.
    /// </summary>
    public interface INativeLibrary
    {
        TDelegate GetFunction<TDelegate>();
        TDelegate GetFunction<TDelegate>(string name);
    }
}