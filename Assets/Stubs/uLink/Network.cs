// Stub: uLink.Network - static network management class
// Single-player defaults: isServer=true, isClient=false, status=Disconnected

using UnityEngine;

namespace uLink
{
    /// <summary>Network configuration settings.</summary>
    public class NetworkConfig
    {
        public float timeoutDelay { get; set; }
        public int maxConnections { get; set; }
        public int sendRate { get; set; }
    }

    public static class Network
    {
        private static readonly NetworkConfig _config = new NetworkConfig();
        private static readonly NetworkPlayer[] _emptyConnections = new NetworkPlayer[0];

        /// <summary>True for single-player: we act as the server.</summary>
        public static bool isServer { get { return true; } }

        /// <summary>False for single-player: no client connection.</summary>
        public static bool isClient { get { return false; } }

        /// <summary>True for single-player: server has authority.</summary>
        public static bool isAuthoritativeServer { get { return true; } }

        /// <summary>Current peer type. Server in single-player.</summary>
        public static NetworkPeerType peerType { get { return NetworkPeerType.Server; } }

        /// <summary>Current network status. Connected in single-player (consistent with isServer=true).</summary>
        public static NetworkStatus status { get { return NetworkStatus.Connected; } }

        /// <summary>The local player.</summary>
        public static NetworkPlayer player { get { return new NetworkPlayer(); } }

        /// <summary>Currently connected players. Empty in single-player.</summary>
        public static NetworkPlayer[] connections { get { return _emptyConnections; } }

        /// <summary>Network send rate in calls per second.</summary>
        public static float sendRate { get; set; }

        /// <summary>Network configuration object.</summary>
        public static NetworkConfig config { get { return _config; } }

        /// <summary>Whether security is required for connecting.</summary>
        public static bool requireSecurityForConnecting { get; set; }

        /// <summary>Public key for secure connections.</summary>
        public static PublicKey publicKey { get; set; }

        /// <summary>Minimum number of manually allocated view IDs.</summary>
        public static int maxManualViewIDs { get; set; }

        /// <summary>Minimum allocatable view IDs.</summary>
        public static int minimumAllocatableViewIDs { get; set; }

        /// <summary>Minimum used view IDs.</summary>
        public static int minimumUsedViewIDs { get; set; }

        // ---- Connection Methods (all no-ops) ----

        public static NetworkConnectionError Connect(string host, int remotePort, string password = "", params object[] loginData)
        {
            return NetworkConnectionError.NoError;
        }

        public static NetworkConnectionError Connect(HostData hostData, string password = "", params object[] loginData)
        {
            return NetworkConnectionError.NoError;
        }

        public static void Disconnect() { }

        public static void Disconnect(int timeout) { }

        public static void InitializeServer(int maxPlayers, int listenPort)
        {
            // No-op
        }

        public static void CloseConnection(NetworkPlayer target, bool sendDisconnectionNotification)
        {
            // No-op
        }

        // ---- Object Management (all no-ops) ----

        public static void Destroy(NetworkViewID viewID)
        {
            // No-op
        }

        public static void Destroy(GameObject gameObject)
        {
            // No-op
        }

        public static GameObject Instantiate(
            string prefabName,
            Vector3 position,
            Quaternion rotation,
            int group,
            params object[] initialData)
        {
            throw new System.NotImplementedException(
                $"uLink stub: Network.Instantiate(\"{prefabName}\") not implemented — use UnityEngine.Object.Instantiate for single-player");
        }

        public static GameObject Instantiate(
            NetworkPlayer owner,
            string prefabName,
            Vector3 position,
            Quaternion rotation,
            int group,
            params object[] initialData)
        {
            throw new System.NotImplementedException(
                $"uLink stub: Network.Instantiate(owner, \"{prefabName}\") not implemented — use UnityEngine.Object.Instantiate for single-player");
        }

        // ---- RPC Methods (all no-ops) ----

        public static void RPC(NetworkView view, string methodName, RPCMode mode, params object[] args)
        {
            // No-op
        }

        public static void RPC(NetworkView view, string methodName, NetworkPlayer target, params object[] args)
        {
            // No-op
        }

        static Network()
        {
            sendRate = 15f;
        }
    }
}
