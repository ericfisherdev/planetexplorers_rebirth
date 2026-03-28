---
phase: 01-unity-6-migration
plan: 01
subsystem: infra
tags: [unity, migration, unityscript, csharp, project-settings]

# Dependency graph
requires: []
provides:
  - "Zero UnityScript (.js) files under Assets/ -- Unity 6 can import the project"
  - "9 C# MonoBehaviour replacements for particle/utility scripts"
  - "Clean ProjectSettings/ directory ready for Unity 6 regeneration"
  - "Scene list backup with all 10 build scenes in order"
affects: [01-02-PLAN, 01-03-PLAN, 01-04-PLAN]

# Tech tracking
tech-stack:
  added: []
  patterns:
    - "Mechanical UnityScript-to-C# conversion preserving field names for prefab compatibility"

key-files:
  created:
    - Assets/Resources/Particle/Script/Wind.cs
    - Assets/Resources/Particle/Script/ExplosionObject.cs
    - Assets/Resources/Particle/Script/ParticleSetting.cs
    - Assets/Resources/Particle/Script/Slash.cs
    - Assets/Resources/Prefab/Particle/150316/Scripts/MGE_destroyThisTimed.cs
    - Assets/Resources/Prefab/Particle/150316/Scripts/MGE_SetInitialForces.cs
    - Assets/Resources/Prefab/Particle/140520/Scripts/SM_trailFade.cs
    - Assets/Resources/Prefab/Particle/140520/Scripts/SM_rotateThis.cs
    - Assets/Resources/Prefab/Particle/yanwei/shader/randomRotate.cs
    - .planning/phases/01-unity-6-migration/scene-list-backup.txt
  modified: []

key-decisions:
  - "Preserved original field name typos (Objcet, postitionoffset) to maintain prefab serialization compatibility"
  - "Deleted SqliteAccess.js without replacement since SqliteAccessCS.cs already exists"

patterns-established:
  - "Field name preservation: when converting scripts, keep original field names including typos for Unity serialization compatibility"

requirements-completed: [ENV-01]

# Metrics
duration: 2min
completed: 2026-03-28
---

# Phase 1 Plan 1: Remove UnityScript Files and Clean ProjectSettings Summary

**Converted 9 UnityScript particle/utility scripts to C#, deleted all 10 .js files, and removed 16 binary ProjectSettings .asset files for Unity 6 compatibility**

## Performance

- **Duration:** 2 min
- **Started:** 2026-03-28T20:12:21Z
- **Completed:** 2026-03-28T20:14:32Z
- **Tasks:** 2
- **Files modified:** 46 (29 in Task 1, 17 in Task 2)

## Accomplishments
- Eliminated all UnityScript (.js) files from Assets/, unblocking Unity 6 project import
- Created 9 faithful C# MonoBehaviour replacements with matching class and field names
- Backed up the 10-scene build list before deleting EditorBuildSettings.asset
- Removed all 16 binary .asset files from ProjectSettings/ so Unity 6 can regenerate them

## Task Commits

Each task was committed atomically:

1. **Task 1: Convert UnityScript files to C# and delete originals** - `3e32b16f` (feat)
2. **Task 2: Clean ProjectSettings for Unity 6 regeneration** - `5cf85884` (feat)

## Files Created/Modified
- `Assets/Resources/Particle/Script/Wind.cs` - Wind force particle effect (C# replacement)
- `Assets/Resources/Particle/Script/ExplosionObject.cs` - Explosion spawner with debris (C# replacement)
- `Assets/Resources/Particle/Script/ParticleSetting.cs` - Particle lifetime and light fade (C# replacement)
- `Assets/Resources/Particle/Script/Slash.cs` - Scale animation for slash effects (C# replacement)
- `Assets/Resources/Prefab/Particle/150316/Scripts/MGE_destroyThisTimed.cs` - Timed self-destruct (C# replacement)
- `Assets/Resources/Prefab/Particle/150316/Scripts/MGE_SetInitialForces.cs` - Random initial force/torque (C# replacement)
- `Assets/Resources/Prefab/Particle/140520/Scripts/SM_trailFade.cs` - Trail renderer fade in/out (C# replacement)
- `Assets/Resources/Prefab/Particle/140520/Scripts/SM_rotateThis.cs` - Continuous rotation (C# replacement)
- `Assets/Resources/Prefab/Particle/yanwei/shader/randomRotate.cs` - Random rotation over time (C# replacement)
- `.planning/phases/01-unity-6-migration/scene-list-backup.txt` - Preserved scene build order

## Decisions Made
- Preserved original field name typos (Objcet, postitionoffset) because Unity prefabs reference fields by serialized name; changing them would break prefab references
- Deleted SqliteAccess.js without creating a C# replacement because SqliteAccessCS.cs already provides identical functionality
- Deleted all .js.meta files alongside .js files to avoid orphaned meta references

## Deviations from Plan

None - plan executed exactly as written.

## Issues Encountered
None.

## User Setup Required
None - no external service configuration required.

## Next Phase Readiness
- Project is now free of UnityScript files and can be opened in Unity 6
- ProjectSettings/ contains only ProjectVersion.txt; Unity 6 will regenerate all settings with defaults on first open
- Scene list is backed up for manual re-addition in Unity 6 Build Settings
- Ready for Plan 01-02 (next wave of Unity 6 migration work)

---
*Phase: 01-unity-6-migration*
*Completed: 2026-03-28*
