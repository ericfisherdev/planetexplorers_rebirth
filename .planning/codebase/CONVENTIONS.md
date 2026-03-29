# Coding Conventions

**Analysis Date:** 2026-03-28

## Language and Runtime

This is a Unity game project written in **C# (Unity 5.x era, .NET 3.5 subset)**. All gameplay logic is in `.cs` files under `Assets/`. 2888 C# files total.

## Naming Patterns

**Classes:**
- PascalCase throughout: `PlayerNetwork`, `AudioManager`, `MissionManager`
- Manager classes use `Mgr` suffix: `UITipRecordsMgr`, `RandomItemMgr`, `EntityCreateMgr`
- Manager classes also use `Manager` suffix interchangeably: `MissionManager`, `AudioManager`
- UI scripts prefixed by subsystem: `CSUI_Hospital`, `CSUI_Train`, `UIAdminstratorWnd`
- NPC component scripts prefixed by abbreviated type: `BTNpcBase` (Behavior Tree NPC)
- Custom scenario actions use descriptive `*Action` suffix: `RunMissionAction`, `PlaySpeechAction`
- New-style UI scripts appended with `_N`: `ItemGetItem_N`, `ItemOpBtn_N`

**Enums:**
- PascalCase for type name: `MissionType`, `ENpcJob`, `ELineType`
- Prefixes `E` used inconsistently — some enums have it (`ENpcJob`, `ECtrlType`), many do not (`MissionType`, `DungeonType`)
- Values are PascalCase: `MissionType_Main` (old style with underscores) or `Follower` (newer style)
- ALL_CAPS used occasionally: `INVITESTATE`
- Enum values sometimes include Chinese comments for developer reference

**Interfaces:**
- `I` prefix convention: `INetworkEvent`, `IPeMsg`, `ISkillTarget`, `IAttack`, `IWeapon`
- Defined in files: `Assets/Scripts/BehaviorTree/Scripts/BTInterface.cs`, `Assets/Scripts/Operate/Operation.cs`

**Private Fields:**
- `m_` prefix for instance members (most common in core gameplay code): `m_Entity`, `m_Trans`, `m_Enemies`
- `_` prefix for some instance members: `_transCmpt`, `_initOk`, `_bDirty`
- `s_` prefix for static members: `s_instance`, `s_tmpDbFileName`
- No prefix on some older files (inconsistent)
- Hungarian-style type prefixes appear in some UI code: `m_CheckSpr` (Sprite), `mbGrounded` (bool), `mfRotationY` (float)

**Public Fields and Properties:**
- PascalCase for public properties: `PlayerEntity`, `PlayerPos`, `Instance`
- camelCase for some public fields: `mainPlayerId`, `mainPlayer`
- Public fields directly on MonoBehaviours (for Inspector wiring): `public UISprite m_CheckSpr`

**Methods:**
- Public methods: PascalCase — `GetComponent<T>()`, `CopyTo()`, `SetCheckIcon()`
- Private methods: PascalCase — `UpdateVelocity()`, `RefreshNPCGrids()`, `GetAnchorDir()`
- Some private methods use camelCase (older/inconsistent code): `deleteRoleInfo()`, `setSelectObjEnergySheild()`
- Callback/event handler naming: `OnDestroy`, `OnBtnStartLearnSkill`, `HandleLog`
- Unity lifecycle methods: `Awake`, `Start`, `Update`, `FixedUpdate`, `OnDestroy`, `OnEnable`, `OnDisable`, `OnGUI`

**Files:**
- One class per file is common but not universal — related small classes coexist in one file
- File name matches primary class name: `AudioManager.cs` → `class AudioManager`
- No enforced file naming for interfaces

**Namespaces:**
- Used inconsistently — roughly 401 of 2328 Script files use a `namespace` declaration
- Core gameplay code in `Pathea` namespace: `namespace Pathea { ... }`
- Skill system in `SkillAsset` namespace
- Behavior tree in `Behave.Runtime`
- Custom scenario in `PeCustom`
- Most MonoBehaviour UI scripts and older code have NO namespace (global scope)

## Code Style

**Formatting:**
- No enforced formatter detected (no `.editorconfig`, `.prettierrc`, or equivalent)
- Mixed indentation styles: some files use tabs, others use 4-space indentation
- Brace placement: generally Allman (opening brace on new line) for class/method bodies, but K&R style (`{` same line) also appears
- Single-line bodies sometimes written inline: `public static T Instance { get { return _instance; } }`

**Linting:**
- No linting config detected
- Compiler directives (`#define`, `#if`) used for conditional compilation: `#define TMP_CODE`, `#if SAVE_GAME_LOG`, `#if UNITY_EDITOR`

## Import Organization

Imports are not ordered by any consistent rule. Common patterns observed:

1. `using UnityEngine;` — almost always first
2. `using System.Collections;` / `using System.Collections.Generic;` — second/third
3. System namespaces: `using System.IO;`, `using System.Xml;`, `using System.Linq;`
4. Game-specific namespaces: `using Pathea;`, `using ItemAsset;`, `using SkillSystem;`
5. Third-party: `using Steamworks;`, `using Mono.Data.SqliteClient;`

