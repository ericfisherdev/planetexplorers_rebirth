// Stub: Steamworks types - value types, enums, and callback structs used across the Steam API

using System;

namespace Steamworks
{
    // ---- Core ID Types ----

    public struct CSteamID : IEquatable<CSteamID>
    {
        public ulong m_SteamID;

        public CSteamID(ulong steamID) { m_SteamID = steamID; }

        public bool IsValid() { return m_SteamID != 0; }
        public AccountID_t GetAccountID() { return new AccountID_t((uint)(m_SteamID & 0xFFFFFFFF)); }

        public static implicit operator CSteamID(ulong value) { return new CSteamID(value); }
        public static implicit operator ulong(CSteamID steamID) { return steamID.m_SteamID; }

        public bool Equals(CSteamID other) { return m_SteamID == other.m_SteamID; }
        public override bool Equals(object obj) { return obj is CSteamID other && Equals(other); }
        public override int GetHashCode() { return m_SteamID.GetHashCode(); }
        public static bool operator ==(CSteamID a, CSteamID b) { return a.m_SteamID == b.m_SteamID; }
        public static bool operator !=(CSteamID a, CSteamID b) { return a.m_SteamID != b.m_SteamID; }
        public override string ToString() { return m_SteamID.ToString(); }
    }

    public struct CGameID : IEquatable<CGameID>
    {
        public ulong m_GameID;

        public CGameID(ulong gameID) { m_GameID = gameID; }

        public bool Equals(CGameID other) { return m_GameID == other.m_GameID; }
        public override bool Equals(object obj) { return obj is CGameID other && Equals(other); }
        public override int GetHashCode() { return m_GameID.GetHashCode(); }
        public static bool operator ==(CGameID a, CGameID b) { return a.m_GameID == b.m_GameID; }
        public static bool operator !=(CGameID a, CGameID b) { return a.m_GameID != b.m_GameID; }
    }

    public struct AppId_t : IEquatable<AppId_t>
    {
        public uint m_AppId;

        public static readonly AppId_t Invalid = new AppId_t(0);

        public AppId_t(uint appId) { m_AppId = appId; }

        public static explicit operator AppId_t(uint value) { return new AppId_t(value); }
        public static explicit operator uint(AppId_t appId) { return appId.m_AppId; }

        public bool Equals(AppId_t other) { return m_AppId == other.m_AppId; }
        public override bool Equals(object obj) { return obj is AppId_t other && Equals(other); }
        public override int GetHashCode() { return (int)m_AppId; }
        public static bool operator ==(AppId_t a, AppId_t b) { return a.m_AppId == b.m_AppId; }
        public static bool operator !=(AppId_t a, AppId_t b) { return a.m_AppId != b.m_AppId; }
    }

    public struct PublishedFileId_t : IEquatable<PublishedFileId_t>
    {
        public ulong m_PublishedFileId;

        public static readonly PublishedFileId_t Invalid = new PublishedFileId_t(0);

        public PublishedFileId_t(ulong id) { m_PublishedFileId = id; }

        public bool Equals(PublishedFileId_t other) { return m_PublishedFileId == other.m_PublishedFileId; }
        public override bool Equals(object obj) { return obj is PublishedFileId_t other && Equals(other); }
        public override int GetHashCode() { return m_PublishedFileId.GetHashCode(); }
        public static bool operator ==(PublishedFileId_t a, PublishedFileId_t b) { return a.m_PublishedFileId == b.m_PublishedFileId; }
        public static bool operator !=(PublishedFileId_t a, PublishedFileId_t b) { return a.m_PublishedFileId != b.m_PublishedFileId; }
    }

    public struct UGCHandle_t : IEquatable<UGCHandle_t>
    {
        public ulong m_UGCHandle;

        public static readonly UGCHandle_t Invalid = new UGCHandle_t(0xFFFFFFFFFFFFFFFF);

        public UGCHandle_t(ulong handle) { m_UGCHandle = handle; }

        public bool Equals(UGCHandle_t other) { return m_UGCHandle == other.m_UGCHandle; }
        public override bool Equals(object obj) { return obj is UGCHandle_t other && Equals(other); }
        public override int GetHashCode() { return m_UGCHandle.GetHashCode(); }
        public static bool operator ==(UGCHandle_t a, UGCHandle_t b) { return a.m_UGCHandle == b.m_UGCHandle; }
        public static bool operator !=(UGCHandle_t a, UGCHandle_t b) { return a.m_UGCHandle != b.m_UGCHandle; }
    }

