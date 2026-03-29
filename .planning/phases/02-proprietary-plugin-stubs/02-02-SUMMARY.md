---
phase: 02-proprietary-plugin-stubs
plan: 02
subsystem: networking
tags: [ulink, ulobby, steamworks, stubs, single-player, networking]

# Dependency graph
requires:
  - phase: 01-unity6-api-migration
    provides: Unity 6 API compatibility for MonoBehaviour base class
provides:
  - uLink networking stubs (MonoBehaviour, BitStream, NetworkView, Network, etc.)
  - uLobby lobby/matchmaking stubs (Lobby, ServerInfo, ServerRegistry)
  - Steamworks.NET platform integration stubs (SteamAPI, SteamFriends, SteamUGC, etc.)
affects: [03-compilation-fixes, 05-player-game-loop-bootstrap]

# Tech tracking
tech-stack:
  added: []
  patterns: [stub-with-single-player-defaults, no-op-networking-layer]

key-files:
  created:
    - Assets/Stubs/uLink/MonoBehaviour.cs
    - Assets/Stubs/uLink/BitStream.cs
    - Assets/Stubs/uLink/Network.cs
    - Assets/Stubs/uLink/NetworkView.cs
    - Assets/Stubs/uLink/NetworkTypes.cs
    - Assets/Stubs/uLobby/Lobby.cs
    - Assets/Stubs/Steamworks/SteamTypes.cs
    - Assets/Stubs/Steamworks/SteamAPI.cs
    - Assets/Stubs/Steamworks/SteamRemoteStorage.cs
    - Assets/Stubs/Steamworks/Callback.cs
    - Assets/Stubs/Steamworks/CallResult.cs
  modified: []

key-decisions:
  - "uLink.MonoBehaviour returns isMine=true, isServer=true so local-player code paths execute correctly"
  - "BitStream.Read<T>() returns default(T) for all types -- safe for single-player where no serialization occurs"
  - "SteamManager.cs stub is documentation-only to avoid duplicate class with existing Assets/Scripts/Steamwork.NET/SteamManager.cs"
  - "Added SteamRemoteStorage stub (Rule 2) -- 30+ references in game code, omitted from plan"

patterns-established:
  - "Stub pattern: namespace matches original, classes/structs match original API surface, all methods return safe defaults"
  - "Single-player defaults: isMine=true, isServer=true, isClient=false for all networking stubs"

requirements-completed: [ENV-02]

# Metrics
duration: 9min
completed: 2026-03-29
---

# Phase 02 Plan 02: uLink, uLobby, and Steamworks.NET Stubs Summary

**No-op networking and platform stubs with single-player defaults (isMine=true, isServer=true) covering 681 BitStream refs, 592 NetworkMessageInfo refs, and 28 Steamworks files**

## Performance

- **Duration:** 9 min
- **Started:** 2026-03-29T01:10:51Z
- **Completed:** 2026-03-29T01:19:33Z
- **Tasks:** 2
- **Files modified:** 30

## Accomplishments
- uLink stubs (12 files) covering MonoBehaviour, BitStream (53 Read/Write methods), NetworkView, Network, NetworkTypes, MasterServer, HostData, RPCMode, RegisterPrefabs, BitStreamCodec, NetworkMessageInfo, NetworkViewID
- uLobby stubs (4 files) covering Lobby with events, ServerInfo, ServerRegistry, LobbyConnectionError
- Steamworks.NET stubs (14 files) covering SteamAPI, SteamTypes (CSteamID, AppId_t, PublishedFileId_t, EResult, 15+ callback structs), SteamFriends, SteamUserStats, SteamUser, SteamUGC, SteamRemoteStorage, SteamUtils, SteamMatchmaking, SteamClient, SteamApps, Callback<T>, CallResult<T>

## Task Commits

Each task was committed atomically:

1. **Task 1: Create uLink and uLobby networking stubs** - `b3c96fb6` (feat)
2. **Task 2: Create Steamworks.NET stubs** - `48dceebb` (feat)

