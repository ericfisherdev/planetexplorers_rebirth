// Stub: Steamworks.Callback<T> - Steam callback registration
// Create() returns a no-op callback instance that holds the delegate but never fires.

using System;

namespace Steamworks
{
    public class Callback<T>
    {
        private Action<T> _handler;

        private Callback() { }

        /// <summary>Create a callback listener. The handler is stored but never invoked in stub mode.</summary>
        public static Callback<T> Create(Action<T> handler)
        {
            return new Callback<T> { _handler = handler };
        }

        /// <summary>Dispose the callback. No-op in stub mode.</summary>
        public void Dispose() { _handler = null; }
    }
}
