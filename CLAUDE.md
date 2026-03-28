<!-- GSD:project-start source:PROJECT.md -->
## Project

**Planet Explorers Rebirth**

A faithful recreation of the original Planet Explorers game, built from the officially open-sourced Unity 5.2.4f1 C# source code (released by Pathea Games in 2019). The goal is to get the full core gameplay loop — exploration, building, crafting, and combat — running as standalone builds on Linux and Windows, starting with single-player.

**Core Value:** The game launches, loads a world, and lets you play the core gameplay loop (explore, build, craft, fight) as a standalone executable on Linux and Windows.

### Constraints

- **Engine version**: Upgrading from Unity 5.2.4f1 to modern Unity (6+) — significant API migration but enables Linux editor and modern tooling
- **Proprietary plugins**: uLink, uLobby, NGUI, and other paid plugins must be replaced with open-source alternatives
- **No original assets**: May need placeholder art/audio if original assets aren't available
- **Compilation first**: Must achieve clean compilation before any gameplay work
<!-- GSD:project-end -->

<!-- GSD:stack-start source:codebase/STACK.md -->
## Technology Stack

## Languages
- C# (.NET/Mono) - All gameplay, networking, UI, and engine scripting in `Assets/Scripts/` and `Assets/PeCustomGame/`
- HLSL/ShaderLab - GPU shaders in `Assets/Resources/ShadersForDebug/`, `Assets/Terrain/Voxelform2/Voxelform/Source/Shaders/`, `Assets/PeAudio/Resources/`
- XML - Data configuration files; `ConfigFiles/MapConfig.xml`, `ConfigFiles/Position.xml`
- JSON - Network/server config; `ConfigFiles/ClientConfig.conf`
## Runtime
- Unity Engine 5.2.4f1 (confirmed via `ProjectSettings/ProjectVersion.txt`)
- Mono runtime (standard Unity 5.x scripting backend)
- Not applicable - Unity 5.x uses manual asset placement (no Package Manager in this era)
- Third-party plugins intentionally excluded from repo (per `README.md`)
## Frameworks
- Unity 5.2.4f1 - Game engine, rendering, physics, animation
- uLink - RPC-based multiplayer networking; core network layer in `Assets/Scripts/GameNetwork/NetworkInterface.cs`, `NetworkManager.cs`
- uLobby - Lobby/matchmaking server layer; used in `Assets/Scripts/GameNetwork/LobbyInterface.cs`, `Assets/Scripts/LobbyShop/Shop.cs`
- NGUI (Next-Gen UI) - Unity UI framework; used extensively across 200+ files with `UILabel`, `UIPanel`, `UISprite`, `UIButton` components in `Assets/Scripts/Chenzhi/NewUI/`
- FMOD Studio - Professional audio middleware; `Assets/PeAudio/PeFmodEditor/FMODAudioSourceRTE.cs` uses `FMOD.Studio` namespace
- RootMotion Final IK - Full-body inverse kinematics; `Assets/Scripts/Ik/IKHumanMgr.cs`, `Assets/Scripts/Assist/IKCombat.cs`
- Unity Mecanim - Standard Unity animator system
- A* Pathfinding Project - Pathfinding and RVO steering; `Assets/Scripts/Assist/PEPathfinder.cs` (with `Pathfinding.RVO`)
- Behave - Behavior tree runtime; `Assets/Scripts/BehaviorTree/Scripts/BTLauncher.cs`, namespace `Behave.Runtime`
- UnitySteer - Steering behaviors for autonomous agents; `Assets/Scripts/AiScripts/UnitySteer/`
- Voxelform2 - Marching cubes/transvoxel terrain system; `Assets/Terrain/Voxelform2/`
- Block45 - Custom voxel building/editing system; `Assets/Block45/`
- Transvoxel algorithm - Smooth voxel surface extraction; `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/TransVoxel/`
- OpenCLNet (Win/Mac variants) - GPU-accelerated marching cubes; `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/MarchingCubes/Ocl/`
- Nova Environment - Weather, sky, and environment effects; `Assets/Nova Environment/`
- Camera Forge - Cinematic camera controller system; `Assets/Camera Forge/`
- ScenarioRTL - In-house scenario/mission scripting runtime; `Assets/PeCustomGame/Scenario/ScenarioRTL/`
- PatheaScript - Custom script language runtime for modding; `Assets/PatheaScript/Core/`
- Unity Standard Assets Image Effects - Motion blur, grayscale; `Assets/Editor/ImageEffects/`
- Custom VehiclePhysics - Internal vehicle simulation using Unity Rigidbody; `Assets/Scripts/VehiclePhysics/`
- Unity Editor 5.2.4f1 - IDE and build tooling
- AssetStore Tools - Asset management editor extensions; `Assets/AssetStoreTools/`
## Key Dependencies
- `uLink` - Core multiplayer transport; every networked object extends `uLink.MonoBehaviour`; found in `Assets/Scripts/GameNetwork/NetworkInterface.cs`
- `uLobby` - Server lobby and matchmaking; `Assets/Scripts/GameNetwork/LobbyInterface.cs`
- `Steamworks.NET` (v1.0.3) - Steam platform integration; `Assets/Scripts/Steamwork.NET/SteamManager.cs`
- `Mono.Data.SqliteClient` - SQLite database access via Mono; `Assets/PeMap/MapLabel.cs`, `Assets/PeWorld/NameGenerator.cs`
- `RootMotion.FinalIK` - Full-body IK; used in 5+ files in `Assets/Scripts/Assist/` and `Assets/Scripts/MountsMonster/`
- `FMOD` + `FMOD.Studio` - All non-trivial audio playback; `Assets/PeAudio/`
- `Pathfinding` + `Pathfinding.RVO` - All NPC pathfinding; `Assets/Scripts/Assist/PEPathfinder.cs`
- `Behave.Runtime` - NPC and entity behavior trees; `Assets/Scripts/BehaviorTree/`
- `ScenarioRTL` - Custom game scenario execution; `Assets/PeCustomGame/Scenario/`
## Configuration
- `ConfigFiles/ClientConfig.conf` - JSON file with LobbyIP, LobbyPort, ProxyIP, ProxyPort
- `ConfigFiles/MapConfig.xml` - Map configuration
- `ConfigFiles/Position.xml` - World position configuration
- `ConfigFiles/version.txt` - Current version: `Ver 1.1.0`
- `ProjectSettings/ProjectSettings.asset` - Unity project settings (binary)
- `ProjectSettings/EditorBuildSettings.asset` - Scene build list
- `ProjectSettings/GraphicsSettings.asset` - Rendering settings
- `ProjectSettings/QualitySettings.asset` - Quality tiers
## Platform Requirements
- Unity 5.2.4f1 (exact version required; `ProjectSettings/ProjectVersion.txt`)
- Third-party plugins must be sourced separately (uLink, uLobby, FMOD, Steamworks.NET, FinalIK, NGUI, A* Pathfinding Project, Behave — all excluded from repo per `README.md`)
- Windows PC (primary platform; `ClientConfig.conf` references Windows lobby server IPs)
- Steam distribution via Steamworks
- Dedicated server model via uLobby (lobby at `119.28.73.40:12534`, proxy at `119.28.73.40:12535`)
<!-- GSD:stack-end -->