No import sorting enforcer is in place.

## Error Handling

**Primary approach: Silent failure / null guard**
```csharp
// Null guards with early return (most common)
if (clipId <= 0)
    return null;

SESoundBuff buff = SESoundBuff.GetSESoundData(clipId);
if (buff == null) {
    Debug.LogError("Can't find sound : " + clipId);
    return null;
}
```

**Exception handling (used sparingly, ~53 files):**
```csharp
// IO-related operations use try/catch with centralized helper
try
{
    mDirInfo = Directory.CreateDirectory(dir);
}
catch (System.Exception ex)
{
    GameLog.HandleIOException(ex);
}
```

**Centralized IO error handler:** `GameLog.HandleIOException(ex)` in `Assets/Scripts/ZhouXun/Misc/GameLog.cs` — routes exceptions to `Debug.LogError`/`Debug.LogWarning` with specific error categorization.

**Catch-all exceptions:** Some code catches bare `Exception` with no handling:
```csharp
catch (Exception)
// body empty or silently ignored
```

**No custom exception types** are defined or thrown by game code — errors surface through Unity's `Debug.LogError`.

## Logging

**Framework:** Unity `Debug.Log` family — used in 501 files.

**Levels:**
- `Debug.Log(...)` — informational, general trace
- `Debug.LogWarning(...)` — non-fatal issues, used in 380 files
- `Debug.LogError(...)` — errors, including caught exceptions, used in 380 files

**Colored log output pattern** (ZhouXun code style):
```csharp
Debug.Log("<color=aqua>temp path:" + tempPath + "</color>");
```

**Error categorization strings** passed to `Debug.LogError` for `GameLog` crash handler parsing:
- `"SaveDataCorrupt:"`
- `"FilesCorrupt:"`
- `"AutoSaveDataCorrupt:"`
- `"OclKernelError"`

## Comments

**Mixed languages:** Comments appear in both English and Chinese (Simplified). Chinese comments annotate game design intent; English comments are more common in technical sections.

**Style:**
- XML doc comments (`/// <summary>`) used in some UI scripts (`CSUI_Hospital.cs`, etc.)
- Block comments for logic sections: `/* ... */`
- Inline Chinese annotations on fields: `public int npcID; // NPC的ID`
- Section dividers in large files: `/******** ... *********/`
- Commented-out code is common throughout

**TODO/FIXME pattern:**
```csharp
// TODO : code
// TODO : Confirm how an enemy to attack building
// TODO RANDOM NEEDED
// FIXME (not used, but TODO markers are widespread)
```
Found in: `Assets/Scripts/Skills/SkillRunner.cs`, `Assets/Scripts/GameNetwork/Player/PlayerNetwork.cs`, `Assets/Scripts/AiScripts/AiSpawn/SPTerrainEvent.cs`, and many others.

## Structural Patterns

**Singleton (MonoBehaviour):** Generic base class at `Assets/Scripts/Assist/Singleton.cs`
```csharp
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour { ... }
public class PESingleton<T> { ... }  // Non-MonoBehaviour variant
```

**Manual singleton pattern** (used more often than base class, ~798 files):
```csharp
private static AudioManager _instance;
public static AudioManager instance { get { ... } }
```

**Region blocks** for organization (374 files):
```csharp
#region Static Variables
#region Variables
#region Properties
#region Unity Internal APIs
#region Internal APIs
```

**Partial classes** (104 files): Used heavily to split large classes across files. Example: `PlayerNetwork.cs` is `public partial class PlayerNetwork`.

**Events/Delegates** (201 files):
```csharp
public static event Action OnTeamChangedEventHandler;
public Action<PeEntity, PeEntity, float> HatredEvent;
```

**Coroutines** (167 files): Used for async operations, animations, and delayed logic via `StartCoroutine(IEnumerator)`.

**Serialization patterns:**
- Binary: `BinaryReader`/`BinaryWriter` (152 files) for save data
- XML: `XmlSerializer`/`XmlDocument` (42 files) for configuration files
- Custom Export/Import pattern on data classes:
```csharp
public byte[] Export() { return PETools.Serialize.Export((w) => { w.Write(npcID); ... }); }
public void Import(byte[] data) { PETools.Serialize.Import(data, (r) => { npcID = r.ReadInt32(); ... }); }
```

**Data-only structs** used for network sync and lightweight data transfer:
```csharp
public struct MonsterIDNum { public Vector3 pos; public int id; public int num; }
```

## Function Design

**Size:** Many methods are very long (hundreds of lines). `StroyManager.cs` is 6151 lines; `BTNpcBase.cs` is 4642 lines. Single-responsibility is frequently violated.

**Parameters:** No consistent pattern for number of parameters — some methods have 8+ parameters.

**Return values:** Methods returning `bool` for success/failure; `null` for missing references; `BehaveResult.Success/Failure` in behavior tree actions.

## Module Design

**Exports:** No barrel file pattern. Classes are imported directly by full name.

**No package/module system** — Unity project-level assembly; all scripts in a single default assembly unless using `.asmdef` files (none detected).

---

*Convention analysis: 2026-03-28*
