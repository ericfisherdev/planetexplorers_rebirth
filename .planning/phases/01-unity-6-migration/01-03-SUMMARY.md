---
phase: 01-unity-6-migration
plan: 03
subsystem: engine-migration
tags: [unity6, particle-system, deprecated-api, FindObjectsByType]

# Dependency graph
requires:
  - phase: 01-unity-6-migration (plans 01, 02)
    provides: UnityScript removal and batch deprecated API fixes
provides:
  - Zero legacy particle system class references (ParticleEmitter, ParticleAnimator, ParticleRenderer)
  - Zero FindObjectsOfType/FindObjectOfType calls (replaced with Unity 6 equivalents)
affects: [01-04-unity-editor-verification, phase-2-plugin-stubs]

# Tech tracking
tech-stack:
  added: []
  patterns:
    - "ParticleSystemRenderer replaces ParticleRenderer in type checks"
    - "FindObjectsByType(FindObjectsSortMode.None) replaces FindObjectsOfType"
    - "FindFirstObjectByType replaces FindObjectOfType"

key-files:
  created: []
  modified:
    - Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Misc/ParticleScaler.cs
    - Assets/Scripts/ZhouXun/Voxel Creation/Scripts/WhiteCat/PE-VC/Controller/CreationController.cs
    - Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VC*Data.cs (10 files)
    - Assets/Scripts/Assist/Singleton.cs
    - Assets/Scripts/BehaviorTree/Scripts/Singlton.cs
    - Assets/Scripts/GameNetwork/ProxyLabel.cs
    - Assets/Scripts/TG42_Script/VoxelEditor_E.cs
    - Assets/Scripts/ZhouXun/Voxel Creation/Scripts/WhiteCat/Common/Behaviour/SingletonBehaviour.cs
    - Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/VFVoxelChunkGo.cs

key-decisions:
  - "Removed ParticleRenderer branches from type checks instead of replacing with ParticleSystemRenderer -- ParticleSystemRenderer was already handled in the next branch of each if-chain"
  - "Left Resources.FindObjectsOfTypeAll unchanged in DetectMemLeak.cs -- it is a different API that is not deprecated in Unity 6"
  - "Replaced ScaleLegacySystems body with TODO stub rather than attempting full ParticleSystem module port -- legacy editor-only code, modern ParticleSystem scaling handled by ScaleShurikenSystems"

patterns-established:
  - "FindObjectsSortMode.None used for all FindObjectsByType replacements (fastest, matching original undefined ordering)"

requirements-completed: [ENV-01]

# Metrics
duration: 3min
completed: 2026-03-28
---

# Phase 1 Plan 3: Replace Legacy Particles and FindObjectsOfType Summary

**Removed all legacy particle class references (ParticleEmitter/ParticleAnimator/ParticleRenderer) and replaced FindObjectsOfType with FindObjectsByType across 18 files**

## Performance

- **Duration:** 3 min
- **Started:** 2026-03-28T20:18:43Z
- **Completed:** 2026-03-28T20:22:07Z
- **Tasks:** 2
- **Files modified:** 18

## Accomplishments
- Eliminated all ParticleEmitter, ParticleAnimator, and ParticleRenderer references from active code (12 files)
- Replaced all FindObjectsOfType/FindObjectOfType calls with Unity 6 equivalents using FindObjectsSortMode.None (6 files)
- Complex legacy particle scaling in ParticleScaler.cs stubbed with TODO(phase-4) comment preserving original behavior documentation

## Task Commits

Each task was committed atomically:

1. **Task 1: Replace legacy particle system classes** - `9aa113f4` (feat)
2. **Task 2: Replace FindObjectsOfType with FindObjectsByType** - `83563bed` (feat)

## Files Created/Modified

### Task 1: Legacy Particle System Removal
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Misc/ParticleScaler.cs` - Replaced ScaleLegacySystems body with TODO stub (legacy ParticleEmitter/ParticleAnimator APIs)
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/WhiteCat/PE-VC/Controller/CreationController.cs` - Removed ParticleRenderer from renderer type check chain
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCFixedPartData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCGeneralPartData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCFreePartData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCAsymmetricFixedPartData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCAsymmetricGeneralPartData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCTriphaseFixedPartData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCQuadphaseFixedPartData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCObjectLightData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCObjectPivotData.cs` - Removed ParticleRenderer branch
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Components/DataStructs/VCFixedHandPartData.cs` - Removed ParticleRenderer branch

### Task 2: FindObjectsOfType Replacement
- `Assets/Scripts/Assist/Singleton.cs` - FindObjectOfType -> FindFirstObjectByType, FindObjectsOfType -> FindObjectsByType
- `Assets/Scripts/BehaviorTree/Scripts/Singlton.cs` - Same singleton pattern replacement
- `Assets/Scripts/GameNetwork/ProxyLabel.cs` - FindObjectsOfType -> FindObjectsByType
- `Assets/Scripts/TG42_Script/VoxelEditor_E.cs` - GameObject.FindObjectsOfType -> GameObject.FindObjectsByType
- `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/WhiteCat/Common/Behaviour/SingletonBehaviour.cs` - FindObjectsOfType<T> -> FindObjectsByType<T>
- `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/VFVoxelChunkGo.cs` - Object.FindObjectsOfType -> Object.FindObjectsByType

## Decisions Made
- Removed ParticleRenderer branches entirely rather than replacing -- ParticleSystemRenderer was already the next check in every if-chain, so the ParticleRenderer branch was redundant
- Left `Resources.FindObjectsOfTypeAll` in DetectMemLeak.cs unchanged -- this is a separate API not deprecated in Unity 6
- Stubbed ParticleScaler.ScaleLegacySystems with TODO(phase-4) comment -- the legacy emitter/animator properties have no 1:1 mapping and the modern Shuriken scaling path already exists

## Deviations from Plan

None - plan executed exactly as written.

## Known Stubs

| File | Line | Stub | Reason |
|------|------|------|--------|
| `Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Misc/ParticleScaler.cs` | ~92 | `TODO(phase-4)` empty ScaleLegacySystems body | Legacy ParticleEmitter/ParticleAnimator APIs have no direct ParticleSystem equivalent; modern scaling handled by ScaleShurikenSystems |

## Issues Encountered
None

## User Setup Required
None - no external service configuration required.

## Next Phase Readiness
- All deprecated APIs that can be fixed via text replacement are now handled (plans 01, 02, 03 complete)
- Ready for Plan 04: Open project in Unity 6 editor and verify migration
- Component accessor deprecations (.rigidbody, .collider, etc.) will be auto-fixed by Unity API Updater when project opens in Unity 6

---
*Phase: 01-unity-6-migration*
*Completed: 2026-03-28*
