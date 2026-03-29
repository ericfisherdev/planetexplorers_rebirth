# Architecture

**Analysis Date:** 2026-03-28

## Pattern Overview

**Overall:** Unity MonoBehaviour-based Entity-Component System (ECS-like), with a custom `PeEntity`/`PeCmpt` component layer on top of Unity's native component model.

**Key Characteristics:**
- Entities (`PeEntity`) are Unity GameObjects with composable `PeCmpt` components carrying game logic and serialization
- Global singletons (both `Singleton<T>` MonoBehaviour and `PESingleton<T>` plain-class variants) coordinate cross-cutting systems
- Event-driven decoupling via typed `PeEvent.Event<T>` and `PeEventGlobal` for lifecycle signals (death, pickup, revive, destroy, HP change)
- Client/server multiplayer split via uLink network layer with Owner/Proxy prefab pairs
- Custom binary serialization (`Serialize`/`Deserialize`) on every `IPeCmpt` for save/load

## Layers

**Bootstrap / Global Layer:**
- Purpose: Application startup, data loading, cross-scene persistence
- Location: `Assets/Scripts/Global/`
- Contains: `GlobalBehaviour.cs` (MonoBehaviour on persistent GameObject), `GameTime.cs`, `SceneMediator.cs`
- Depends on: `LocalDatabase`, `PeLogicGlobal`, `PeCamera`, all singletons
- Used by: Unity Engine (called via `Awake`/`Update`/`LateUpdate`)

**Game Management Layer:**
- Purpose: Game mode, scene flow, archive/save orchestration
- Location: `Assets/PeLauncher/`
- Contains: `PeGameMgr.cs` (static class, player type/scene mode enums), `PeFlowMgr.cs`, `PeGameLoader.cs`, `ArchiveMgr.cs`, `SingleGame.cs`, `MultiGame.cs`
- Depends on: `PeEntity` layer, `ArchiveMgr`
- Used by: Scene loading, UI menus

**Entity / Component Layer:**
- Purpose: Core game object abstraction; all in-game actors (players, NPCs, monsters, doodads) are `PeEntity` instances
- Location: `Assets/Scripts/PeEntity/`
- Contains: `PeEntity.cs` (partial, versioned binary serialization), `EntityMgr.cs` (singleton registry), `PeCmpt.cs` (abstract base), `PeEventGlobal.cs`
- Sub-directory: `Assets/Scripts/PeEntity/Cmpt/` — 126 component files covering animation, avatar, biology view, combat, colony, inventory, NPC, motion, networking, skills, etc.
- Depends on: `SkillSystem`, `AssetsLoader`, Unity MonoBehaviour
- Used by: All gameplay systems

**AI / Behavior Layer:**
- Purpose: NPC and monster decision-making
- Location: `Assets/Scripts/AiScripts/`, `Assets/Scripts/Behave/`, `Assets/Scripts/BehaviorTree/`
- Contains: Behavior trees (`Behave.Runtime.BTResolver`), AI steering (`UnitySteer`), AI network controllers
- Depends on: `PeEntity` layer, `SkillSystem`, pathfinding
- Used by: NPC/monster entities via `BehaveCmpt`

**Skill / Combat Layer:**
- Purpose: Attribute system, skill execution, damage/buff application
- Location: `Assets/Scripts/SkillSystem/`
- Contains: `SkEntity.cs`, `SkAttribs.cs`, `SkBuffs.cs`, `SkEffect.cs`, `SkInst.cs`, `SkillRunner.cs`
- Depends on: `PeEntity` layer
- Used by: Combat, AI, player actions

**World / Voxel Layer:**
- Purpose: Voxel terrain (Block45 engine), procedural world generation
- Location: `Assets/Block45/`
- Contains: `B45ChunkData.cs`, `B45ChunkGo.cs`, `Block45Building.cs`, LOD octree
- Depends on: Unity renderer
- Used by: Terrain, building/dig systems

**Build & Dig Layer:**
- Purpose: Player construction and terrain modification
- Location: `Assets/Scripts/BuildNDig/`
- Contains: `BuildBlockManager.cs`, `DigTerrainManager.cs`, `EditBuilding.cs`, `TownEditor.cs`
- Depends on: Block45 voxel layer, `PeEntity` layer

