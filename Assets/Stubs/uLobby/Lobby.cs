// Stub: uLobby.Lobby - lobby connection and matchmaking
// All methods are no-ops in single-player.

using System;

namespace uLobby
{
    public static class Lobby
    {
        /// <summary>Whether the lobby is connected. Always false in single-player.</summary>
        public static bool isConnected { get { return false; } }

        /// <summary>Event raised when connected to the lobby.</summary>
        public static event Action OnConnected;

        /// <summary>Event raised when disconnected from the lobby.</summary>
        public static event Action OnDisconnected;

        /// <summary>Event raised when connection to the lobby fails.</summary>
        public static event Action<LobbyConnectionError> OnFailedToConnect;

        /// <summary>Add a listener object for lobby events. No-op in single-player.</summary>
        public static void AddListener(object listener) { }

        /// <summary>Remove a listener object for lobby events. No-op in single-player.</summary>
        public static void RemoveListener(object listener) { }

        /// <summary>Connect to the lobby server. No-op in single-player.</summary>
        public static void Connect(string host, int port) { }

        /// <summary>Disconnect from the lobby server. No-op in single-player.</summary>
        public static void Disconnect() { }

        // Suppress unused event warnings -- events are part of the public API surface
        private static void SuppressWarnings()
        {
            OnConnected?.Invoke();
            OnDisconnected?.Invoke();
            OnFailedToConnect?.Invoke(LobbyConnectionError.ConnectionFailed);
        }
    }
}
