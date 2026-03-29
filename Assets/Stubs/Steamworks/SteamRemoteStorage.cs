// Stub: Steamworks.SteamRemoteStorage - Cloud storage and Workshop file operations
// Deviation Rule 2: Added because 30+ references exist in game code but plan omitted it.
// All methods return safe defaults. No-ops in stub mode.

namespace Steamworks
{
    public static class SteamRemoteStorage
    {
        // ---- File Operations ----

        public static bool FileWrite(string pchFile, byte[] pvData, int cubData) { return false; }
        public static bool FileDelete(string pchFile) { return false; }
        public static bool FileExists(string pchFile) { return false; }

        public static SteamAPICall_t FileShare(string pchFile) { return SteamAPICall_t.Invalid; }

        public static int GetFileCount() { return 0; }

        public static string GetFileNameAndSize(int iFile, out int pnFileSizeInBytes)
        {
            pnFileSizeInBytes = 0;
            return string.Empty;
        }

        // ---- Cloud Settings ----

        public static bool IsCloudEnabledForAccount() { return false; }
        public static bool IsCloudEnabledForApp() { return false; }
        public static void SetCloudEnabledForApp(bool bEnabled) { }

        // ---- UGC Download ----

        public static SteamAPICall_t UGCDownload(UGCHandle_t hContent, uint unPriority)
        {
            return SteamAPICall_t.Invalid;
        }

        public static bool GetUGCDownloadProgress(UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected)
        {
            pnBytesDownloaded = 0;
            pnBytesExpected = 0;
            return false;
        }

        public static int UGCRead(UGCHandle_t hContent, byte[] pvData, int cubDataToRead, uint cOffset, EUGCReadAction eAction)
        {
            return 0;
        }

        // ---- Published File Operations ----

        public static SteamAPICall_t GetPublishedFileDetails(PublishedFileId_t unPublishedFileId, uint unMaxSecondsOld)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t GetPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t UpdateUserPublishedItemVote(PublishedFileId_t unPublishedFileId, bool bVoteUp)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t DeletePublishedFile(PublishedFileId_t unPublishedFileId)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t EnumeratePublishedWorkshopFiles(
            EWorkshopEnumerationType eEnumerationType,
            uint unStartIndex,
            uint unCount,
            uint unDays,
            string[] pTags,
            string[] pUserTags)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t EnumerateUserSharedWorkshopFiles(
            CSteamID steamId,
            uint unStartIndex,
            string[] pRequiredTags,
            string[] pExcludedTags)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t PublishWorkshopFile(
            string pchFile,
            string pchPreviewFile,
            AppId_t nConsumerAppId,
            string pchTitle,
            string pchDescription,
            ERemoteStoragePublishedFileVisibility eVisibility,
            string[] pTags,
            EWorkshopFileType eWorkshopFileType)
        {
            return SteamAPICall_t.Invalid;
        }
    }

    public enum EWorkshopEnumerationType
    {
        k_EWorkshopEnumerationTypeRankedByVote = 0,
        k_EWorkshopEnumerationTypeRecent = 1,
        k_EWorkshopEnumerationTypeTrending = 2,
        k_EWorkshopEnumerationTypeFavoritesOfFriends = 3,
        k_EWorkshopEnumerationTypeVotedByFriends = 4,
        k_EWorkshopEnumerationTypeContentByFriends = 5,
        k_EWorkshopEnumerationTypeRecentFromFollowedUsers = 6
    }

    public enum EUGCReadAction
    {
        k_EUGCRead_ContinueReadingUntilFinished = 0,
        k_EUGCRead_ContinueReading = 1,
        k_EUGCRead_Close = 2
    }
}
