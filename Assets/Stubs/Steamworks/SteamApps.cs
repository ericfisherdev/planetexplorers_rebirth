// Stub: Steamworks.SteamApps - application information and DLC
// All methods return safe defaults.

namespace Steamworks
{
    public static class SteamApps
    {
        public static string GetCurrentGameLanguage() { return "english"; }

        public static bool BIsSubscribedApp(AppId_t appID) { return false; }

        public static bool BIsDlcInstalled(AppId_t appID) { return false; }

        public static string GetAvailableGameLanguages() { return "english"; }

        public static bool BIsSubscribed() { return false; }
    }
}
