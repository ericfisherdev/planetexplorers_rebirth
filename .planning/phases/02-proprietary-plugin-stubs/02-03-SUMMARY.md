---
phase: 02-proprietary-plugin-stubs
plan: 03
subsystem: stubs
tags: [finalik, astar-pathfinding, behave, fmod, ik-solvers, behavior-trees, audio, navigation]

# Dependency graph
requires:
  - phase: 01-unity6-api-migration
    provides: "Clean Unity 6 API-compatible codebase to stub against"
provides:
  - "RootMotion.FinalIK namespace stubs (AimIK, FullBodyBipedIK, CCDIK, GrounderFBBIK, IK base, solver types)"
  - "Pathfinding namespace stubs (AstarPath, Seeker, Path/ABPath/RandomPath, GridGraph, LayerGridGraph, RVO, Ionic.Zlib)"
  - "Behave.Runtime stubs (BehaveResult enum, IAgent interface, Tree class, TickForward/ResetForward delegates)"
  - "Behave namespace stubs (EPriority, EState enums, Behave.Assets types)"
  - "FMOD/FMOD.Studio namespace stubs (EventDescription, EventInstance, Bank, Bus, VCA, System)"
  - "FMOD Unity integration stubs (FMODAudioSource, FMODAudioListener, FMODAsset, FMOD_StudioSystem)"
affects: [03-networking-stubs, 04-terrain-world-generation, 05-player-game-loop-bootstrap, 06-combat-ai-systems]

# Tech tracking
tech-stack:
  added: []
  patterns: [ik-solver-inheritance-chain, singleton-pathfinding, delegate-forwarding-stubs, fmod-out-parameter-pattern]

key-files:
  created:
    - Assets/Stubs/FinalIK/IKTypes.cs
    - Assets/Stubs/FinalIK/AimIK.cs
    - Assets/Stubs/FinalIK/FullBodyBipedIK.cs
    - Assets/Stubs/FinalIK/CCDIK.cs
    - Assets/Stubs/FinalIK/GrounderFBBIK.cs
    - Assets/Stubs/FinalIK/IKSolvers.cs
    - Assets/Stubs/AstarPathfinding/AstarPath.cs
    - Assets/Stubs/AstarPathfinding/Seeker.cs
    - Assets/Stubs/AstarPathfinding/Path.cs
    - Assets/Stubs/AstarPathfinding/RandomPath.cs
    - Assets/Stubs/AstarPathfinding/GraphTypes.cs
    - Assets/Stubs/AstarPathfinding/RVO.cs
    - Assets/Stubs/AstarPathfinding/Ionic.cs
    - Assets/Stubs/Behave/BehaveRuntime.cs
    - Assets/Stubs/FMOD/FMODAudioSource.cs
    - Assets/Stubs/FMOD/FMODAudioListener.cs
    - Assets/Stubs/FMOD/FMODAsset.cs
    - Assets/Stubs/FMOD/FMOD_StudioSystem.cs
    - Assets/Stubs/FMOD/FMODStudio.cs
  modified: []

key-decisions:
  - "FinalIK IK base class exposes Disable/Enable methods instead of relying on MonoBehaviour.enabled alone -- game code calls ik.Disable() directly"
  - "Ionic.Zlib.ZlibStream delegates to System.IO.Compression.DeflateStream for functional voxel compression rather than pure no-op"
  - "Tree.SetInitForward/SetTickForward/SetResetForward with TickForward/ResetForward delegates stubbed to match reflection-based BTAgent forwarding pattern"
  - "FMODEditorExtension added as Rule 2 deviation -- referenced by FMODEventInspector but not in original plan file list"

patterns-established:
  - "IK solver inheritance: IK (MonoBehaviour) -> typed solver property -> IKSolver subclass with shared IKPosition/IKPositionWeight/axis/target"
  - "A* Path callback pattern: Seeker.pathCallback += OnPathComplete; Seeker.StartPath(start, end)"
  - "FMOD out-parameter pattern: all FMOD.Studio methods return RESULT and use out parameters for data"
  - "Behave delegate forwarding: Tree.SetTickForward(index, delegate) maps action indices to method delegates via reflection"

requirements-completed: [ENV-02]

# Metrics
duration: 7min
completed: 2026-03-29
---

# Phase 2 Plan 3: Gameplay Middleware Stubs Summary

**FinalIK, A* Pathfinding, Behave (2110-ref BehaveResult), and FMOD stubs providing full API surfaces for IK solvers, navigation, behavior trees, and audio**

## Performance

- **Duration:** 7 min
- **Started:** 2026-03-29T01:10:54Z
- **Completed:** 2026-03-29T01:17:25Z
- **Tasks:** 2
- **Files created:** 19

## Accomplishments
- FinalIK stubs with full IK solver inheritance chain (IK -> IKSolver -> typed solvers) matching .solver.IKPositionWeight, .solver.axis, .solver.target access patterns across 71+ AimIK references
- A* Pathfinding stubs covering AstarPath singleton (66 refs), Seeker/Path/ABPath/RandomPath, GridGraph/LayerGridGraph with center/width/depth, RVOController no-op, and Ionic.Zlib functional compression wrapper
- Behave runtime stubs for BehaveResult enum (2110 references), IAgent interface, Tree class with Frequency/Tick/Reset/forward delegates, EPriority/EState enums, and Behave.Assets editor types
- FMOD stubs covering FMODAudioSource/FMODAudioListener MonoBehaviours, FMOD.Studio namespace (EventDescription, EventInstance, ParameterInstance, Bank, Bus, VCA, System), and FMODEditorExtension for editor code

## Task Commits

Each task was committed atomically:

1. **Task 1: Create FinalIK and A* Pathfinding stubs** - `c3c68772` (feat)
2. **Task 2: Create Behave runtime and FMOD stubs** - `07f5e4b9` (feat)

## Files Created/Modified
- `Assets/Stubs/FinalIK/IKTypes.cs` - IK, IKSolver, IKSolverFullBodyBiped, IKSolverAim, IKSolverCCD, IKEffector, IKMapping base types
- `Assets/Stubs/FinalIK/AimIK.cs` - AimIK MonoBehaviour with IKSolverAim solver property
- `Assets/Stubs/FinalIK/FullBodyBipedIK.cs` - FullBodyBipedIK MonoBehaviour with IKSolverFullBodyBiped solver
- `Assets/Stubs/FinalIK/CCDIK.cs` - CCDIK MonoBehaviour with IKSolverCCD solver
- `Assets/Stubs/FinalIK/GrounderFBBIK.cs` - Ground alignment MonoBehaviour stub
- `Assets/Stubs/FinalIK/IKSolvers.cs` - Reserved for additional solver types
- `Assets/Stubs/AstarPathfinding/AstarPath.cs` - AstarPath singleton with active, StartPath, UpdateGraphs, GetNearest, AstarData
- `Assets/Stubs/AstarPathfinding/Seeker.cs` - Seeker MonoBehaviour with pathCallback, StartPath, SimpleSmoothModifier
- `Assets/Stubs/AstarPathfinding/Path.cs` - Path, ABPath, NNConstraint, PathNNConstraint, GraphUpdateObject
- `Assets/Stubs/AstarPathfinding/RandomPath.cs` - RandomPath with Construct(start, length, callback)
- `Assets/Stubs/AstarPathfinding/GraphTypes.cs` - NavGraph, GridGraph, LayerGridGraph, GraphNode, NNInfo
- `Assets/Stubs/AstarPathfinding/RVO.cs` - Pathfinding.RVO.RVOController no-op
- `Assets/Stubs/AstarPathfinding/Ionic.cs` - Pathfinding.Ionic.Zlib.ZlibStream wrapping DeflateStream
- `Assets/Stubs/Behave/BehaveRuntime.cs` - BehaveResult, IAgent, Tree, TickForward/ResetForward, EPriority, EState, Behave.Assets types
- `Assets/Stubs/FMOD/FMODAudioSource.cs` - FMODAudioSource MonoBehaviour with path, volume, audioInst
- `Assets/Stubs/FMOD/FMODAudioListener.cs` - FMODAudioListener singleton MonoBehaviour
- `Assets/Stubs/FMOD/FMODAsset.cs` - FMODAsset ScriptableObject with path and id
- `Assets/Stubs/FMOD/FMOD_StudioSystem.cs` - FMOD_StudioSystem singleton with System property
- `Assets/Stubs/FMOD/FMODStudio.cs` - FMOD/FMOD.Studio namespace types, FMODEditorExtension

## Decisions Made
- FinalIK IK base class provides Disable/Enable methods matching game code's direct `ik.Disable()` calls
- Ionic.Zlib.ZlibStream wraps System.IO.Compression.DeflateStream for functional compression -- not a pure no-op because voxel data compression is used at runtime
- Behave Tree delegate forwarding stubs (SetInitForward, SetTickForward, SetResetForward) match the reflection-based pattern in BTAgent.SetTreeForward
- FMOD.Studio types use out-parameter pattern matching the real FMOD API convention

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 2 - Missing Critical] Added FMODEditorExtension static class**
- **Found during:** Task 2 (FMOD stubs)
- **Issue:** FMODEventInspector.cs references FMODEditorExtension.GetEventDescription, .AuditionEvent, .StopEvent, .SetEventParameterValue -- but this class was not in the plan's file list
- **Fix:** Added FMODEditorExtension as a static class in FMODStudio.cs under #if UNITY_EDITOR guard
- **Files modified:** Assets/Stubs/FMOD/FMODStudio.cs
- **Verification:** All 6 FMODEditorExtension references in Assets/Editor/FMODEventInspector.cs now have matching stubs
- **Committed in:** 07f5e4b9 (Task 2 commit)

**2. [Rule 2 - Missing Critical] Added SimpleSmoothModifier stub**
- **Found during:** Task 1 (A* Pathfinding stubs)
- **Issue:** PEPathfinder.cs has [RequireComponent(typeof(SimpleSmoothModifier))] but SimpleSmoothModifier was not in the plan
- **Fix:** Added SimpleSmoothModifier as empty MonoBehaviour in Seeker.cs (same namespace Pathfinding)
- **Files modified:** Assets/Stubs/AstarPathfinding/Seeker.cs
- **Verification:** grep confirms the type exists in stubs
- **Committed in:** c3c68772 (Task 1 commit)

---

**Total deviations:** 2 auto-fixed (2 missing critical)
**Impact on plan:** Both auto-fixes necessary for compilation completeness. No scope creep.

## Issues Encountered
None

## Known Stubs
None -- all stubs are intentionally no-op implementations per project strategy (compile first, replace incrementally).

## User Setup Required
None - no external service configuration required.

## Next Phase Readiness
- All gameplay middleware types now have compilation stubs
- Ready for Phase 2 Plan 4 (remaining stubs) or Phase 3+ compilation work
- IK, pathfinding, behavior tree, and audio code paths can be compiled and incrementally replaced

## Self-Check: PASSED

All 19 created files verified present on disk. Both task commits (c3c68772, 07f5e4b9) verified in git log.

---
*Phase: 02-proprietary-plugin-stubs*
*Completed: 2026-03-29*
