// Stub: Steamworks.SteamMatchmaking - matchmaking and lobby management
// All methods are no-ops in stub mode.

namespace Steamworks
{
    public static class SteamMatchmaking
    {
        public static int GetFavoriteGameCount() { return 0; }

        public static bool GetFavoriteGame(int iGame, out AppId_t pnAppID, out uint pnIP, out ushort pnConnPort,
            out ushort pnQueryPort, out uint punFlags, out uint pRTime32LastPlayedOnServer)
        {
            pnAppID = new AppId_t();
            pnIP = 0;
            pnConnPort = 0;
            pnQueryPort = 0;
            punFlags = 0;
            pRTime32LastPlayedOnServer = 0;
            return false;
        }

        public static SteamAPICall_t CreateLobby(ELobbyType eLobbyType, int cMaxMembers)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t JoinLobby(CSteamID steamIDLobby)
        {
            return SteamAPICall_t.Invalid;
        }

        public static void LeaveLobby(CSteamID steamIDLobby) { }
    }

    public enum ELobbyType
    {
        k_ELobbyTypePrivate = 0,
        k_ELobbyTypeFriendsOnly = 1,
        k_ELobbyTypePublic = 2,
        k_ELobbyTypeInvisible = 3
    }
}
