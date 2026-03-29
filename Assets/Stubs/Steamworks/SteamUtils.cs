// Stub: Steamworks.SteamUtils - utility functions (app ID, image data, overlay)
// All methods return safe defaults.

namespace Steamworks
{
    public static class SteamUtils
    {
        public static AppId_t GetAppID() { return new AppId_t(480); }

        public static string GetIPCountry() { return "US"; }

        public static bool IsOverlayEnabled() { return false; }

        public static void SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition) { }

        public static bool GetImageSize(int iImage, out uint pnWidth, out uint pnHeight)
        {
            pnWidth = 0;
            pnHeight = 0;
            return false;
        }

        public static bool GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize)
        {
            return false;
        }

        public static uint GetServerRealTime() { return 0; }

        public static bool IsSteamRunningInVR() { return false; }
    }
}
