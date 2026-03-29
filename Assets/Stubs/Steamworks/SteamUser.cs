// Stub: Steamworks.SteamUser - current user identity and authentication
// All methods return safe defaults.

namespace Steamworks
{
    public static class SteamUser
    {
        public static CSteamID GetSteamID() { return new CSteamID(); }

        public static bool BLoggedOn() { return false; }

        public static HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket)
        {
            pcbTicket = 0;
            return HAuthTicket.Invalid;
        }

        public static void CancelAuthTicket(HAuthTicket hAuthTicket) { }
    }
}
