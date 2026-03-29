// Stub: uLink.NetworkView - network-aware view component attached to networked objects
// Single-player defaults: isMine=true, isOwner=true, isProxy=false

using UnityEngine;

namespace uLink
{
    [DisallowMultipleComponent]
    public class NetworkView : UnityEngine.MonoBehaviour
    {
        private BitStream _initialData;

        /// <summary>The view ID for this network view.</summary>
        public NetworkViewID viewID { get; set; }

        /// <summary>True for single-player: this view belongs to the local player.</summary>
        public bool isMine { get { return true; } }

        /// <summary>True for single-player: local player owns this view.</summary>
        public bool isOwner { get { return true; } }

        /// <summary>False for single-player: no proxy views exist.</summary>
        public bool isProxy { get { return false; } }

        /// <summary>The owning player.</summary>
        public NetworkPlayer owner { get { return new NetworkPlayer(); } }

        /// <summary>Network group for this view.</summary>
        public int group { get; set; }

        /// <summary>Initial data stream passed during instantiation.</summary>
        public BitStream initialData
        {
            get
            {
                if (_initialData == null)
                {
                    _initialData = new BitStream { isReading = true };
                }
                return _initialData;
            }
            set { _initialData = value; }
        }

        /// <summary>Send an RPC call. No-op in single-player.</summary>
        public void RPC(string methodName, RPCMode mode, params object[] args)
        {
            // No-op
        }

        /// <summary>Send an RPC to a specific player. No-op in single-player.</summary>
        public void RPC(string methodName, NetworkPlayer target, params object[] args)
        {
            // No-op
        }

        /// <summary>Send an unreliable RPC call. No-op in single-player.</summary>
        public void UnreliableRPC(string methodName, RPCMode mode, params object[] args)
        {
            // No-op
        }

        /// <summary>Send an unreliable RPC to a specific player. No-op in single-player.</summary>
        public void UnreliableRPC(string methodName, NetworkPlayer target, params object[] args)
        {
            // No-op
        }

        /// <summary>Find a NetworkView by its view ID. Returns null in single-player.</summary>
        public static NetworkView Find(NetworkViewID viewID)
        {
            return null;
        }
    }
}
