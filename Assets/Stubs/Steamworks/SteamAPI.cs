// Stub: Steamworks.SteamAPI - Steam platform initialization and lifecycle
// Init() returns true but logs a warning. All other methods are no-ops.

using UnityEngine;

namespace Steamworks
{
    public static class SteamAPI
    {
        /// <summary>Initialize the Steam API. Returns true (stubbed) with a log warning.</summary>
        public static bool Init()
        {
            Debug.LogWarning("[Steamworks Stub] SteamAPI.Init() called -- Steam integration is stubbed for standalone build.");
            return true;
        }

        /// <summary>Shut down the Steam API. No-op in stub mode.</summary>
        public static void Shutdown() { }

        /// <summary>Run Steam callbacks. No-op in stub mode.</summary>
        public static void RunCallbacks() { }

        /// <summary>Check if Steam is running. Returns false in stub mode.</summary>
        public static bool IsSteamRunning() { return false; }

        /// <summary>Check if app needs to restart through Steam. Returns false (no restart needed).</summary>
        public static bool RestartAppIfNecessary(AppId_t unOwnAppID) { return false; }
    }
}