    public struct SteamAPICall_t : IEquatable<SteamAPICall_t>
    {
        public ulong m_SteamAPICall;

        public static readonly SteamAPICall_t Invalid = new SteamAPICall_t(0);

        public SteamAPICall_t(ulong call) { m_SteamAPICall = call; }

        public bool Equals(SteamAPICall_t other) { return m_SteamAPICall == other.m_SteamAPICall; }
        public override bool Equals(object obj) { return obj is SteamAPICall_t other && Equals(other); }
        public override int GetHashCode() { return m_SteamAPICall.GetHashCode(); }
        public static bool operator ==(SteamAPICall_t a, SteamAPICall_t b) { return a.m_SteamAPICall == b.m_SteamAPICall; }
        public static bool operator !=(SteamAPICall_t a, SteamAPICall_t b) { return a.m_SteamAPICall != b.m_SteamAPICall; }
    }

    public struct AccountID_t : IEquatable<AccountID_t>
    {
        public uint m_AccountID;

        public AccountID_t(uint id) { m_AccountID = id; }

        public bool Equals(AccountID_t other) { return m_AccountID == other.m_AccountID; }
        public override bool Equals(object obj) { return obj is AccountID_t other && Equals(other); }
        public override int GetHashCode() { return (int)m_AccountID; }
        public static bool operator ==(AccountID_t a, AccountID_t b) { return a.m_AccountID == b.m_AccountID; }
        public static bool operator !=(AccountID_t a, AccountID_t b) { return a.m_AccountID != b.m_AccountID; }
    }

    public struct HAuthTicket : IEquatable<HAuthTicket>
    {
        public uint m_HAuthTicket;

        public static readonly HAuthTicket Invalid = new HAuthTicket(0);

        public HAuthTicket(uint ticket) { m_HAuthTicket = ticket; }

        public bool Equals(HAuthTicket other) { return m_HAuthTicket == other.m_HAuthTicket; }
        public override bool Equals(object obj) { return obj is HAuthTicket other && Equals(other); }
        public override int GetHashCode() { return (int)m_HAuthTicket; }
        public static bool operator ==(HAuthTicket a, HAuthTicket b) { return a.m_HAuthTicket == b.m_HAuthTicket; }
        public static bool operator !=(HAuthTicket a, HAuthTicket b) { return a.m_HAuthTicket != b.m_HAuthTicket; }
    }

    public struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>
    {
        public ulong m_UGCQueryHandle;

        public static readonly UGCQueryHandle_t Invalid = new UGCQueryHandle_t(0xFFFFFFFFFFFFFFFF);

        public UGCQueryHandle_t(ulong handle) { m_UGCQueryHandle = handle; }

        public bool Equals(UGCQueryHandle_t other) { return m_UGCQueryHandle == other.m_UGCQueryHandle; }
        public override bool Equals(object obj) { return obj is UGCQueryHandle_t other && Equals(other); }
        public override int GetHashCode() { return m_UGCQueryHandle.GetHashCode(); }
        public static bool operator ==(UGCQueryHandle_t a, UGCQueryHandle_t b) { return a.m_UGCQueryHandle == b.m_UGCQueryHandle; }
        public static bool operator !=(UGCQueryHandle_t a, UGCQueryHandle_t b) { return a.m_UGCQueryHandle != b.m_UGCQueryHandle; }
    }

    public struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>
    {
        public ulong m_UGCUpdateHandle;

        public static readonly UGCUpdateHandle_t Invalid = new UGCUpdateHandle_t(0xFFFFFFFFFFFFFFFF);

        public UGCUpdateHandle_t(ulong handle) { m_UGCUpdateHandle = handle; }

        public bool Equals(UGCUpdateHandle_t other) { return m_UGCUpdateHandle == other.m_UGCUpdateHandle; }
        public override bool Equals(object obj) { return obj is UGCUpdateHandle_t other && Equals(other); }
        public override int GetHashCode() { return m_UGCUpdateHandle.GetHashCode(); }
        public static bool operator ==(UGCUpdateHandle_t a, UGCUpdateHandle_t b) { return a.m_UGCUpdateHandle == b.m_UGCUpdateHandle; }
        public static bool operator !=(UGCUpdateHandle_t a, UGCUpdateHandle_t b) { return a.m_UGCUpdateHandle != b.m_UGCUpdateHandle; }
    }

