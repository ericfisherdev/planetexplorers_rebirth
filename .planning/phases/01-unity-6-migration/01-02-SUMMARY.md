---
phase: 01-unity-6-migration
plan: 02
subsystem: engine-migration
tags: [unity6, scenemanager, unitywebrequest, cross-platform, deprecated-api]

# Dependency graph
requires:
  - phase: none
    provides: none
provides:
  - SceneManager-based scene loading across all gameplay files
  - Cross-platform HighStopwatch using System.Diagnostics.Stopwatch
  - UnityWebRequest-based asset and audio loading
affects: [01-03, 01-04, 02-plugin-stubs]

# Tech tracking
tech-stack:
  added: [UnityEngine.SceneManagement, UnityEngine.Networking, System.Diagnostics.Stopwatch]
  patterns: [SceneManager.LoadScene for scene transitions, SceneManager.GetActiveScene().name for scene name queries, UnityWebRequestAssetBundle for cached bundle loading, UnityWebRequestMultimedia for audio clip loading]

key-files:
  created: []
  modified:
    - Assets/Resources/Movie/EDRunner.cs
    - Assets/PeLauncher/PeFlowMgr.cs
    - Assets/Scripts/Mission/StroyManager.cs
    - Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/NGUIScripts/TitleMenuGui_N.cs
    - Assets/Scripts/AiScripts/AiSpawn/SPPoint.cs
    - Assets/Scripts/AiScripts/AiSpawn/SPTerrainEvent.cs
    - Assets/Scripts/AiScripts/AiSpawn/SPPlayerSummon.cs
    - Assets/Scripts/Global/GlobalBehaviour.cs
    - Assets/Scripts/ZhouXun/Misc/HighStopwatch.cs
    - Assets/Scripts/AssetsLoader/AssetsLoader.cs
    - Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/PhoneWnd/RadioManager.cs

key-decisions:
  - "HighStopwatch uses Stopwatch.GetTimestamp() (absolute counter) not elapsed ticks, preserving delta-timing semantics used by LagTester"
  - "RadioManager LoadFileByUnity uses UnityWebRequestMultimedia.GetAudioClip with AudioType.UNKNOWN for format auto-detection"
  - "Commented-out deprecated API calls left untouched per plan instructions"

patterns-established:
  - "Scene transitions: SceneManager.LoadScene(name) with using UnityEngine.SceneManagement"
  - "Scene name queries: SceneManager.GetActiveScene().name"
  - "Asset bundle loading: UnityWebRequestAssetBundle.GetAssetBundle with DownloadHandlerAssetBundle.GetContent"
  - "Audio loading: UnityWebRequestMultimedia.GetAudioClip with DownloadHandlerAudioClip.GetContent"

requirements-completed: [ENV-01]

# Metrics
duration: 3min
completed: 2026-03-28
---

# Phase 1 Plan 2: Replace Deprecated APIs Summary

**Replaced Application.LoadLevel/loadedLevelName with SceneManager, kernel32 P/Invoke with System.Diagnostics.Stopwatch, and WWW class with UnityWebRequest across 11 files**

## Performance

- **Duration:** 3 min
- **Started:** 2026-03-28T20:12:09Z
- **Completed:** 2026-03-28T20:15:30Z
- **Tasks:** 2
- **Files modified:** 11

## Accomplishments
- Replaced all active Application.LoadLevel and Application.loadedLevelName calls with SceneManager equivalents across 8 files
- Replaced Windows-only HighStopwatch kernel32.dll P/Invoke with cross-platform System.Diagnostics.Stopwatch
- Replaced WWW class with UnityWebRequest in AssetsLoader (asset bundles) and RadioManager (raw bytes + audio clips)

## Task Commits

Each task was committed atomically:

1. **Task 1: Replace deprecated scene management APIs** - `1df7e5e5` (feat)
2. **Task 2: Replace WWW class and kernel32 P/Invoke** - `25aecb79` (feat)