<!-- GSD:conventions-start source:CONVENTIONS.md -->
## Conventions

## Language and Runtime
## Naming Patterns
- PascalCase throughout: `PlayerNetwork`, `AudioManager`, `MissionManager`
- Manager classes use `Mgr` suffix: `UITipRecordsMgr`, `RandomItemMgr`, `EntityCreateMgr`
- Manager classes also use `Manager` suffix interchangeably: `MissionManager`, `AudioManager`
- UI scripts prefixed by subsystem: `CSUI_Hospital`, `CSUI_Train`, `UIAdminstratorWnd`
- NPC component scripts prefixed by abbreviated type: `BTNpcBase` (Behavior Tree NPC)
- Custom scenario actions use descriptive `*Action` suffix: `RunMissionAction`, `PlaySpeechAction`
- New-style UI scripts appended with `_N`: `ItemGetItem_N`, `ItemOpBtn_N`
- PascalCase for type name: `MissionType`, `ENpcJob`, `ELineType`
- Prefixes `E` used inconsistently — some enums have it (`ENpcJob`, `ECtrlType`), many do not (`MissionType`, `DungeonType`)
- Values are PascalCase: `MissionType_Main` (old style with underscores) or `Follower` (newer style)
- ALL_CAPS used occasionally: `INVITESTATE`
- Enum values sometimes include Chinese comments for developer reference
- `I` prefix convention: `INetworkEvent`, `IPeMsg`, `ISkillTarget`, `IAttack`, `IWeapon`
- Defined in files: `Assets/Scripts/BehaviorTree/Scripts/BTInterface.cs`, `Assets/Scripts/Operate/Operation.cs`
- `m_` prefix for instance members (most common in core gameplay code): `m_Entity`, `m_Trans`, `m_Enemies`
- `_` prefix for some instance members: `_transCmpt`, `_initOk`, `_bDirty`
- `s_` prefix for static members: `s_instance`, `s_tmpDbFileName`
- No prefix on some older files (inconsistent)
- Hungarian-style type prefixes appear in some UI code: `m_CheckSpr` (Sprite), `mbGrounded` (bool), `mfRotationY` (float)
- PascalCase for public properties: `PlayerEntity`, `PlayerPos`, `Instance`
- camelCase for some public fields: `mainPlayerId`, `mainPlayer`
- Public fields directly on MonoBehaviours (for Inspector wiring): `public UISprite m_CheckSpr`
- Public methods: PascalCase — `GetComponent<T>()`, `CopyTo()`, `SetCheckIcon()`
- Private methods: PascalCase — `UpdateVelocity()`, `RefreshNPCGrids()`, `GetAnchorDir()`
- Some private methods use camelCase (older/inconsistent code): `deleteRoleInfo()`, `setSelectObjEnergySheild()`
- Callback/event handler naming: `OnDestroy`, `OnBtnStartLearnSkill`, `HandleLog`
- Unity lifecycle methods: `Awake`, `Start`, `Update`, `FixedUpdate`, `OnDestroy`, `OnEnable`, `OnDisable`, `OnGUI`
- One class per file is common but not universal — related small classes coexist in one file
- File name matches primary class name: `AudioManager.cs` → `class AudioManager`
- No enforced file naming for interfaces
- Used inconsistently — roughly 401 of 2328 Script files use a `namespace` declaration
- Core gameplay code in `Pathea` namespace: `namespace Pathea { ... }`
- Skill system in `SkillAsset` namespace
- Behavior tree in `Behave.Runtime`
- Custom scenario in `PeCustom`
- Most MonoBehaviour UI scripts and older code have NO namespace (global scope)
## Code Style
- No enforced formatter detected (no `.editorconfig`, `.prettierrc`, or equivalent)
- Mixed indentation styles: some files use tabs, others use 4-space indentation
- Brace placement: generally Allman (opening brace on new line) for class/method bodies, but K&R style (`{` same line) also appears
- Single-line bodies sometimes written inline: `public static T Instance { get { return _instance; } }`
- No linting config detected
- Compiler directives (`#define`, `#if`) used for conditional compilation: `#define TMP_CODE`, `#if SAVE_GAME_LOG`, `#if UNITY_EDITOR`
## Import Organization
## Error Handling
## Logging
- `Debug.Log(...)` — informational, general trace
- `Debug.LogWarning(...)` — non-fatal issues, used in 380 files
- `Debug.LogError(...)` — errors, including caught exceptions, used in 380 files
- `"SaveDataCorrupt:"`
- `"FilesCorrupt:"`
- `"AutoSaveDataCorrupt:"`
- `"OclKernelError"`
## Comments
- XML doc comments (`/// <summary>`) used in some UI scripts (`CSUI_Hospital.cs`, etc.)
- Block comments for logic sections: `/* ... */`
- Inline Chinese annotations on fields: `public int npcID; // NPC的ID`
- Section dividers in large files: `/******** ... *********/`
- Commented-out code is common throughout
## Structural Patterns
#region Static Variables
#region Variables
#region Properties
#region Unity Internal APIs
#region Internal APIs
- Binary: `BinaryReader`/`BinaryWriter` (152 files) for save data
- XML: `XmlSerializer`/`XmlDocument` (42 files) for configuration files
- Custom Export/Import pattern on data classes:
## Function Design
## Module Design
<!-- GSD:conventions-end -->