**Networking Layer:**
- Purpose: Client/server multiplayer synchronization
- Location: `Assets/Scripts/GameNetwork/`, `Assets/Network/`, `Assets/Scripts/P2P/`
- Contains: `NetworkManager.cs`, `NetworkInterface.cs`, `PacketType.cs`, `MessageHandlers.cs`, Owner/Proxy prefab pairs per entity type
- Depends on: uLink network library, `PeEntity` layer

**Save/Archive Layer:**
- Purpose: Persistent game state serialization
- Location: `Assets/GameArchive/`
- Contains: `Archive.cs`, `ArchiveMgr.cs`, `ArchiveIndex.cs`, `PeRecordWriter.cs`, `PeRecordReader.cs`, `SwapSpace.cs`
- Depends on: `PeEntity` binary serialization

**Mission / Story Layer:**
- Purpose: Quest logic, story scripting, narrative flow
- Location: `Assets/Scripts/Mission/`
- Contains: `Mission.cs`, `StroyManager.cs`, `PlayerMission.cs`, `RandomMission.cs`, `MissionScript/`, `StoryRepository.cs`
- Depends on: `PeEntity` layer, `EntityMgr`

**Colony / NPC Systems:**
- Purpose: Colony base building, colony building types, NPC management
- Location: `Assets/Scripts/Colony/`, `Assets/Scripts/NPC/`
- Contains: `ColonyBase.cs`, `ColonyFactory.cs`, `ColonyFarm.cs`, `ColonyStorage.cs`, `NpcStorage.cs`, `NpcThinking.cs`
- Depends on: `PeEntity` layer

**UI Layer:**
- Purpose: All in-game and menu UI
- Location: `Assets/Scripts/Chenzhi/NewUI/`
- Contains: `PE_GameUI/` (HUD, windows), `GameUISystem/`, `UIComponent/`
- Depends on: NGUI, `PeEntity` layer, game systems
- Used by: All player-facing interactions

**Data / Database Layer:**
- Purpose: Game data definitions and SQLite-backed localization
- Location: `Assets/Scripts/Database/`, `Assets/Scripts/Items/`
- Contains: `LocalDatabase.cs`, `SqliteAccessCS.cs`, `ItemData.cs`, `ItemManager.cs`
- Root: `i18n.db` (SQLite, 2.6 MB, internationalization strings)

**Utilities / Tools Layer:**
- Location: `Assets/Scripts/Assist/`, `Assets/Scripts/PETools/`
- Contains: `Singleton.cs`, `PESingleton<T>`, `PEUtil.cs`, `PETools.cs`, `XmlUtil.cs`

## Data Flow

**Entity Lifecycle:**
1. `PeGameMgr` / `PeGameLoader` initiates scene load
2. `SceneEntityCreator` / `EntityMgr.Create()` instantiates prefab via `AssetsLoader.InstantiateAssetImm()`
3. `PeEntity` attaches; components auto-register via `PeCmpt.Start()` → `CmptMgr.Instance.AddCmpt()`
4. Entity registered in `EntityMgr.mDicEntity` and `m_Entities` list
5. Components run game logic via `OnUpdate()` each frame

**Save/Load:**
1. `ArchiveMgr.Save()` iterates all `PeEntity` instances
2. `PeEntity.Export(BinaryWriter)` writes version, proto, then each `IPeCmpt.Serialize()`
3. Load reverses: `PeEntity.Import(byte[])` reads version, restores component state via `Deserialize()`

**Multiplayer Message Flow:**
1. Owner peer sends packet via `NetworkInterface`
2. `MessageHandlers.cs` routes by `PacketType` enum
3. AI/entity network objects (e.g., `AiMonsterNetwork`, `PlayerOwner`/`PlayerProxy`) apply state changes on proxy peers
4. P2P managed through `P2PManager` and uLink

**Entity Death/Revive:**
1. `SkillSystem` reduces HP → fires `PeEventGlobal.DeathEvent`
2. `PeLogicGlobal.OnEntityDeath()` determines revive time by proto type
3. Coroutine schedules revive or `DestroyEnumerator` with visual fade-out
4. On revive: `MotionMgrCmpt.DoAction(PEActionType.Revive)`