## Files Created/Modified
- `Assets/Stubs/uLink/MonoBehaviour.cs` - Base class extending UnityEngine.MonoBehaviour with single-player defaults
- `Assets/Stubs/uLink/BitStream.cs` - Network serialization stream with 53 Read/Write methods returning defaults
- `Assets/Stubs/uLink/NetworkMessageInfo.cs` - Message metadata struct (sender, timestamp, networkView)
- `Assets/Stubs/uLink/BitStreamCodec.cs` - Custom type serializer registration (no-op)
- `Assets/Stubs/uLink/Network.cs` - Static network management with NetworkConfig, isServer=true, isClient=false
- `Assets/Stubs/uLink/NetworkView.cs` - NetworkView component with Find(), RPC(), initialData
- `Assets/Stubs/uLink/NetworkViewID.cs` - View ID struct with equality operators
- `Assets/Stubs/uLink/RPCMode.cs` - RPC target mode enum
- `Assets/Stubs/uLink/NetworkTypes.cs` - NetworkPlayer, NetworkPeer, NetworkStatus, NetworkLog, PublicKey, etc.
- `Assets/Stubs/uLink/MasterServer.cs` - Server discovery (returns empty host lists)
- `Assets/Stubs/uLink/HostData.cs` - Server info data class
- `Assets/Stubs/uLink/RegisterPrefabs.cs` - Prefab registration MonoBehaviour (no-op)
- `Assets/Stubs/uLobby/Lobby.cs` - Lobby connection with events (isConnected=false)
- `Assets/Stubs/uLobby/ServerInfo.cs` - Server info data class
- `Assets/Stubs/uLobby/ServerRegistry.cs` - Server registry returning empty collections
- `Assets/Stubs/uLobby/LobbyConnectionError.cs` - Connection error enum
- `Assets/Stubs/Steamworks/SteamTypes.cs` - CSteamID, CGameID, AppId_t, PublishedFileId_t, UGCHandle_t, HAuthTicket, EResult, 15+ callback/result structs, Packsize, DllCheck
- `Assets/Stubs/Steamworks/SteamAPI.cs` - Init() returns true with warning, RestartAppIfNecessary returns false
- `Assets/Stubs/Steamworks/SteamManager.cs` - Documentation-only (no duplicate class)
- `Assets/Stubs/Steamworks/SteamFriends.cs` - Friend list and social features
- `Assets/Stubs/Steamworks/SteamUserStats.cs` - Achievements, stats, leaderboards
- `Assets/Stubs/Steamworks/SteamUser.cs` - User identity and auth tickets
- `Assets/Stubs/Steamworks/SteamUtils.cs` - App ID, image data, overlay
- `Assets/Stubs/Steamworks/SteamUGC.cs` - Workshop UGC query/create/update/download
- `Assets/Stubs/Steamworks/SteamRemoteStorage.cs` - Cloud storage, UGC download, workshop publishing
- `Assets/Stubs/Steamworks/SteamMatchmaking.cs` - Matchmaking and lobbies
- `Assets/Stubs/Steamworks/SteamClient.cs` - Warning message hook
- `Assets/Stubs/Steamworks/SteamApps.cs` - App info and DLC checks
- `Assets/Stubs/Steamworks/Callback.cs` - Generic callback registration (stores handler, never fires)
- `Assets/Stubs/Steamworks/CallResult.cs` - Async call result handler (stores handler, never fires)

## Decisions Made
- uLink.MonoBehaviour returns isMine=true, isServer=true so existing code selects local-player branches
- BitStream generic Read<T>() returns default(T) -- safe because no actual network data exists in single-player
- SteamManager.cs is documentation-only to avoid class collision with the existing one in Assets/Scripts/Steamwork.NET/
- SteamAPI.Init() returns true to prevent early Application.Quit() in SteamManager.Awake()

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 2 - Missing Critical] Added SteamRemoteStorage stub**
- **Found during:** Task 2 (Steamworks.NET stubs)
- **Issue:** SteamRemoteStorage has 30+ references across game code (FileDelete, UGCRead, UGCDownload, EnumeratePublishedWorkshopFiles, etc.) but was not listed in the plan
- **Fix:** Created Assets/Stubs/Steamworks/SteamRemoteStorage.cs with all referenced methods plus supporting enums (EWorkshopEnumerationType, EUGCReadAction)
- **Files modified:** Assets/Stubs/Steamworks/SteamRemoteStorage.cs
- **Verification:** All SteamRemoteStorage methods referenced in grep are covered
- **Committed in:** 48dceebb (part of Task 2 commit)

---

**Total deviations:** 1 auto-fixed (1 missing critical)
**Impact on plan:** Essential for compilation -- 30+ references would produce CS0246 errors without this stub. No scope creep.

## Issues Encountered
None

## User Setup Required
None - no external service configuration required.

## Known Stubs
This entire plan creates intentional stubs. All stubs are no-op by design for single-player mode. They will be replaced incrementally if multiplayer support is added in future phases.

## Next Phase Readiness
- All uLink, uLobby, and Steamworks.NET types are stubbed for compilation
- Combined with other Phase 2 plans (NGUI, FMOD, FinalIK, Behave, A*), these stubs will eliminate the majority of CS0246 missing-type errors
- Ready for Phase 3 compilation fixes

---
*Phase: 02-proprietary-plugin-stubs*
*Completed: 2026-03-29*

## Self-Check: PASSED
- All 10 key files verified present on disk
- Both task commits (b3c96fb6, 48dceebb) verified in git log
