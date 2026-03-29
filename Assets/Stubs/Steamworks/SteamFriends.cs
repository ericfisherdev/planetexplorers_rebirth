// Stub: Steamworks.SteamFriends - friend list and social features
// All methods return safe defaults. No-ops in stub mode.

namespace Steamworks
{
    public static class SteamFriends
    {
        public static string GetPersonaName() { return "Player"; }

        public static int GetFriendCount(EFriendFlags eFriendFlags) { return 0; }

        public static CSteamID GetFriendByIndex(int iFriend, EFriendFlags eFriendFlags) { return new CSteamID(); }

        public static string GetFriendPersonaName(CSteamID steamIDFriend) { return "Friend"; }

        public static EPersonaState GetFriendPersonaState(CSteamID steamIDFriend) { return EPersonaState.k_EPersonaStateOffline; }

        public static bool GetFriendGamePlayed(CSteamID steamIDFriend, out FriendGameInfo_t pFriendGameInfo)
        {
            pFriendGameInfo = new FriendGameInfo_t();
            return false;
        }

        public static int GetSmallFriendAvatar(CSteamID steamIDFriend) { return -1; }

        public static int GetMediumFriendAvatar(CSteamID steamIDFriend) { return -1; }

        public static int GetLargeFriendAvatar(CSteamID steamIDFriend) { return -1; }

        public static void ActivateGameOverlayToUser(string pchDialog, CSteamID steamID) { }

        public static void ActivateGameOverlay(string pchDialog) { }

        public static bool InviteUserToGame(CSteamID steamIDFriend, string pchConnectString) { return false; }

        public static bool SetListenForFriendsMessages(bool bInterceptEnabled) { return false; }

        public static bool ReplyToFriendMessage(CSteamID steamIDFriend, string pchMsgToSend) { return false; }

        public static int GetFriendMessage(CSteamID steamIDFriend, int iMessageID, out string pvData, int cubData, out EChatEntryType peChatEntryType)
        {
            pvData = string.Empty;
            peChatEntryType = EChatEntryType.k_EChatEntryTypeChatMsg;
            return 0;
        }
    }

    public enum EChatEntryType
    {
        k_EChatEntryTypeInvalid = 0,
        k_EChatEntryTypeChatMsg = 1,
        k_EChatEntryTypeTyping = 2,
        k_EChatEntryTypeInviteGame = 3,
        k_EChatEntryTypeEmote = 4,
        k_EChatEntryTypeLeftConversation = 6,
        k_EChatEntryTypeEntered = 7,
        k_EChatEntryTypeWasKicked = 8,
        k_EChatEntryTypeWasBanned = 9,
        k_EChatEntryTypeDisconnected = 10,
        k_EChatEntryTypeHistoricalChat = 11,
        k_EChatEntryTypeLinkBlocked = 14
    }
}
