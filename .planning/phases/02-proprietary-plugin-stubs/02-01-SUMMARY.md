---
phase: 02-proprietary-plugin-stubs
plan: 01
subsystem: ui
tags: [ngui, stubs, unity, monobehaviour, ui-framework]

# Dependency graph
requires:
  - phase: 01-unity6-api-migration
    provides: Unity 6 API-compatible codebase ready for stub compilation
provides:
  - NGUI stub types for all 417+ files referencing UILabel, UISprite, UIPanel, UIButton, etc.
  - Correct inheritance chain UIRect -> UIWidget -> UIBasicSprite -> UISprite
  - EventDelegate system for NGUI event callback wiring
  - NGUITools static utilities (AddChild, SetActive, FindInParents, etc.)
  - Supporting types (UISpriteData, MouseOrTouch, BMFont, BetterList, NGUIText)
affects: [02-proprietary-plugin-stubs, 08-ui-implementation]

# Tech tracking
tech-stack:
  added: []
  patterns: [stub-with-default-returns, global-namespace-stubs, inheritance-chain-preservation]

key-files:
  created:
    - Assets/Stubs/NGUI/UIRect.cs
    - Assets/Stubs/NGUI/UIWidget.cs
    - Assets/Stubs/NGUI/UIBasicSprite.cs
    - Assets/Stubs/NGUI/UISprite.cs
    - Assets/Stubs/NGUI/UILabel.cs
    - Assets/Stubs/NGUI/UIPanel.cs
    - Assets/Stubs/NGUI/UICamera.cs
    - Assets/Stubs/NGUI/NGUITools.cs
    - Assets/Stubs/NGUI/EventDelegate.cs
    - Assets/Stubs/NGUI/UIEventListener.cs
    - Assets/Stubs/NGUI/UITweener.cs
    - Assets/Stubs/NGUI/TweenPosition.cs
    - Assets/Stubs/NGUI/UIButton.cs
    - Assets/Stubs/NGUI/UIGrid.cs
    - Assets/Stubs/NGUI/UIScrollView.cs
    - Assets/Stubs/NGUI/UIInput.cs
  modified: []

key-decisions:
  - "UIProgressBar inherits UIWidget (not MonoBehaviour directly) to match real NGUI hierarchy where foreground/background are UIWidget references"
  - "Included UISpriteData, MouseOrTouch, BMFont, BetterList, NGUIText as supporting types discovered during grep analysis"
  - "UIFont.CalculatePrintedSize returns Vector2.zero -- game code multiplies result by font.size so zero is safe default"

patterns-established:
  - "Stub pattern: global namespace, MonoBehaviour inheritance, default return values, all referenced enums included"
  - "Inheritance chain preservation: UIRect -> UIWidget -> UIBasicSprite -> UISprite matches real NGUI for cast compatibility"
  - "EventDelegate list pattern: fields initialized to non-null empty List<EventDelegate> so .Add()/.Remove() calls work without null checks"

requirements-completed: [ENV-02]

# Metrics
duration: 7min
completed: 2026-03-29
---

# Phase 2 Plan 1: NGUI Stub Library Summary

**39 NGUI stub types covering all 417+ file references with correct inheritance chains, enums, and global namespace placement**

## Performance

- **Duration:** 7 min
- **Started:** 2026-03-29T01:10:46Z
- **Completed:** 2026-03-29T01:18:14Z
- **Tasks:** 2
- **Files created:** 39

## Accomplishments
- Created 39 NGUI stub .cs files in Assets/Stubs/NGUI/ covering every NGUI type referenced across the codebase
- Preserved correct NGUI inheritance chain: UIRect -> UIWidget -> UIBasicSprite -> UISprite (and UILabel : UIWidget, UIPanel : UIRect, etc.)
- All stubs in global namespace (zero namespace declarations) matching how game code references NGUI types
- Included all enums referenced by game code: UIWidget.Pivot, UIDrawCall.Clipping, UILabel.Overflow/Effect, UIFont.SymbolStyle, UIBasicSprite.Type/FillDirection, UITweener.Method/Style, etc.
- Included supporting types discovered during grep analysis: UISpriteData, MouseOrTouch, BMFont, BetterList, NGUIText

## Task Commits

Each task was committed atomically:

1. **Task 1: Create NGUI core widget and container stubs** - `4c352da1` (feat)
2. **Task 2: Create NGUI interaction, layout, and tween stubs** - `9ad54fc7` (feat)