## Files Created/Modified
- `Assets/Resources/Movie/EDRunner.cs` - LoadLevel -> SceneManager.LoadScene
- `Assets/PeLauncher/PeFlowMgr.cs` - LoadLevel -> SceneManager.LoadScene
- `Assets/Scripts/Mission/StroyManager.cs` - LoadLevel -> SceneManager.LoadScene
- `Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/NGUIScripts/TitleMenuGui_N.cs` - LoadLevel -> SceneManager.LoadScene
- `Assets/Scripts/AiScripts/AiSpawn/SPPoint.cs` - loadedLevelName -> GetActiveScene().name
- `Assets/Scripts/AiScripts/AiSpawn/SPTerrainEvent.cs` - loadedLevelName -> GetActiveScene().name
- `Assets/Scripts/AiScripts/AiSpawn/SPPlayerSummon.cs` - loadedLevelName -> GetActiveScene().name
- `Assets/Scripts/Global/GlobalBehaviour.cs` - loadedLevelName -> GetActiveScene().name
- `Assets/Scripts/ZhouXun/Misc/HighStopwatch.cs` - kernel32 P/Invoke -> System.Diagnostics.Stopwatch
- `Assets/Scripts/AssetsLoader/AssetsLoader.cs` - WWW.LoadFromCacheOrDownload -> UnityWebRequestAssetBundle
- `Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/PhoneWnd/RadioManager.cs` - new WWW -> UnityWebRequest.Get and UnityWebRequestMultimedia.GetAudioClip

## Decisions Made
- HighStopwatch uses `Stopwatch.GetTimestamp()` (returns absolute counter value) rather than `Stopwatch.ElapsedTicks` -- the original code's `Value` property returned the raw performance counter, and `LagTester` computes deltas between successive calls
- RadioManager's `LoadFileByUnity` uses `UnityWebRequestMultimedia.GetAudioClip` with `AudioType.UNKNOWN` for automatic format detection, matching the original WWW behavior
- Commented-out deprecated API calls in SceneMediator.cs, LoadGui_N.cs, SPTerrainRect.cs left untouched per plan instructions

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 3 - Blocking] HighStopwatch at different path than plan specified**
- **Found during:** Task 2
- **Issue:** Plan referenced `Assets/Scripts/Assist/HighStopwatch.cs` but file is at `Assets/Scripts/ZhouXun/Misc/HighStopwatch.cs`
- **Fix:** Used actual file path
- **Files modified:** Assets/Scripts/ZhouXun/Misc/HighStopwatch.cs
- **Verification:** grep confirms no kernel32 or DllImport references remain
- **Committed in:** 25aecb79

**2. [Rule 2 - Missing Critical] AssetsLoader WWW.LoadFromCacheOrDownload not mentioned in plan counts**
- **Found during:** Task 2
- **Issue:** Plan said "3 occurrences in 2 files" for WWW but `WWW.LoadFromCacheOrDownload` in AssetsLoader is a different pattern than `new WWW()` -- replaced it with `UnityWebRequestAssetBundle.GetAssetBundle`
- **Fix:** Used the correct UnityWebRequestAssetBundle API for cached asset bundle loading
- **Files modified:** Assets/Scripts/AssetsLoader/AssetsLoader.cs
- **Verification:** No WWW references remain in any .cs file
- **Committed in:** 25aecb79

---

**Total deviations:** 2 auto-fixed (1 blocking, 1 missing critical)
**Impact on plan:** Both necessary for correctness. No scope creep.

## Issues Encountered
- No Application.isLoadingLevel or Application.levelCount usages found in active code (all were in comments) -- no replacements needed for those patterns
- Plan expected ~34 occurrences in ~18 files but actual count was lower due to many being in comments

## User Setup Required
None - no external service configuration required.

## Next Phase Readiness
- All manually-fixable deprecated APIs replaced
- Unity API Updater only needs to handle component accessors (.rigidbody, .collider) and FindObjectsOfType when project opens in Unity 6
- Ready for Plan 01-03 (compile error triage) and Plan 01-04 (validation)

---
*Phase: 01-unity-6-migration*
*Completed: 2026-03-28*

## Self-Check: PASSED
