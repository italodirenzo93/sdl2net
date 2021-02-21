using System;
using System.Runtime.InteropServices;

namespace SDL2Net.Utilities
{
    internal class StructPtr<T> : IDisposable where T : struct
    {
        private bool _disposed;

        public StructPtr(T? structure = null)
        {
            if (structure.HasValue)
            {
                IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
                Marshal.StructureToPtr(structure, IntPtr, false);
            }
        }

        public IntPtr IntPtr { get; } = IntPtr.Zero;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~StructPtr()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (IntPtr != IntPtr.Zero) Marshal.FreeHGlobal(IntPtr);

            _disposed = true;
        }

        public static explicit operator IntPtr(StructPtr<T> sp) => sp.IntPtr;
    }
}