## Files Created/Modified
- `Assets/Stubs/NGUI/UIRect.cs` - Base class for all NGUI UI elements
- `Assets/Stubs/NGUI/UIWidget.cs` - Base visible widget with Pivot enum, color, depth, alpha
- `Assets/Stubs/NGUI/UIBasicSprite.cs` - Base sprite with Type/FillDirection enums, fillAmount
- `Assets/Stubs/NGUI/UISprite.cs` - Sprite with spriteName, atlas (+ UISpriteData helper)
- `Assets/Stubs/NGUI/UISlicedSprite.cs` - Backward-compatible UISprite alias
- `Assets/Stubs/NGUI/UILabel.cs` - Text widget with font, overflow, effect (+ NGUIText helper)
- `Assets/Stubs/NGUI/UITexture.cs` - Texture display widget
- `Assets/Stubs/NGUI/UIPanel.cs` - Container with clipping, alpha, depth
- `Assets/Stubs/NGUI/UICamera.cs` - Input event handling (+ MouseOrTouch helper)
- `Assets/Stubs/NGUI/UIAnchor.cs` - Legacy positioning anchor
- `Assets/Stubs/NGUI/UIRoot.cs` - UI scaling root
- `Assets/Stubs/NGUI/UIDrawCall.cs` - Draw call batching with Clipping enum
- `Assets/Stubs/NGUI/UIFont.cs` - Font asset with CalculatePrintedSize (+ BMFont, BetterList helpers)
- `Assets/Stubs/NGUI/UIAtlas.cs` - Sprite atlas asset
- `Assets/Stubs/NGUI/NGUITools.cs` - Static utilities (AddChild, SetActive, FindInParents, PlaySound, etc.)
- `Assets/Stubs/NGUI/EventDelegate.cs` - Event delegate system with Add/Remove/Execute
- `Assets/Stubs/NGUI/UIButton.cs` - Clickable button with state transitions
- `Assets/Stubs/NGUI/UICheckbox.cs` - Legacy checkbox (replaced by UIToggle)
- `Assets/Stubs/NGUI/UIToggle.cs` - Toggle/radio button with group support
- `Assets/Stubs/NGUI/UISlider.cs` - Slider extending UIProgressBar
- `Assets/Stubs/NGUI/UIProgressBar.cs` - Base progress bar with foreground/background/thumb
- `Assets/Stubs/NGUI/UIScrollBar.cs` - Scroll bar extending UISlider
- `Assets/Stubs/NGUI/UIInput.cs` - Text input with InputType/Validation/KeyboardType enums
- `Assets/Stubs/NGUI/UIPopupList.cs` - Dropdown/popup list
- `Assets/Stubs/NGUI/UIEventListener.cs` - Event listener with Get(GameObject) factory
- `Assets/Stubs/NGUI/UIGrid.cs` - Grid layout with Arrangement/Sorting enums
- `Assets/Stubs/NGUI/UITable.cs` - Table layout with variable height support
- `Assets/Stubs/NGUI/UIScrollView.cs` - Scrollable container with momentum
- `Assets/Stubs/NGUI/UIDragScrollView.cs` - Drag-to-scroll helper
- `Assets/Stubs/NGUI/UIWrapContent.cs` - Virtual scrolling content recycler
- `Assets/Stubs/NGUI/UISpriteAnimation.cs` - Sprite frame animation
- `Assets/Stubs/NGUI/UIPlayTween.cs` - Tween playback trigger
- `Assets/Stubs/NGUI/SpringPanel.cs` - Spring-based panel movement
- `Assets/Stubs/NGUI/UITweener.cs` - Base tween with Style/Method enums
- `Assets/Stubs/NGUI/TweenPosition.cs` - Position tween with Begin() factory
- `Assets/Stubs/NGUI/TweenRotation.cs` - Rotation tween with Begin() factory
- `Assets/Stubs/NGUI/TweenScale.cs` - Scale tween with Begin()/Play() factories
- `Assets/Stubs/NGUI/TweenAlpha.cs` - Alpha tween with Begin() factory
- `Assets/Stubs/NGUI/TweenColor.cs` - Color tween with Begin() factory

## Decisions Made
- UIProgressBar inherits UIWidget (not MonoBehaviour directly) to match real NGUI hierarchy where foreground/background are UIWidget references
- Included UISpriteData, MouseOrTouch, BMFont, BetterList, NGUIText as supporting types discovered during grep analysis of actual game code usage
- UIFont.CalculatePrintedSize returns Vector2.zero -- game code multiplies result by font.size so zero is a safe no-op default

## Deviations from Plan

None - plan executed exactly as written.

## Issues Encountered

None.

## User Setup Required

None - no external service configuration required.

## Known Stubs

All 39 files are intentionally stub implementations returning default values. This is by design -- stubs exist to satisfy compilation. Functional replacements are planned for Phase 8 (UI Implementation).

## Next Phase Readiness
- All NGUI types stubbed -- 417+ files referencing NGUI types can resolve these types at compile time
- Ready for Phase 2 Plans 02-04 to stub remaining proprietary plugins (uLink, uLobby, FMOD, FinalIK, etc.)
- Phase 8 will replace these stubs with functional uGUI equivalents

## Self-Check: PASSED

- All 39 stub files exist in Assets/Stubs/NGUI/
- Commit 4c352da1 (Task 1) verified
- Commit 9ad54fc7 (Task 2) verified
- Zero namespace declarations across all files
- Inheritance chain correct: UIRect -> UIWidget -> UIBasicSprite -> UISprite

---
*Phase: 02-proprietary-plugin-stubs*
*Completed: 2026-03-29*
