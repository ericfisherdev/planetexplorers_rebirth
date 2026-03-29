---
phase: 03-clean-compilation
plan: 01
subsystem: compilation
tags: [unity6, preprocessor, image-effects, stubs, conditional-compilation]

# Dependency graph
requires:
  - phase: 01-unity-6-migration
    provides: Initial API migration and deprecated symbol fixes
  - phase: 02-proprietary-plugin-stubs
    provides: Stub pattern for missing third-party types
provides:
  - All UNITY_5 preprocessor conditionals resolved for Unity 6
  - Stub MonoBehaviour types for 16 legacy UnityStandardAssets.ImageEffects classes
  - Editor image effect scripts compilable against stub types
affects: [03-clean-compilation, 08-ui-implementation]

# Tech tracking
tech-stack:
  added: []
  patterns: [image-effects-stubs-in-UnityStandardAssets.ImageEffects-namespace]

key-files:
  created:
    - Assets/Scripts/Stubs/ImageEffectsStubs.cs
  modified:
    - Assets/Scripts/Lilj/ViewCameraControler.cs
    - Assets/Scripts/ZhouXun/Misc/PlayerDeathEffect.cs
    - Assets/Scripts/ZhouXun/Misc/PlayerShakeEffect.cs
    - Assets/Scripts/ZhouXun/Camera Controller/Scripts/Mediator/PECameraMan.cs
    - Assets/Scripts/ZhouXun/Camera Controller/Scripts/Core/CamController.cs
    - Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/NGUIScripts/UIOption.cs
    - Assets/Scripts/Effect/CaveColorCorrection.cs
    - Assets/Scripts/AssetsLoader/AssetsLoader.cs
    - Assets/Scripts/WuYiqiu/Colony System/GUI/CSUI_MainWndCtrl.cs
    - Assets/Scripts/ZhouXun/Random SubTerrain/RSubTerrainMgr.cs
    - Assets/Scripts/ZhouXun/Layered SubTerrain/LSubTerrainMgr.cs
    - Assets/Editor/Steamworks.NET/RedistInstall.cs

key-decisions:
  - "Kept UNITY_5 branch code (Unity 5+ path) as it contains the modern API calls compatible with Unity 6"
  - "CaveColorCorrection inverted conditional: #if !UNITY_5 was the old Unity 4 code using ImageEffectBase; kept the #else branch (MonoBehaviour with Shader field)"
  - "Removed !UNITY_5 blocks in SubTerrainMgr files since they contained Unity 4 dead code that would erroneously compile on Unity 6"
  - "Added methods/enums to stubs (Dx11Support, CurrentAAMaterial, AAMode, TonemapperType) required by Editor scripts"

patterns-established:
  - "Image effect stubs: MonoBehaviour subclasses with public fields matching the original API surface, in UnityStandardAssets.ImageEffects namespace"

requirements-completed: [ENV-03]

# Metrics
duration: 5min
completed: 2026-03-29
---

# Phase 3 Plan 1: Fix UNITY_5 Conditionals and Image Effect References Summary

**Resolved all #if UNITY_5 preprocessor blocks across 12 files and created 16 MonoBehaviour stubs for legacy UnityStandardAssets.ImageEffects types**

## Performance

- **Duration:** 5 min
- **Started:** 2026-03-29T01:23:21Z
- **Completed:** 2026-03-29T01:28:44Z
- **Tasks:** 2
- **Files modified:** 13

## Accomplishments
- Eliminated all `#if UNITY_5` / `#if !UNITY_5` / `#if UNITY_5_3 || UNITY_5_4` conditional blocks, keeping the Unity 5+ code paths compatible with Unity 6
- Created comprehensive ImageEffectsStubs.cs with 16 stub classes, 2 enums, and all properties/methods accessed by gameplay and editor scripts
- All 13 Editor/ImageEffects custom inspector scripts now compile against stub types

## Task Commits

Each task was committed atomically:

1. **Task 1: Resolve UNITY_5 conditional compilation blocks and image effect using directives** - `4d4e2f4f` (feat)
2. **Task 2: Clean up Editor ImageEffects scripts** - `ae1071ea` (feat)