**State Management:**
- Scene mode held in `PeGameMgr.ESceneMode` static field
- Global flags (`IsMultiMode`, `IsNight`, `IsInVCE`) on `GameConfig` and `PeGameMgr`
- Per-entity state via component pattern; `CmptMgr` centralizes update dispatching

## Key Abstractions

**PeEntity:**
- Purpose: Unified game actor (player, NPC, monster, doodad, building)
- Examples: `Assets/Scripts/PeEntity/PeEntity.cs`
- Pattern: Partial class across multiple files; wraps Unity GameObject; delegates to `IPeCmpt` components for all behavior

**IPeCmpt / PeCmpt:**
- Purpose: Serializable, addressable component attached to a `PeEntity`
- Examples: `Assets/Scripts/PeEntity/PeCmpt.cs`, all files in `Assets/Scripts/PeEntity/Cmpt/`
- Pattern: Abstract base with virtual `Serialize`/`Deserialize`; auto-registers with `CmptMgr` and `IPeMsg` on `Start`

**Singleton / PESingleton:**
- Purpose: Single-instance service access
- Examples: `Assets/Scripts/Assist/Singleton.cs` — `Singleton<T>` (MonoBehaviour, DontDestroyOnLoad), `PESingleton<T>` (plain class, lazy-create)
- Pattern: All major managers (`EntityMgr`, `ArchiveMgr`, `PeEventGlobal`, etc.) use one of these

**PeEvent / PeEventGlobal:**
- Purpose: Type-safe event bus for entity lifecycle and game events
- Examples: `Assets/Scripts/PeEvent/PeEvent.cs`, `Assets/Scripts/PeEntity/PeEventGlobal.cs`
- Pattern: Typed `UnityEvent` subclasses; listeners subscribe via `AddListener()`

**EEntityProto:**
- Purpose: Entity prototype enum (Monster, Npc, Doodad, RandomNpc, etc.) used throughout to dispatch behavior by type
- Referenced in: `PeEntity`, `EntityMgr`, `PeLogicGlobal`, `SceneEntityCreator`

## Entry Points

**Application Start:**
- Location: `Assets/Scripts/Global/GlobalBehaviour.cs` (Awake)
- Triggers: Unity scene load on the persistent global GameObject
- Responsibilities: Loads `LocalDatabase`, initializes `PeLogicGlobal`, `PeCamera`, voxel surface extractors, behavior tree cache

**Scene/Game Load:**
- Location: `Assets/PeLauncher/PeGameMgr.cs` (static methods), `PeGameLoader.cs`
- Triggers: Menu selections, scene transitions
- Responsibilities: Sets `EPlayerType`, `ESceneMode`; calls `PeSingletonRunner.Launch()`; initializes auto-save

**Entity Creation:**
- Location: `Assets/Scripts/PeEntity/EntityMgr.cs` (`Create`, `InitEntity`)
- Triggers: Scene loading, spawners, network messages
- Responsibilities: Instantiates prefab via `AssetsLoader`, assigns ID, registers in entity registry

**Network Entry:**
- Location: `Assets/Scripts/GameNetwork/NetworkManager.cs`, `Assets/Scripts/GameNetwork/MessageHandlers.cs`
- Triggers: Server/client connection
- Responsibilities: Routes `PacketType`-keyed messages to handler methods

## Error Handling

**Strategy:** Log-and-continue using `Debug.LogError` / `Debug.LogWarning`; null-check guards on entity/component references before use.

**Patterns:**
- Entity creation failures return `null` with `Debug.LogError` (see `EntityMgr.Create`)
- Component lookups return `null` if missing; callers guard with `if (cmpt != null)`
- `try/catch` at `PeEntity.Create` to log exceptions without crashing the frame
- Archive version mismatches log error and return early

## Cross-Cutting Concerns

**Logging:** `Debug.Log` / `Debug.LogError` directly from UnityEngine; `LogManager.cs` (`Assets/Scripts/Log/`) provides a custom log manager wrapper.

**Validation:** Inline null checks and ID validation (`IdGenerator.Invalid`) at creation sites.

**Authentication:** Not applicable (local game + uLink peer authentication for multiplayer).

**Localization:** SQLite database `i18n.db` at project root; accessed via `LocalDatabase.cs` and `SqliteAccessCS.cs`.

---

*Architecture analysis: 2026-03-28*
