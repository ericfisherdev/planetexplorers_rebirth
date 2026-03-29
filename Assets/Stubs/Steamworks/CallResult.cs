// Stub: Steamworks.CallResult<T> - Steam async call result handler
// Create() returns a no-op instance. Set() accepts a call handle but never fires the callback.

using System;

namespace Steamworks
{
    public class CallResult<T>
    {
        private Action<T, bool> _handler;

        private CallResult() { }

        /// <summary>Create a call result listener. The handler is stored but never invoked in stub mode.</summary>
        public static CallResult<T> Create(Action<T, bool> handler)
        {
            return new CallResult<T> { _handler = handler };
        }

        /// <summary>Associate this call result with a Steam API call. No-op in stub mode.</summary>
        public void Set(SteamAPICall_t hAPICall, Action<T, bool> handler = null)
        {
            if (handler != null) _handler = handler;
        }

        /// <summary>Dispose the call result. No-op in stub mode.</summary>
        public void Dispose() { _handler = null; }
    }
}