    public struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>
    {
        public ulong m_SteamLeaderboard;

        public SteamLeaderboard_t(ulong handle) { m_SteamLeaderboard = handle; }

        public bool Equals(SteamLeaderboard_t other) { return m_SteamLeaderboard == other.m_SteamLeaderboard; }
        public override bool Equals(object obj) { return obj is SteamLeaderboard_t other && Equals(other); }
        public override int GetHashCode() { return m_SteamLeaderboard.GetHashCode(); }
        public static bool operator ==(SteamLeaderboard_t a, SteamLeaderboard_t b) { return a.m_SteamLeaderboard == b.m_SteamLeaderboard; }
        public static bool operator !=(SteamLeaderboard_t a, SteamLeaderboard_t b) { return a.m_SteamLeaderboard != b.m_SteamLeaderboard; }
    }

    public struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>
    {
        public ulong m_SteamLeaderboardEntries;

        public SteamLeaderboardEntries_t(ulong handle) { m_SteamLeaderboardEntries = handle; }

        public bool Equals(SteamLeaderboardEntries_t other) { return m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries; }
        public override bool Equals(object obj) { return obj is SteamLeaderboardEntries_t other && Equals(other); }
        public override int GetHashCode() { return m_SteamLeaderboardEntries.GetHashCode(); }
        public static bool operator ==(SteamLeaderboardEntries_t a, SteamLeaderboardEntries_t b) { return a.m_SteamLeaderboardEntries == b.m_SteamLeaderboardEntries; }
        public static bool operator !=(SteamLeaderboardEntries_t a, SteamLeaderboardEntries_t b) { return a.m_SteamLeaderboardEntries != b.m_SteamLeaderboardEntries; }
    }

    // ---- Enums ----

    public enum EResult
    {
        k_EResultOK = 1,
        k_EResultFail = 2,
        k_EResultNoConnection = 3,
        k_EResultInvalidPassword = 5,
        k_EResultLoggedInElsewhere = 6,
        k_EResultInvalidProtocolVer = 7,
        k_EResultInvalidParam = 8,
        k_EResultFileNotFound = 9,
        k_EResultBusy = 10,
        k_EResultInvalidState = 11,
        k_EResultInvalidName = 12,
        k_EResultInvalidEmail = 13,
        k_EResultDuplicateName = 14,
        k_EResultAccessDenied = 15,
        k_EResultTimeout = 16,
        k_EResultBanned = 17,
        k_EResultAccountNotFound = 18,
        k_EResultInsufficientPrivilege = 24,
        k_EResultLimitExceeded = 25,
        k_EResultRevoked = 26,
        k_EResultExpired = 27,
        k_EResultAlreadyRedeemed = 28,
        k_EResultDuplicateRequest = 29,
        k_EResultFileNotWritable = 83
    }

    public enum EPersonaState
    {
        k_EPersonaStateOffline = 0,
        k_EPersonaStateOnline = 1,
        k_EPersonaStateBusy = 2,
        k_EPersonaStateAway = 3,
        k_EPersonaStateSnooze = 4,
        k_EPersonaStateLookingToTrade = 5,
        k_EPersonaStateLookingToPlay = 6,
        k_EPersonaStateMax = 7
    }

    public enum EFriendRelationship
    {
        k_EFriendRelationshipNone = 0,
        k_EFriendRelationshipBlocked = 1,
        k_EFriendRelationshipRequestRecipient = 2,
        k_EFriendRelationshipFriend = 3,
        k_EFriendRelationshipRequestInitiator = 4,
        k_EFriendRelationshipIgnored = 5,
        k_EFriendRelationshipIgnoredFriend = 6,
        k_EFriendRelationshipMax = 8
    }

