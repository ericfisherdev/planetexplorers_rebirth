---
phase: 03-clean-compilation
plan: 02
subsystem: networking
tags: [ulink, single-player, game-mode, network-stubs]

# Dependency graph
requires:
  - phase: 02-proprietary-plugin-stubs
    provides: uLink and uLobby stub types (MonoBehaviour, NetworkView, Network, BitStream)
provides:
  - Single-player default game mode (PeGameMgr.IsSingle=true, IsMulti=false)
  - Guarded network connection paths (no live uLink calls in single-player)
  - Safe offline defaults for NetworkInterface, GameClientNetwork, GameClientLobby
affects: [03-clean-compilation, 04-terrain-world-generation, 05-player-game-loop-bootstrap]

# Tech tracking
tech-stack:
  added: []
  patterns: [early-return guard pattern for multiplayer code paths]

key-files:
  created: []
  modified:
    - Assets/PeLauncher/PeGameMgr.cs
    - Assets/Scripts/GameNetwork/NetworkInterface.cs
    - Assets/Scripts/GameNetwork/GameClient/GameClientNetwork.cs
    - Assets/Scripts/GameNetwork/GameClient/GameClientLobby.cs

key-decisions:
  - "No code changes needed for PeGameMgr -- mPlayerType already defaults to EPlayerType.Single"
  - "Used early-return guards (if !IsMulti return) rather than wrapping entire method bodies in conditionals"
  - "GameConfig.IsMultiMode properly delegates to PeGameMgr.IsMulti -- no independent multiplayer state"
  - "PlayerNetwork.mainPlayer/mainPlayerId left at null/0 defaults -- callers already null-check"
  - "LocalTest.cs already wrapped in #if LOCALTEST -- no changes needed"

patterns-established:
  - "Guard pattern: multiplayer-only methods start with 'if (!Pathea.PeGameMgr.IsMulti) return;'"
  - "NetworkInterface.Connect() guards use GameConfig.IsMultiMode for consistency with call sites"

requirements-completed: [CORE-04]

# Metrics
duration: 3min
completed: 2026-03-29
---

# Phase 3 Plan 2: Wire Networking for Single-Player Operation Summary

**All multiplayer code paths guarded with IsMulti early-returns so single-player mode never hits uLink network calls**

## Performance

- **Duration:** 3 min
- **Started:** 2026-03-29T01:23:36Z
- **Completed:** 2026-03-29T01:26:35Z
- **Tasks:** 2
- **Files modified:** 4

## Accomplishments
- Verified PeGameMgr.mPlayerType already defaults to EPlayerType.Single (IsSingle=true, IsMulti=false at startup)
- Added IsMulti/IsMultiMode early-return guards to all 6 network entry points (NetworkInterface.Connect x2, GameClientNetwork.Start, GameClientNetwork.Connect, GameClientLobby.Start, GameClientLobby.ConnectToLobby)
- Confirmed GameConfig.IsMultiMode delegates to PeGameMgr.IsMulti with no independent state
- Confirmed PlayerNetwork static fields default to safe values (mainPlayer=null, mainPlayerId=0)
- Confirmed LocalTest.cs is already disabled via #if LOCALTEST conditional compilation

## Task Commits

Each task was committed atomically:

1. **Task 1: Force single-player defaults in PeGameMgr and verify GameConfig routing** - `d98acac4` (feat)
2. **Task 2: Ensure NetworkInterface and PlayerNetwork compile and default to offline single-player behavior** - `8b74070c` (feat)

## Files Created/Modified
- `Assets/PeLauncher/PeGameMgr.cs` - Added clarifying comment on mPlayerType default (already EPlayerType.Single)
- `Assets/Scripts/GameNetwork/NetworkInterface.cs` - Added IsMultiMode guards to both Connect() overloads
- `Assets/Scripts/GameNetwork/GameClient/GameClientNetwork.cs` - Added IsMulti guards to Start() and Connect()
- `Assets/Scripts/GameNetwork/GameClient/GameClientLobby.cs` - Added IsMulti guards to Start() and ConnectToLobby()

## Decisions Made
- No code changes needed for PeGameMgr -- the mPlayerType field already initializes to EPlayerType.Single, satisfying all IsMulti/IsSingle checks throughout the codebase
- Used early-return guard pattern rather than wrapping method bodies in conditionals -- cleaner, less indentation, easier to read
- GameConfig.IsMultiClient/IsMultiServer rely on uLink stubs returning false -- no additional guards needed since the stubs handle it
- PlayerNetwork and LocalTest required no modifications -- defaults are already safe for single-player

## Deviations from Plan

None - plan executed exactly as written.

## Issues Encountered

None

## User Setup Required

None - no external service configuration required.

## Known Stubs

None - no stubs introduced in this plan. Existing uLink stubs from Phase 2 are relied upon but not modified.

## Next Phase Readiness
- All networking code paths safely no-op in single-player mode
- Ready for 03-03 (iterative compilation error triage and Play mode verification)
- The ~100+ files checking IsMulti/IsMultiMode/IsClient will all take the single-player branch by default

---
*Phase: 03-clean-compilation*
*Completed: 2026-03-29*
