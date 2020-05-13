using System;

namespace SDL2Net.Internal
{
    [Flags]
    internal enum SDL_WindowFlags : uint
    {
        SDL_WINDOW_FULLSCREEN = 0x00000001,

        /**< fullscreen window */
        SDL_WINDOW_OPENGL = 0x00000002,

        /**< window usable with OpenGL context */
        SDL_WINDOW_SHOWN = 0x00000004,

        /**< window is visible */
        SDL_WINDOW_HIDDEN = 0x00000008,

        /**< window is not visible */
        SDL_WINDOW_BORDERLESS = 0x00000010,

        /**< no window decoration */
        SDL_WINDOW_RESIZABLE = 0x00000020,

        /**< window can be resized */
        SDL_WINDOW_MINIMIZED = 0x00000040,

        /**< window is minimized */
        SDL_WINDOW_MAXIMIZED = 0x00000080,

        /**< window is maximized */
        SDL_WINDOW_INPUT_GRABBED = 0x00000100,

        /**< window has grabbed input focus */
        SDL_WINDOW_INPUT_FOCUS = 0x00000200,

        /**< window has input focus */
        SDL_WINDOW_MOUSE_FOCUS = 0x00000400,

        /**< window has mouse focus */
        SDL_WINDOW_FULLSCREEN_DESKTOP = (SDL_WINDOW_FULLSCREEN | 0x00001000),
        SDL_WINDOW_FOREIGN = 0x00000800,

        /**< window not created by SDL */
        SDL_WINDOW_ALLOW_HIGHDPI = 0x00002000,

        /**< window should be created in high-DPI mode if supported.
                                                     On macOS NSHighResolutionCapable must be set true in the
                                                     application's Info.plist for this to have any effect. */
        SDL_WINDOW_MOUSE_CAPTURE = 0x00004000,

        /**< window has mouse captured (unrelated to INPUT_GRABBED) */
        SDL_WINDOW_ALWAYS_ON_TOP = 0x00008000,

        /**< window should always be above others */
        SDL_WINDOW_SKIP_TASKBAR = 0x00010000,

        /**< window should not be added to the taskbar */
        SDL_WINDOW_UTILITY = 0x00020000,

        /**< window should be treated as a utility window */
        SDL_WINDOW_TOOLTIP = 0x00040000,

        /**< window should be treated as a tooltip */
        SDL_WINDOW_POPUP_MENU = 0x00080000,

        /**< window should be treated as a popup menu */
        SDL_WINDOW_VULKAN = 0x10000000 /**< window usable for Vulkan surface */
    }

    [Flags]
    internal enum SDL_MessageBoxFlags : uint
    {
        SDL_MESSAGEBOX_ERROR = 0x00000010,

        /**< error dialog */
        SDL_MESSAGEBOX_WARNING = 0x00000020,

        /**< warning dialog */
        SDL_MESSAGEBOX_INFORMATION = 0x00000040,

        /**< informational dialog */
        SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT = 0x00000080,

        /**< buttons placed left to right */
        SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT = 0x00000100 /**< buttons placed right to left */
    }

    internal static partial class SDL
    {
        public delegate IntPtr SDL_CreateWindow(string title, int x, int y, int w, int h, SDL_WindowFlags flags);

        public static readonly SDL_CreateWindow CreateWindow =
            Util.LoadFunction<SDL_CreateWindow>(NativeLibrary, nameof(SDL_CreateWindow));

        public delegate void SDL_DestroyWindow(IntPtr window);

        public static readonly SDL_DestroyWindow DestroyWindow =
            Util.LoadFunction<SDL_DestroyWindow>(NativeLibrary, nameof(SDL_DestroyWindow));

        public delegate void SDL_ShowWindow(IntPtr window);

        public static readonly SDL_ShowWindow ShowWindow =
            Util.LoadFunction<SDL_ShowWindow>(NativeLibrary, nameof(SDL_ShowWindow));

        public delegate void SDL_HideWindow(IntPtr window);

        public static readonly SDL_HideWindow HideWindow =
            Util.LoadFunction<SDL_HideWindow>(NativeLibrary, nameof(SDL_HideWindow));

        public delegate IntPtr SDL_GetWindowTitle(IntPtr window);

        public static readonly SDL_GetWindowTitle GetWindowTitle =
            Util.LoadFunction<SDL_GetWindowTitle>(NativeLibrary, nameof(SDL_GetWindowTitle));

        public delegate void SDL_SetWindowTitle(IntPtr window, string title);

        public static readonly SDL_SetWindowTitle SetWindowTitle =
            Util.LoadFunction<SDL_SetWindowTitle>(NativeLibrary, nameof(SDL_SetWindowTitle));

        public delegate void SDL_GetWindowPosition(IntPtr window, out int x, out int y);

        public static readonly SDL_GetWindowPosition GetWindowPosition =
            Util.LoadFunction<SDL_GetWindowPosition>(NativeLibrary, nameof(SDL_GetWindowPosition));

        public delegate void SDL_SetWindowPosition(IntPtr window, int x, int y);

        public static readonly SDL_SetWindowPosition SetWindowPosition =
            Util.LoadFunction<SDL_SetWindowPosition>(NativeLibrary, nameof(SDL_SetWindowPosition));

        public delegate void SDL_GetWindowSize(IntPtr window, out int x, out int y);

        public static readonly SDL_GetWindowSize GetWindowSize =
            Util.LoadFunction<SDL_GetWindowSize>(NativeLibrary, nameof(SDL_GetWindowSize));

        public delegate void SDL_SetWindowSize(IntPtr window, int x, int y);

        public static readonly SDL_SetWindowSize SetWindowSize =
            Util.LoadFunction<SDL_SetWindowSize>(NativeLibrary, nameof(SDL_SetWindowPosition));

        public delegate SDL_WindowFlags SDL_GetWindowFlags(IntPtr window);

        public static readonly SDL_GetWindowFlags GetWindowFlags =
            Util.LoadFunction<SDL_GetWindowFlags>(NativeLibrary, nameof(SDL_GetWindowFlags));

        public delegate void SDL_SetWindowResizable(IntPtr window, int resizable);

        public static readonly SDL_SetWindowResizable SetWindowResizable =
            Util.LoadFunction<SDL_SetWindowResizable>(NativeLibrary, nameof(SDL_SetWindowResizable));

        public delegate int SDL_ShowSimpleMessageBox(SDL_MessageBoxFlags flags, string title, string message,
            IntPtr window);

        public static readonly SDL_ShowSimpleMessageBox ShowSimpleMessageBox =
            Util.LoadFunction<SDL_ShowSimpleMessageBox>(NativeLibrary, nameof(SDL_ShowSimpleMessageBox));
    }
}