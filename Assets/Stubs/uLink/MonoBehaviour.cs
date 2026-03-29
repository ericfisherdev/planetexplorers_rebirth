// Stub: uLink.MonoBehaviour - extends UnityEngine.MonoBehaviour for single-player
// All networked game objects inherit from this. Returns single-player defaults.

namespace uLink
{
    public class MonoBehaviour : UnityEngine.MonoBehaviour
    {
        private NetworkView _stubNetworkView;

        /// <summary>Returns true for single-player: this object belongs to the local player.</summary>
        public bool isMine { get { return true; } }

        /// <summary>Returns true for single-player: we are the server.</summary>
        public bool isServer { get { return true; } }

        /// <summary>Returns false for single-player: no remote client connection.</summary>
        public bool isClient { get { return false; } }

        /// <summary>Returns true for single-player: local player owns this object.</summary>
        public bool isOwner { get { return true; } }

        /// <summary>Returns false for single-player: no proxy objects exist.</summary>
        public bool isProxy { get { return false; } }

        /// <summary>Stub NetworkView accessor. Returns a cached stub NetworkView component.</summary>
        public new NetworkView networkView
        {
            get
            {
                if (_stubNetworkView == null)
                {
                    _stubNetworkView = GetComponent<NetworkView>();
                    if (_stubNetworkView == null)
                    {
                        _stubNetworkView = gameObject.AddComponent<NetworkView>();
                    }
                }
                return _stubNetworkView;
            }
        }

        /// <summary>Called by uLink for network serialization. No-op in single-player.</summary>
        public virtual void uLink_OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
        {
            // No-op: no network serialization in single-player
        }

        /// <summary>Called when object is instantiated over network. No-op in single-player.</summary>
        public virtual void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
        {
            // No-op: no network instantiation in single-player
        }

        /// <summary>Called when connected to server. No-op in single-player.</summary>
        public virtual void uLink_OnConnectedToServer()
        {
            // No-op
        }

        /// <summary>Called when disconnected from server. No-op in single-player.</summary>
        public virtual void uLink_OnDisconnectedFromServer(NetworkDisconnection mode)
        {
            // No-op
        }

        /// <summary>Called when connection to server fails. No-op in single-player.</summary>
        public virtual void uLink_OnFailedToConnect(NetworkConnectionError error)
        {
            // No-op
        }

        /// <summary>Called on master server event. No-op in single-player.</summary>
        public virtual void uLink_OnMasterServerEvent(MasterServerEvent msEvent)
        {
            // No-op
        }

        /// <summary>Called when pre-buffered RPCs are received. No-op in single-player.</summary>
        public virtual void uLink_OnPreBufferedRPCs(NetworkBufferedRPC[] bufferedArray)
        {
            // No-op
        }

        /// <summary>Called when a P2P peer disconnects. No-op in single-player.</summary>
        public virtual void uLink_OnPeerDisconnected(NetworkPeer peer)
        {
            // No-op
        }
    }
}
