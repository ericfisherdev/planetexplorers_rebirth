// Stub: Steamworks.SteamUserStats - achievements, stats, and leaderboards
// All methods return safe defaults. No-ops in stub mode.

namespace Steamworks
{
    public static class SteamUserStats
    {
        public static bool RequestCurrentStats() { return false; }

        public static bool GetStat(string pchName, out int pData) { pData = 0; return false; }
        public static bool GetStat(string pchName, out float pData) { pData = 0f; return false; }

        public static bool SetStat(string pchName, int nData) { return false; }
        public static bool SetStat(string pchName, float fData) { return false; }

        public static bool StoreStats() { return false; }

        public static bool SetAchievement(string pchName) { return false; }
        public static bool ClearAchievement(string pchName) { return false; }

        public static bool GetAchievement(string pchName, out bool pbAchieved)
        {
            pbAchieved = false;
            return false;
        }

        public static string GetAchievementDisplayAttribute(string pchName, string pchKey)
        {
            return string.Empty;
        }

        public static SteamAPICall_t FindOrCreateLeaderboard(
            string pchLeaderboardName,
            ELeaderboardSortMethod eLeaderboardSortMethod,
            ELeaderboardDisplayType eLeaderboardDisplayType)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t UploadLeaderboardScore(
            SteamLeaderboard_t hSteamLeaderboard,
            ELeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod,
            int nScore,
            int[] pScoreDetails,
            int cScoreDetailsCount)
        {
            return SteamAPICall_t.Invalid;
        }

        public static SteamAPICall_t DownloadLeaderboardEntries(
            SteamLeaderboard_t hSteamLeaderboard,
            ELeaderboardDataRequest eLeaderboardDataRequest,
            int nRangeStart,
            int nRangeEnd)
        {
            return SteamAPICall_t.Invalid;
        }

        public static bool GetDownloadedLeaderboardEntry(
            SteamLeaderboardEntries_t hSteamLeaderboardEntries,
            int index,
            out LeaderboardEntry_t pLeaderboardEntry,
            int[] pDetails,
            int cDetailsMax)
        {
            pLeaderboardEntry = new LeaderboardEntry_t();
            return false;
        }
    }

    public enum ELeaderboardUploadScoreMethod
    {
        k_ELeaderboardUploadScoreMethodNone = 0,
        k_ELeaderboardUploadScoreMethodKeepBest = 1,
        k_ELeaderboardUploadScoreMethodForceUpdate = 2
    }
}