    [Flags]
    public enum EFriendFlags
    {
        k_EFriendFlagNone = 0x00,
        k_EFriendFlagBlocked = 0x01,
        k_EFriendFlagFriendshipRequested = 0x02,
        k_EFriendFlagImmediate = 0x04,
        k_EFriendFlagClanMember = 0x08,
        k_EFriendFlagOnGameServer = 0x10,
        k_EFriendFlagRequestingFriendship = 0x80,
        k_EFriendFlagRequestingInfo = 0x100,
        k_EFriendFlagIgnored = 0x200,
        k_EFriendFlagIgnoredFriend = 0x400,
        k_EFriendFlagAll = 0xFFFF
    }

    public enum EUGCQuery
    {
        k_EUGCQuery_RankedByVote = 0,
        k_EUGCQuery_RankedByPublicationDate = 1,
        k_EUGCQuery_AcceptedForGameRankedByAcceptanceDate = 2,
        k_EUGCQuery_RankedByTrend = 3,
        k_EUGCQuery_FavoritedByFriendsRankedByPublicationDate = 4,
        k_EUGCQuery_CreatedByFriendsRankedByPublicationDate = 5,
        k_EUGCQuery_RankedByNumTimesReported = 6,
        k_EUGCQuery_CreatedByFollowedUsersRankedByPublicationDate = 7,
        k_EUGCQuery_NotYetRated = 8,
        k_EUGCQuery_RankedByTotalVotesAsc = 9,
        k_EUGCQuery_RankedByVotesUp = 10,
        k_EUGCQuery_RankedByTextSearch = 11
    }

    public enum EUGCMatchingUGCType
    {
        k_EUGCMatchingUGCType_Items = 0,
        k_EUGCMatchingUGCType_Items_Mtx = 1,
        k_EUGCMatchingUGCType_Items_ReadyToUse = 2,
        k_EUGCMatchingUGCType_Collections = 3,
        k_EUGCMatchingUGCType_Artwork = 4,
        k_EUGCMatchingUGCType_Videos = 5,
        k_EUGCMatchingUGCType_Screenshots = 6,
        k_EUGCMatchingUGCType_AllGuides = 7,
        k_EUGCMatchingUGCType_WebGuides = 8,
        k_EUGCMatchingUGCType_IntegratedGuides = 9,
        k_EUGCMatchingUGCType_UsableInGame = 10,
        k_EUGCMatchingUGCType_ControllerBindings = 11,
        k_EUGCMatchingUGCType_GameManagedItems = 12,
        k_EUGCMatchingUGCType_All = unchecked((int)0xFFFFFFFF)
    }

    public enum EUserUGCList
    {
        k_EUserUGCList_Published = 0,
        k_EUserUGCList_VotedOn = 1,
        k_EUserUGCList_VotedUp = 2,
        k_EUserUGCList_VotedDown = 3,
        k_EUserUGCList_WillVoteLater = 4,
        k_EUserUGCList_Favorited = 5,
        k_EUserUGCList_Subscribed = 6,
        k_EUserUGCList_UsedOrPlayed = 7,
        k_EUserUGCList_Followed = 8
    }

    public enum EUserUGCListSortOrder
    {
        k_EUserUGCListSortOrder_CreationOrderDesc = 0,
        k_EUserUGCListSortOrder_CreationOrderAsc = 1,
        k_EUserUGCListSortOrder_TitleAsc = 2,
        k_EUserUGCListSortOrder_LastUpdatedDesc = 3,
        k_EUserUGCListSortOrder_SubscriptionDateDesc = 4,
        k_EUserUGCListSortOrder_VoteScoreDesc = 5,
        k_EUserUGCListSortOrder_ForModeration = 6
    }

    public enum EItemStatistic
    {
        k_EItemStatistic_NumSubscriptions = 0,
        k_EItemStatistic_NumFavorites = 1,
        k_EItemStatistic_NumFollowers = 2,
        k_EItemStatistic_NumUniqueSubscriptions = 3,
        k_EItemStatistic_NumUniqueFavorites = 4,
        k_EItemStatistic_NumUniqueFollowers = 5,
        k_EItemStatistic_NumUniqueWebsiteViews = 6,
        k_EItemStatistic_ReportScore = 7
    }

    public enum EItemUpdateStatus
    {
        k_EItemUpdateStatusInvalid = 0,
        k_EItemUpdateStatusPreparingConfig = 1,
        k_EItemUpdateStatusPreparingContent = 2,
        k_EItemUpdateStatusUploadingContent = 3,
        k_EItemUpdateStatusUploadingPreviewFile = 4,
        k_EItemUpdateStatusCommittingChanges = 5
    }

