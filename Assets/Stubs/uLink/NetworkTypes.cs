// Stub: uLink network types - consolidated small types used across the networking layer

using System;

namespace uLink
{
    /// <summary>Represents a player in the network. In single-player, represents the local player.</summary>
    public struct NetworkPlayer : IEquatable<NetworkPlayer>
    {
        public int id;

        /// <summary>Whether this player is the server.</summary>
        public bool isServer { get { return true; } }

        /// <summary>Whether this player is the local player.</summary>
        public bool isMine { get { return true; } }

        public bool Equals(NetworkPlayer other) { return id == other.id; }
        public override bool Equals(object obj) { return obj is NetworkPlayer other && Equals(other); }
        public override int GetHashCode() { return id; }
        public static bool operator ==(NetworkPlayer a, NetworkPlayer b) { return a.id == b.id; }
        public static bool operator !=(NetworkPlayer a, NetworkPlayer b) { return a.id != b.id; }
        public override string ToString() { return "NetworkPlayer(" + id + ")"; }
    }

    /// <summary>Identifies a network peer in P2P communication.</summary>
    public struct NetworkPeer : IEquatable<NetworkPeer>
    {
        public int id;

        public bool Equals(NetworkPeer other) { return id == other.id; }
        public override bool Equals(object obj) { return obj is NetworkPeer other && Equals(other); }
        public override int GetHashCode() { return id; }
        public static bool operator ==(NetworkPeer a, NetworkPeer b) { return a.id == b.id; }
        public static bool operator !=(NetworkPeer a, NetworkPeer b) { return a.id != b.id; }
    }

    /// <summary>Type of network peer.</summary>
    public enum NetworkPeerType
    {
        Disconnected,
        Server,
        Client,
        Connecting
    }

    /// <summary>Network connection status.</summary>
    public enum NetworkStatus
    {
        Disconnected,
        Connecting,
        Connected
    }

    /// <summary>Reason for network disconnection.</summary>
    public enum NetworkDisconnection
    {
        LostConnection,
        Disconnected
    }

    /// <summary>Network connection error codes.</summary>
    public enum NetworkConnectionError
    {
        NoError,
        ConnectionFailed,
        AlreadyConnectedToServer,
        AlreadyConnectedToAnotherServer,
        CreateSocketOrThreadFailure,
        IncorrectParameters,
        EmptyConnectTarget,
        InternalDirectConnectFailed,
        TooManyConnectedPlayers,
        ConnectionBanned,
        InvalidPassword,
        RSAPublicKeyMismatch,
        LimitedPlayers
    }

    /// <summary>Flags for network logging categories.</summary>
    [Flags]
    public enum NetworkLogFlags
    {
        None = 0,
        AuthoritativeServer = 1,
        Client = 2,
        Server = 4,
        Handshake = 8,
        StateSync = 16,
        RPC = 32,
        Instantiate = 64,
        All = 0x7FFFFFFF
    }

    /// <summary>Network log severity levels.</summary>
    public enum NetworkLogLevel
    {
        Off,
        Error,
        Warning,
        Info,
        Debug
    }

    /// <summary>Master server events.</summary>
    public enum MasterServerEvent
    {
        RegistrationSucceeded,
        RegistrationFailedGameName,
        RegistrationFailedGameType,
        RegistrationFailedNoServer,
        HostListReceived
    }

    /// <summary>Metadata for P2P messages.</summary>
    public struct NetworkP2PMessageInfo
    {
        public NetworkPeer sender;
        public double timestamp;
    }

    /// <summary>P2P networking. All methods are no-ops in single-player.</summary>
    public class NetworkP2P : UnityEngine.MonoBehaviour
    {
        public void RPC(string methodName, NetworkPeer target, params object[] args) { }
        public void RPC(string methodName, params object[] args) { }
    }

    /// <summary>Network logging. All methods are no-ops in single-player.</summary>
    public static class NetworkLog
    {
        public delegate void LogWriter(NetworkLogFlags flags, object[] args);

        public static LogWriter errorWriter { get; set; }
        public static LogWriter warningWriter { get; set; }
        public static LogWriter infoWriter { get; set; }

        public static void SetLevel(NetworkLogFlags flags, NetworkLogLevel level) { }
        public static void Info(NetworkLogFlags flags, params object[] args) { }
        public static void Warning(NetworkLogFlags flags, params object[] args) { }
        public static void Error(NetworkLogFlags flags, params object[] args) { }
    }

    /// <summary>Network log utility for formatting log output.</summary>
    public static class NetworkLogUtility
    {
        public static string ObjectsToString(object[] args)
        {
            if (args == null || args.Length == 0) return string.Empty;
            return string.Join(", ", System.Array.ConvertAll(args, a => a != null ? a.ToString() : "null"));
        }
    }

    /// <summary>General network utility methods.</summary>
    public static class NetworkUtility
    {
        public static string GetLocalIP() { return "127.0.0.1"; }
    }

    /// <summary>Represents a buffered RPC call.</summary>
    public class NetworkBufferedRPC
    {
        public string name;
        public RPCMode mode;
    }

    /// <summary>RSA public key for secure network connections.</summary>
    public class PublicKey
    {
        private readonly string _xmlKey;

        public PublicKey(string xmlKey)
        {
            _xmlKey = xmlKey;
        }

        public override string ToString() { return _xmlKey ?? string.Empty; }
    }
}
