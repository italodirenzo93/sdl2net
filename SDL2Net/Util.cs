using System;
using System.Runtime.InteropServices;

namespace SDL2Net
{
    internal enum Platform
    {
        Unknown,
        Windows,
        Linux,
        MacOS
    }

    internal static class Util
    {
        private const int RTLD_LAZY = 0x0001;

        private static Platform? _currentPlatform;

        internal static Platform CurrentPlatform
        {
            get
            {
                if (_currentPlatform == null)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        _currentPlatform = Platform.Windows;
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        _currentPlatform = Platform.Linux;
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        _currentPlatform = Platform.MacOS;
                    else
                        _currentPlatform = Platform.Unknown;
                }

                return (Platform) _currentPlatform;
            }
        }

        internal static IntPtr LoadLibrary(string name) => CurrentPlatform switch
        {
            Platform.Windows => Windows.LoadLibraryW(name),
            Platform.Linux => Linux.dlopen(name, RTLD_LAZY),
            Platform.MacOS => MacOS.dlopen(name, RTLD_LAZY),
            _ => throw new NotSupportedException(RuntimeInformation.OSDescription)
        };

        internal static TDelegate LoadFunction<TDelegate>(IntPtr library, string functionName)
        {
            var procAddress = CurrentPlatform switch
            {
                Platform.Windows => Windows.GetProcAddress(library, functionName),
                Platform.Linux => Linux.dlsym(library, functionName),
                Platform.MacOS => MacOS.dlsym(library, functionName),
                _ => throw new NotSupportedException(RuntimeInformation.OSDescription)
            };

            return Marshal.GetDelegateForFunctionPointer<TDelegate>(procAddress);
        }

        internal static void ThrowIfFailed(int status)
        {
            if (status != 0) throw new SDLException();
        }

        internal static void ThrowIfFailed(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) throw new SDLException();
        }

        private static class Windows
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadLibraryW(string lpszLib);
        }

        private static class MacOS
        {
            [DllImport("/usr/lib/libSystem.dylib")]
            public static extern IntPtr dlopen(string path, int flags);

            [DllImport("/usr/lib/libSystem.dylib")]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);
        }

        private static class Linux
        {
            [DllImport("libdl.so.2")]
            public static extern IntPtr dlopen(string path, int flags);

            [DllImport("libdl.so.2")]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);
        }
    }
}