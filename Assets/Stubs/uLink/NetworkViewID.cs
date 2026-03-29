// Stub: uLink.NetworkViewID - unique identifier for network views

namespace uLink
{
    public struct NetworkViewID : System.IEquatable<NetworkViewID>
    {
        /// <summary>Internal numeric identifier.</summary>
        public int id;

        /// <summary>Represents an unassigned view ID.</summary>
        public static readonly NetworkViewID unassigned = new NetworkViewID { id = 0 };

        public bool Equals(NetworkViewID other)
        {
            return id == other.id;
        }

        public override bool Equals(object obj)
        {
            return obj is NetworkViewID other && Equals(other);
        }

        public override int GetHashCode()
        {
            return id;
        }

        public static bool operator ==(NetworkViewID a, NetworkViewID b)
        {
            return a.id == b.id;
        }

        public static bool operator !=(NetworkViewID a, NetworkViewID b)
        {
            return a.id != b.id;
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}
