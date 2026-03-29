// Stub: uLobby.ServerRegistry - registry of available servers in the lobby
// All methods return empty results in single-player.

using System.Collections.Generic;
using System.Linq;

namespace uLobby
{
    public static class ServerRegistry
    {
        private static readonly List<ServerInfo> _emptyServers = new List<ServerInfo>();

        /// <summary>Get all registered servers. Returns empty in single-player.</summary>
        public static IEnumerable<ServerInfo> GetServers()
        {
            return Enumerable.Empty<ServerInfo>();
        }

        /// <summary>Register a server. No-op in single-player.</summary>
        public static void RegisterServer(ServerInfo serverInfo) { }

        /// <summary>Unregister a server. No-op in single-player.</summary>
        public static void UnregisterServer(ServerInfo serverInfo) { }
    }
}