    public enum ENotificationPosition
    {
        k_EPositionTopLeft = 0,
        k_EPositionTopRight = 1,
        k_EPositionBottomLeft = 2,
        k_EPositionBottomRight = 3
    }

    public enum ELeaderboardSortMethod
    {
        k_ELeaderboardSortMethodNone = 0,
        k_ELeaderboardSortMethodAscending = 1,
        k_ELeaderboardSortMethodDescending = 2
    }

    public enum ELeaderboardDisplayType
    {
        k_ELeaderboardDisplayTypeNone = 0,
        k_ELeaderboardDisplayTypeNumeric = 1,
        k_ELeaderboardDisplayTypeTimeSeconds = 2,
        k_ELeaderboardDisplayTypeTimeMilliSeconds = 3
    }

    public enum ELeaderboardDataRequest
    {
        k_ELeaderboardDataRequestGlobal = 0,
        k_ELeaderboardDataRequestGlobalAroundUser = 1,
        k_ELeaderboardDataRequestFriends = 2,
        k_ELeaderboardDataRequestUsers = 3
    }

    public enum EWorkshopFileType
    {
        k_EWorkshopFileTypeFirst = 0,
        k_EWorkshopFileTypeCommunity = 0,
        k_EWorkshopFileTypeMicrotransaction = 1,
        k_EWorkshopFileTypeCollection = 2,
        k_EWorkshopFileTypeArt = 3,
        k_EWorkshopFileTypeVideo = 4,
        k_EWorkshopFileTypeScreenshot = 5,
        k_EWorkshopFileTypeGame = 6,
        k_EWorkshopFileTypeSoftware = 7,
        k_EWorkshopFileTypeConcept = 8,
        k_EWorkshopFileTypeWebGuide = 9,
        k_EWorkshopFileTypeIntegratedGuide = 10,
        k_EWorkshopFileTypeMerch = 11,
        k_EWorkshopFileTypeControllerBinding = 12,
        k_EWorkshopFileTypeSteamworksAccessInvite = 13,
        k_EWorkshopFileTypeSteamVideo = 14,
        k_EWorkshopFileTypeGameManagedItem = 15,
        k_EWorkshopFileTypeMax = 16
    }

    public enum ERemoteStoragePublishedFileVisibility
    {
        k_ERemoteStoragePublishedFileVisibilityPublic = 0,
        k_ERemoteStoragePublishedFileVisibilityFriendsOnly = 1,
        k_ERemoteStoragePublishedFileVisibilityPrivate = 2
    }

    // ---- Callback Structs ----

    public struct SteamUGCDetails_t
    {
        public PublishedFileId_t m_nPublishedFileId;
        public EResult m_eResult;
        public EWorkshopFileType m_eFileType;
        public AppId_t m_nCreatorAppID;
        public AppId_t m_nConsumerAppID;
        public string m_rgchTitle;
        public string m_rgchDescription;
        public ulong m_ulSteamIDOwner;
        public uint m_rtimeCreated;
        public uint m_rtimeUpdated;
        public uint m_rtimeAddedToUserList;
        public ERemoteStoragePublishedFileVisibility m_eVisibility;
        public bool m_bBanned;
        public bool m_bAcceptedForUse;
        public bool m_bTagsTruncated;
        public string m_rgchTags;
        public UGCHandle_t m_hFile;
        public UGCHandle_t m_hPreviewFile;
        public string m_pchFileName;
        public int m_nFileSize;
        public int m_nPreviewFileSize;
        public string m_rgchURL;
        public uint m_unVotesUp;
        public uint m_unVotesDown;
        public float m_flScore;
        public uint m_unNumChildren;
    }

    public struct SteamUGCQueryCompleted_t
    {
        public UGCQueryHandle_t m_handle;
        public EResult m_eResult;
        public uint m_unNumResultsReturned;
        public uint m_unTotalMatchingResults;
        public bool m_bCachedData;
    }

    public struct PersonaStateChange_t
    {
        public ulong m_ulSteamID;
        public int m_nChangeFlags;
    }

    public struct GameOverlayActivated_t
    {
        public byte m_bActive;
    }

