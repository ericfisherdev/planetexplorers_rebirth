// Stub: uLink.NetworkMessageInfo - metadata accompanying network messages (592 references)

namespace uLink
{
    public struct NetworkMessageInfo
    {
        /// <summary>The player who sent this message.</summary>
        public NetworkPlayer sender;

        /// <summary>Timestamp when the message was sent.</summary>
        public double timestamp;

        /// <summary>The NetworkView associated with this message.</summary>
        public NetworkView networkView;
    }
}
