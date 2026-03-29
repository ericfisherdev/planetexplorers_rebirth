// Stub: Steamworks.SteamUGC - User Generated Content (Workshop) operations
// All methods return safe defaults. No-ops in stub mode.

namespace Steamworks
{
    public static class SteamUGC
    {
        public static UGCQueryHandle_t CreateQueryUserUGCRequest(
            AccountID_t unAccountID,
            EUserUGCList eListType,
            EUGCMatchingUGCType eMatchingUGCType,
            EUserUGCListSortOrder eSortOrder,
            AppId_t nCreatorAppID,
            AppId_t nConsumerAppID,
            uint unPage)
        {
            return UGCQueryHandle_t.Invalid;
        }

        public static UGCQueryHandle_t CreateQueryAllUGCRequest(
            EUGCQuery eQueryType,
            EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType,
            AppId_t nCreatorAppID,
            AppId_t nConsumerAppID,
            uint unPage)
        {
            return UGCQueryHandle_t.Invalid;
        }

        public static bool SetSearchText(UGCQueryHandle_t handle, string pSearchText) { return false; }

        public static bool SetCloudFileNameFilter(UGCQueryHandle_t handle, string pMatchCloudFileName) { return false; }

        public static bool AddRequiredTag(UGCQueryHandle_t handle, string pTagName) { return false; }

        public static SteamAPICall_t SendQueryUGCRequest(UGCQueryHandle_t handle)
        {
            return SteamAPICall_t.Invalid;
        }

        public static bool GetQueryUGCResult(UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t pDetails)
        {
            pDetails = new SteamUGCDetails_t();
            return false;
        }

        public static bool ReleaseQueryUGCRequest(UGCQueryHandle_t handle) { return false; }

        public static SteamAPICall_t CreateItem(AppId_t nConsumerAppId, EWorkshopFileType eFileType)
        {
            return SteamAPICall_t.Invalid;
        }

        public static UGCUpdateHandle_t StartItemUpdate(AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID)
        {
            return UGCUpdateHandle_t.Invalid;
        }

        public static bool SetItemTitle(UGCUpdateHandle_t handle, string pchTitle) { return false; }
        public static bool SetItemDescription(UGCUpdateHandle_t handle, string pchDescription) { return false; }
        public static bool SetItemContent(UGCUpdateHandle_t handle, string pszContentFolder) { return false; }
        public static bool SetItemPreview(UGCUpdateHandle_t handle, string pszPreviewFile) { return false; }
        public static bool SetItemTags(UGCUpdateHandle_t updateHandle, System.Collections.Generic.IList<string> pTags) { return false; }

        public static SteamAPICall_t SubmitItemUpdate(UGCUpdateHandle_t handle, string pchChangeNote)
        {
            return SteamAPICall_t.Invalid;
        }

        public static EItemUpdateStatus GetItemUpdateProgress(UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal)
        {
            punBytesProcessed = 0;
            punBytesTotal = 0;
            return EItemUpdateStatus.k_EItemUpdateStatusInvalid;
        }

        public static bool GetItemDownloadInfo(PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal)
        {
            punBytesDownloaded = 0;
            punBytesTotal = 0;
            return false;
        }

        public static uint GetItemState(PublishedFileId_t nPublishedFileID) { return 0; }

        public static bool DownloadItem(PublishedFileId_t nPublishedFileID, bool bHighPriority) { return false; }
    }
}