    public struct GameConnectedFriendChatMsg_t
    {
        public CSteamID m_steamIDUser;
        public int m_iMessageID;
    }

    public struct FriendGameInfo_t
    {
        public CGameID m_gameID;
        public uint m_unGameIP;
        public ushort m_usGamePort;
        public ushort m_usQueryPort;
        public CSteamID m_steamIDLobby;
    }

    public struct UserStatsReceived_t
    {
        public ulong m_nGameID;
        public EResult m_eResult;
        public CSteamID m_steamIDUser;
    }

    public struct UserStatsStored_t
    {
        public ulong m_nGameID;
        public EResult m_eResult;
    }

    public struct UserAchievementStored_t
    {
        public ulong m_nGameID;
        public bool m_bGroupAchievement;
        public string m_rgchAchievementName;
        public uint m_nCurProgress;
        public uint m_nMaxProgress;
    }

    public struct LeaderboardFindResult_t
    {
        public SteamLeaderboard_t m_hSteamLeaderboard;
        public byte m_bLeaderboardFound;
    }

    public struct LeaderboardScoreUploaded_t
    {
        public byte m_bSuccess;
        public SteamLeaderboard_t m_hSteamLeaderboard;
        public int m_nScore;
        public byte m_bScoreChanged;
        public int m_nGlobalRankNew;
        public int m_nGlobalRankPrevious;
    }

    public struct LeaderboardScoresDownloaded_t
    {
        public SteamLeaderboard_t m_hSteamLeaderboard;
        public SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;
        public int m_cEntryCount;
    }

    public struct LeaderboardEntry_t
    {
        public CSteamID m_steamIDUser;
        public int m_nGlobalRank;
        public int m_nScore;
        public int m_cDetails;
        public UGCHandle_t m_hUGC;
    }

    public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
    {
        public EResult m_eResult;
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
        public PublishedFileId_t[] m_rgPublishedFileId;
    }

    public struct RemoteStorageEnumerateWorkshopFilesResult_t
    {
        public EResult m_eResult;
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
        public PublishedFileId_t[] m_rgPublishedFileId;
        public float[] m_rgScore;
    }

    public struct RemoteStorageGetPublishedFileDetailsResult_t
    {
        public EResult m_eResult;
        public PublishedFileId_t m_nPublishedFileId;
        public AppId_t m_nCreatorAppID;
        public AppId_t m_nConsumerAppID;
        public string m_rgchTitle;
        public string m_rgchDescription;
        public UGCHandle_t m_hFile;
        public UGCHandle_t m_hPreviewFile;
        public CSteamID m_ulSteamIDOwner;
        public uint m_rtimeCreated;
        public uint m_rtimeUpdated;
        public string m_rgchTags;
        public string m_pchFileName;
        public int m_nFileSize;
    }

    public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
    {
        public EResult m_eResult;
        public PublishedFileId_t m_unPublishedFileId;
        public int m_nVotesFor;
        public int m_nVotesAgainst;
        public int m_nReports;
        public float m_fScore;
    }

    public struct RemoteStorageDownloadUGCResult_t
    {
        public EResult m_eResult;
        public UGCHandle_t m_hFile;
        public AppId_t m_nAppID;
        public int m_nSizeInBytes;
        public string m_pchFileName;
        public CSteamID m_ulSteamIDOwner;
    }

    public struct RemoteStorageFileShareResult_t
    {
        public EResult m_eResult;
        public UGCHandle_t m_hFile;
        public string m_rgchFilename;
    }

    public struct RemoteStoragePublishFileResult_t
    {
        public EResult m_eResult;
        public PublishedFileId_t m_nPublishedFileId;
        public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
    }

    public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
    {
        public EResult m_eResult;
        public PublishedFileId_t m_nPublishedFileId;
    }

    // ---- Utility Types ----

    public delegate void SteamAPIWarningMessageHook_t(int nSeverity, System.Text.StringBuilder pchDebugText);

    /// <summary>Packsize validation. Always passes in stub mode.</summary>
    public static class Packsize
    {
        public static bool Test() { return true; }
    }

    /// <summary>DLL check validation. Always passes in stub mode.</summary>
    public static class DllCheck
    {
        public static bool Test() { return true; }
    }
}