<!-- GSD:architecture-start source:ARCHITECTURE.md -->
## Architecture

## Pattern Overview
- Entities (`PeEntity`) are Unity GameObjects with composable `PeCmpt` components carrying game logic and serialization
- Global singletons (both `Singleton<T>` MonoBehaviour and `PESingleton<T>` plain-class variants) coordinate cross-cutting systems
- Event-driven decoupling via typed `PeEvent.Event<T>` and `PeEventGlobal` for lifecycle signals (death, pickup, revive, destroy, HP change)
- Client/server multiplayer split via uLink network layer with Owner/Proxy prefab pairs
- Custom binary serialization (`Serialize`/`Deserialize`) on every `IPeCmpt` for save/load
## Layers
- Purpose: Application startup, data loading, cross-scene persistence
- Location: `Assets/Scripts/Global/`
- Contains: `GlobalBehaviour.cs` (MonoBehaviour on persistent GameObject), `GameTime.cs`, `SceneMediator.cs`
- Depends on: `LocalDatabase`, `PeLogicGlobal`, `PeCamera`, all singletons
- Used by: Unity Engine (called via `Awake`/`Update`/`LateUpdate`)
- Purpose: Game mode, scene flow, archive/save orchestration
- Location: `Assets/PeLauncher/`
- Contains: `PeGameMgr.cs` (static class, player type/scene mode enums), `PeFlowMgr.cs`, `PeGameLoader.cs`, `ArchiveMgr.cs`, `SingleGame.cs`, `MultiGame.cs`
- Depends on: `PeEntity` layer, `ArchiveMgr`
- Used by: Scene loading, UI menus
- Purpose: Core game object abstraction; all in-game actors (players, NPCs, monsters, doodads) are `PeEntity` instances
- Location: `Assets/Scripts/PeEntity/`
- Contains: `PeEntity.cs` (partial, versioned binary serialization), `EntityMgr.cs` (singleton registry), `PeCmpt.cs` (abstract base), `PeEventGlobal.cs`
- Sub-directory: `Assets/Scripts/PeEntity/Cmpt/` — 126 component files covering animation, avatar, biology view, combat, colony, inventory, NPC, motion, networking, skills, etc.
- Depends on: `SkillSystem`, `AssetsLoader`, Unity MonoBehaviour
- Used by: All gameplay systems
- Purpose: NPC and monster decision-making
- Location: `Assets/Scripts/AiScripts/`, `Assets/Scripts/Behave/`, `Assets/Scripts/BehaviorTree/`
- Contains: Behavior trees (`Behave.Runtime.BTResolver`), AI steering (`UnitySteer`), AI network controllers
- Depends on: `PeEntity` layer, `SkillSystem`, pathfinding
- Used by: NPC/monster entities via `BehaveCmpt`
- Purpose: Attribute system, skill execution, damage/buff application
- Location: `Assets/Scripts/SkillSystem/`
- Contains: `SkEntity.cs`, `SkAttribs.cs`, `SkBuffs.cs`, `SkEffect.cs`, `SkInst.cs`, `SkillRunner.cs`
- Depends on: `PeEntity` layer
- Used by: Combat, AI, player actions
- Purpose: Voxel terrain (Block45 engine), procedural world generation
- Location: `Assets/Block45/`
- Contains: `B45ChunkData.cs`, `B45ChunkGo.cs`, `Block45Building.cs`, LOD octree
- Depends on: Unity renderer
- Used by: Terrain, building/dig systems
- Purpose: Player construction and terrain modification
- Location: `Assets/Scripts/BuildNDig/`
- Contains: `BuildBlockManager.cs`, `DigTerrainManager.cs`, `EditBuilding.cs`, `TownEditor.cs`
- Depends on: Block45 voxel layer, `PeEntity` layer
- Purpose: Client/server multiplayer synchronization
- Location: `Assets/Scripts/GameNetwork/`, `Assets/Network/`, `Assets/Scripts/P2P/`
- Contains: `NetworkManager.cs`, `NetworkInterface.cs`, `PacketType.cs`, `MessageHandlers.cs`, Owner/Proxy prefab pairs per entity type
- Depends on: uLink network library, `PeEntity` layer
- Purpose: Persistent game state serialization
- Location: `Assets/GameArchive/`
- Contains: `Archive.cs`, `ArchiveMgr.cs`, `ArchiveIndex.cs`, `PeRecordWriter.cs`, `PeRecordReader.cs`, `SwapSpace.cs`
- Depends on: `PeEntity` binary serialization
- Purpose: Quest logic, story scripting, narrative flow
- Location: `Assets/Scripts/Mission/`
- Contains: `Mission.cs`, `StroyManager.cs`, `PlayerMission.cs`, `RandomMission.cs`, `MissionScript/`, `StoryRepository.cs`
- Depends on: `PeEntity` layer, `EntityMgr`
- Purpose: Colony base building, colony building types, NPC management
- Location: `Assets/Scripts/Colony/`, `Assets/Scripts/NPC/`
- Contains: `ColonyBase.cs`, `ColonyFactory.cs`, `ColonyFarm.cs`, `ColonyStorage.cs`, `NpcStorage.cs`, `NpcThinking.cs`
- Depends on: `PeEntity` layer
- Purpose: All in-game and menu UI
- Location: `Assets/Scripts/Chenzhi/NewUI/`
- Contains: `PE_GameUI/` (HUD, windows), `GameUISystem/`, `UIComponent/`
- Depends on: NGUI, `PeEntity` layer, game systems
- Used by: All player-facing interactions
- Purpose: Game data definitions and SQLite-backed localization
- Location: `Assets/Scripts/Database/`, `Assets/Scripts/Items/`
- Contains: `LocalDatabase.cs`, `SqliteAccessCS.cs`, `ItemData.cs`, `ItemManager.cs`
- Root: `i18n.db` (SQLite, 2.6 MB, internationalization strings)
- Location: `Assets/Scripts/Assist/`, `Assets/Scripts/PETools/`
- Contains: `Singleton.cs`, `PESingleton<T>`, `PEUtil.cs`, `PETools.cs`, `XmlUtil.cs`
## Data Flow
- Scene mode held in `PeGameMgr.ESceneMode` static field
- Global flags (`IsMultiMode`, `IsNight`, `IsInVCE`) on `GameConfig` and `PeGameMgr`
- Per-entity state via component pattern; `CmptMgr` centralizes update dispatching
## Key Abstractions
- Purpose: Unified game actor (player, NPC, monster, doodad, building)
- Examples: `Assets/Scripts/PeEntity/PeEntity.cs`
- Pattern: Partial class across multiple files; wraps Unity GameObject; delegates to `IPeCmpt` components for all behavior
- Purpose: Serializable, addressable component attached to a `PeEntity`
- Examples: `Assets/Scripts/PeEntity/PeCmpt.cs`, all files in `Assets/Scripts/PeEntity/Cmpt/`
- Pattern: Abstract base with virtual `Serialize`/`Deserialize`; auto-registers with `CmptMgr` and `IPeMsg` on `Start`
- Purpose: Single-instance service access
- Examples: `Assets/Scripts/Assist/Singleton.cs` — `Singleton<T>` (MonoBehaviour, DontDestroyOnLoad), `PESingleton<T>` (plain class, lazy-create)
- Pattern: All major managers (`EntityMgr`, `ArchiveMgr`, `PeEventGlobal`, etc.) use one of these
- Purpose: Type-safe event bus for entity lifecycle and game events
- Examples: `Assets/Scripts/PeEvent/PeEvent.cs`, `Assets/Scripts/PeEntity/PeEventGlobal.cs`
- Pattern: Typed `UnityEvent` subclasses; listeners subscribe via `AddListener()`
- Purpose: Entity prototype enum (Monster, Npc, Doodad, RandomNpc, etc.) used throughout to dispatch behavior by type
- Referenced in: `PeEntity`, `EntityMgr`, `PeLogicGlobal`, `SceneEntityCreator`
## Entry Points
- Location: `Assets/Scripts/Global/GlobalBehaviour.cs` (Awake)
- Triggers: Unity scene load on the persistent global GameObject
- Responsibilities: Loads `LocalDatabase`, initializes `PeLogicGlobal`, `PeCamera`, voxel surface extractors, behavior tree cache
- Location: `Assets/PeLauncher/PeGameMgr.cs` (static methods), `PeGameLoader.cs`
- Triggers: Menu selections, scene transitions
- Responsibilities: Sets `EPlayerType`, `ESceneMode`; calls `PeSingletonRunner.Launch()`; initializes auto-save
- Location: `Assets/Scripts/PeEntity/EntityMgr.cs` (`Create`, `InitEntity`)
- Triggers: Scene loading, spawners, network messages
- Responsibilities: Instantiates prefab via `AssetsLoader`, assigns ID, registers in entity registry
- Location: `Assets/Scripts/GameNetwork/NetworkManager.cs`, `Assets/Scripts/GameNetwork/MessageHandlers.cs`
- Triggers: Server/client connection
- Responsibilities: Routes `PacketType`-keyed messages to handler methods
## Error Handling
- Entity creation failures return `null` with `Debug.LogError` (see `EntityMgr.Create`)
- Component lookups return `null` if missing; callers guard with `if (cmpt != null)`
- `try/catch` at `PeEntity.Create` to log exceptions without crashing the frame
- Archive version mismatches log error and return early
## Cross-Cutting Concerns
<!-- GSD:architecture-end -->

<!-- GSD:workflow-start source:GSD defaults -->
## GSD Workflow Enforcement

Before using Edit, Write, or other file-changing tools, start work through a GSD command so planning artifacts and execution context stay in sync.

Use these entry points:
- `/gsd:quick` for small fixes, doc updates, and ad-hoc tasks
- `/gsd:debug` for investigation and bug fixing
- `/gsd:execute-phase` for planned phase work

Do not make direct repo edits outside a GSD workflow unless the user explicitly asks to bypass it.
<!-- GSD:workflow-end -->



<!-- GSD:profile-start -->
## Developer Profile

> Profile not yet configured. Run `/gsd:profile-user` to generate your developer profile.
> This section is managed by `generate-claude-profile` -- do not edit manually.
<!-- GSD:profile-end -->
