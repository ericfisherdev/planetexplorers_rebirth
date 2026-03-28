# Testing Patterns

**Analysis Date:** 2026-03-28

## Test Framework

**Runner:** None detected.

No NUnit, Unity Test Framework (`[Test]`, `[UnityTest]`, `[TestFixture]`), or any other test framework is present in the codebase. A search across all 2888 `.cs` files for `[Test]`, `[TestFixture]`, `NUnit`, and `UnityTest` returned no results.

**Assertion Library:** None.

**Run Commands:**
```bash
# No test commands available — no test runner configured
```

## What "Test" Means in This Codebase

The directory `Assets/Scripts/GameUITest/` is **not** a test suite. It is a UI development scratchpad/prototype area containing full MonoBehaviour gameplay scripts (e.g., `CSUI_Hospital.cs`, `UIAdminstratorWnd.cs`). These scripts are production UI components organized under a misleading directory name.

Similarly, `Assets/Camera Forge/Scripts/Test/Test.cs` is a manual runtime integration harness:
```csharp
// Assets/Camera Forge/Scripts/Test/Test.cs
public class Test : MonoBehaviour {
    public CameraForge.CameraController camCtrl;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            camCtrl.CrossFade("Test Blender", 0, 0.2f);
        }
    }
}
```
This is a keyboard-driven in-editor playtest script, not an automated test.

## Test File Organization

**Location:** Not applicable — no automated test files exist.

**Naming:** Not applicable.

## How the Codebase Is Validated

The project relies exclusively on **manual in-editor playtesting** and **runtime error reporting**:

**Runtime error capture** via `Assets/Scripts/ZhouXun/Misc/GameLog.cs`:
- Hooks `Application.logMessageReceived`
- Catches `LogType.Exception` and `LogType.Error` at runtime
- Displays an in-game crash dialog to players
- Routes errors to `Debug.LogError` for Unity console visibility
- Categorizes IO errors via `GameLog.HandleIOException(ex)` called from `catch` blocks

**Debug.Log guards:** 501 files contain `Debug.Log` calls. 380 files use `Debug.LogError`/`Debug.LogWarning`. This is the primary defect detection mechanism.

**Null guards as defensive validation:**
```csharp
// Pattern throughout codebase — validation by returning null/early exit
if (buff == null) {
    Debug.LogError("Can't find sound : " + clipId);
    return null;
}
```

**Conditional compilation blocks for debug builds:**
```csharp
#if UNITY_EDITOR
    // Editor-only assertions or behavior
#endif
#if SAVE_GAME_LOG
    // Detailed log capture in debug builds
#endif
```

## Mocking

**Framework:** None.

**Patterns:** Not applicable — no test infrastructure exists for mocking.

## Fixtures and Factories

**Test Data:** Not applicable.

**Location:** Not applicable.

## Coverage

**Requirements:** None enforced.

**View Coverage:**
```bash
# Not applicable — no test runner to generate coverage
```

## Test Types

**Unit Tests:** Not present.

**Integration Tests:** Not present.

**E2E Tests:** Not present.

**Manual Playtest Infrastructure:**
- `Assets/Scripts/GameUITest/` — UI component development area used for in-editor manual testing of UI features
- `Assets/Camera Forge/Scripts/Test/Test.cs` — keyboard-driven camera behavior test in Play Mode
- `Assets/Editor/` — Unity Editor extension scripts for tooling (e.g., `AnimatorControllerTool.cs`, `RagdollTool.cs`)

## Adding Tests (Recommended Approach)

If automated testing is introduced, the Unity Test Framework (included with Unity 2019+) can be enabled. The project would need to be upgraded to support it. Key files to target first given their complexity:

- `Assets/Scripts/Mission/StroyManager.cs` (6151 lines)
- `Assets/Scripts/Mission/PlayerMission.cs` (4729 lines)
- `Assets/Scripts/Mission/MissionScript/MissionManager.cs` (3674 lines)
- `Assets/Scripts/PeEntity/Cmpt/NpcCmpt.cs` (2914 lines)

Pure C# (non-MonoBehaviour) data classes such as those in `Assets/Scripts/PuJi/Town/Model/` and `Assets/Scripts/Mission/Mission.cs` are the best candidates for unit testing without requiring Unity scene setup.

---

*Testing analysis: 2026-03-28*