## Files Created/Modified
- `Assets/Scripts/Stubs/ImageEffectsStubs.cs` - Stub MonoBehaviour classes for 16 legacy image effect types with fields and methods required by consuming scripts
- `Assets/Scripts/Lilj/ViewCameraControler.cs` - Removed UNITY_5 guards around BlurOptimized using alias and blurIterations/blurSize access
- `Assets/Scripts/ZhouXun/Misc/PlayerDeathEffect.cs` - Removed UNITY_5 guards around Grayscale.rampOffset and BloomAndFlares.bloomThreshold
- `Assets/Scripts/ZhouXun/Misc/PlayerShakeEffect.cs` - Removed UNITY_5 guard around MotionBlur using alias
- `Assets/Scripts/ZhouXun/Camera Controller/Scripts/Mediator/PECameraMan.cs` - Removed UNITY_5 guard around SSAOPro, Antialiasing, DepthOfField using aliases
- `Assets/Scripts/ZhouXun/Camera Controller/Scripts/Core/CamController.cs` - Removed two identical UNITY_5 blocks around Cursor.visible (both branches were identical)
- `Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/NGUIScripts/UIOption.cs` - Removed UNITY_5 guard around Antialiasing using alias
- `Assets/Scripts/Effect/CaveColorCorrection.cs` - Collapsed inverted conditional; kept Unity 5+ branch (MonoBehaviour with Shader field)
- `Assets/Scripts/AssetsLoader/AssetsLoader.cs` - Removed UNITY_5_3/5_4 guard; kept AssetBundle.LoadFromMemory (correct API for Unity 6)
- `Assets/Scripts/WuYiqiu/Colony System/GUI/CSUI_MainWndCtrl.cs` - Removed UNITY_5 guard around isActiveAndEnabled variable declaration
- `Assets/Scripts/ZhouXun/Random SubTerrain/RSubTerrainMgr.cs` - Removed !UNITY_5 dead code block (referenced commented-out variable)
- `Assets/Scripts/ZhouXun/Layered SubTerrain/LSubTerrainMgr.cs` - Removed !UNITY_5 dead code block (Unity 4 collider trigger setup)
- `Assets/Editor/Steamworks.NET/RedistInstall.cs` - Removed obsolete DLL-copy block (only needed on Unity <= 5.0)

## Decisions Made
- Kept UNITY_5 branch code in all files since it contains the modern API calls (rampOffset vs grayAmount, blurIterations vs iterations, LoadFromMemory vs CreateFromMemoryImmediate)
- CaveColorCorrection had inverted logic: `#if !UNITY_5` was the old Unity 4 code using `ImageEffectBase` (which no longer exists); kept the `#else` branch
- Removed `!UNITY_5` blocks from SubTerrainMgr files because they contained Unity 4 code that would erroneously compile on Unity 6 (since UNITY_5 is not defined)
- Added methods, enums, and fields to stubs that editor scripts access via direct casts (Dx11Support, CurrentAAMaterial, AAMode, TonemapperType, basedOnTempTex, ValidDimensions, Convert, validRenderTextureFormat)

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 1 - Bug] Fixed !UNITY_5 blocks in SubTerrainMgr files**
- **Found during:** Task 1
- **Issue:** `#if !UNITY_5` blocks contain Unity 4 code that would erroneously compile on Unity 6 (since UNITY_5 symbol is not defined, !UNITY_5 evaluates to true). In RSubTerrainMgr, this block referenced a commented-out variable, which would cause a compilation error.
- **Fix:** Removed the dead !UNITY_5 blocks entirely
- **Files modified:** Assets/Scripts/ZhouXun/Random SubTerrain/RSubTerrainMgr.cs, Assets/Scripts/ZhouXun/Layered SubTerrain/LSubTerrainMgr.cs
- **Committed in:** 4d4e2f4f

**2. [Rule 1 - Bug] Fixed UNITY_5 conditional in Steamworks RedistInstall.cs**
- **Found during:** Task 1
- **Issue:** `#if UNITY_EDITOR_WIN && (!UNITY_5 || UNITY_5_0)` would evaluate to true on Unity 6 since UNITY_5 is not defined, running an unnecessary DLL copy that was only needed for Unity <= 5.0
- **Fix:** Removed the conditional block with explanatory comment
- **Files modified:** Assets/Editor/Steamworks.NET/RedistInstall.cs
- **Committed in:** 4d4e2f4f

**3. [Rule 1 - Bug] Fixed CaveColorCorrection inverted conditional**
- **Found during:** Task 1
- **Issue:** The file used `#if !UNITY_5` which would include Unity 4 code (extending ImageEffectBase, which does not exist) on Unity 6
- **Fix:** Collapsed to keep only the Unity 5+ branch (MonoBehaviour with Shader field)
- **Files modified:** Assets/Scripts/Effect/CaveColorCorrection.cs
- **Committed in:** 4d4e2f4f

---

**Total deviations:** 3 auto-fixed (3 bugs)
**Impact on plan:** All auto-fixes necessary for correctness -- without them, dead Unity 4 code would compile on Unity 6 and cause errors. No scope creep.

## Issues Encountered
None

## User Setup Required
None - no external service configuration required.

## Known Stubs
- `Assets/Scripts/Stubs/ImageEffectsStubs.cs` - All 16 image effect classes are intentional stubs providing compile-time compatibility only. They contain no rendering logic. Future plans may replace these with real implementations or Unity 6 equivalents if needed.

## Next Phase Readiness
- All UNITY_5 conditionals eliminated -- no more preprocessor-guarded dead code
- Image effect type references resolved via stubs -- editor and gameplay scripts should compile
- Ready for remaining Phase 3 plans (03-02, 03-03) to address other compilation errors

---
*Phase: 03-clean-compilation*
*Completed: 2026-03-29*
