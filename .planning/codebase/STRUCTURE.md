# Codebase Structure

**Analysis Date:** 2026-03-28

## Directory Layout

```
planetexplorers_rebirth/
├── Assets/                     # All Unity project content
│   ├── Scripts/                # Primary C# gameplay code (~2,888 .cs files total)
│   │   ├── PeEntity/           # Core entity/component framework
│   │   │   └── Cmpt/           # 126 entity component implementations
│   │   ├── Global/             # App bootstrap, timers, scene mediation
│   │   ├── AiScripts/          # AI motor, spawning, projectiles, UnitySteer
│   │   ├── Behave/             # Behavior tree runtime (actions, data, operations)
│   │   ├── BehaviorTree/       # Behavior tree editor/data assets
│   │   ├── SkillSystem/        # Attribute, skill, buff, effect engine
│   │   ├── Skills/             # Skill book, runner, merge skill
│   │   ├── GameNetwork/        # Network manager, packet types, message handlers
│   │   ├── P2P/                # P2P prefabs and P2PManager
│   │   ├── Mission/            # Quest/story logic, story manager
│   │   ├── NPC/                # NPC talk, storage, thinking, follower
│   │   ├── Colony/             # Colony buildings (factory, farm, power, etc.)
│   │   ├── BuildNDig/          # Player build and terrain dig managers
│   │   ├── Items/              # Item data, manager, inventory
│   │   ├── Scene/              # Scene entity creators, scene manager
│   │   ├── Database/           # SQLite access, LocalDatabase loader
│   │   ├── System/             # GameConfig, utility data structures
│   │   ├── Assist/             # Singleton bases, utility helpers
│   │   ├── PETools/            # Math and general tool utilities
│   │   ├── PeEvent/            # Typed event system
│   │   ├── Motion/             # Motion/animation helpers
│   │   ├── Sound/              # Audio manager, BGM controllers
│   │   ├── Log/                # LogManager
│   │   ├── AssetsLoader/       # Asset bundle XML + loader singleton
│   │   ├── CreateSystem/       # Creation/crafting system (ZhouXun)
│   │   ├── Player/             # Player-specific components
│   │   ├── Operate/            # Interactive actions (eat, sleep, ride, etc.)
│   │   ├── Attack/             # Attack logic
│   │   ├── Effect/             # Visual effect management
│   │   ├── Input/              # Input handling (PeInput)
│   │   ├── Chenzhi/            # UI system (NewUI, GraphMapping, MLoginGame)
│   │   │   └── NewUI/Scripts/
│   │   │       ├── PE_GameUI/  # HUD windows, minimap, health bars
│   │   │       ├── GameUISystem/
│   │   │       └── UIComponent/
│   │   ├── Jaluca/             # Developer sandbox scripts
│   │   ├── LiuZhichen/         # Developer sandbox scripts
│   │   ├── WuYiqiu/            # Developer sandbox scripts
│   │   ├── ZhangShunBo/        # Developer sandbox scripts
│   │   ├── ZhouXun/            # Voxel creation, UI tree grid, dev scripts
│   │   ├── PuJi/               # Developer sandbox scripts
│   │   └── [many more domains] # RailwaySystem, ReputationSystem, SkillTree, etc.
│   ├── Block45/                # Voxel terrain engine (B45 chunk data, LOD, mesh)
│   │   ├── Block45DataStructure/
│   │   ├── Scripts/
│   │   ├── IO/
│   │   └── Materials/
│   ├── PeEntity/               # Entity-related assets (PeCreature, PeWorld stub)
│   ├── PeLauncher/             # Game manager, archive manager, flow manager
│   ├── GameArchive/            # Save/load system (Archive, ArchiveMgr, SwapSpace)
│   ├── PeMap/                  # Minimap, mask tiles
│   ├── PatheaScript/           # Core scripting (PatheaScript DSL runtime)
│   ├── PeAudio/                # Audio assets and test
│   ├── PeCamera/               # Camera system
│   ├── PeEnvironment/          # Environment/weather system
│   ├── PeWorld/                # World stub (PeWorld.cs, IdGenerator, PeCreature)
│   ├── PeCustomGame/           # Custom game mode support
│   ├── Network/                # Network prefabs (Owner/Proxy pairs per entity type)
│   ├── Scenes/                 # Unity scene files (.unity)
│   ├── Resources/              # Runtime-loaded assets (prefabs, particles, etc.)
│   ├── Prefabs/                # Pre-built prefabs
│   ├── Editor/                 # Unity Editor-only scripts
│   ├── Gizmos/                 # Editor gizmo assets
│   ├── AppearBuilder/          # Appearance customization tool
│   ├── Camera Forge/           # Camera controller asset
│   ├── Nova Environment/       # Environment asset
│   ├── Particle/               # Particle effect assets
│   ├── RedGrass/               # Terrain vegetation
│   ├── River Tool/             # River generation asset
│   ├── Terrain/                # Terrain assets
│   └── [third-party assets]    # AssetStoreTools, NGUI (in Chenzhi/NewUI)
├── ConfigFiles/                # Runtime config (ClientConfig.conf, MapConfig.xml, version.txt)
├── CustomGames/                # Custom game definitions (Dragon Desert, Lost Planet, etc.)
├── CustomSounds/               # User-replaceable sound files
├── TutorialMode/               # Tutorial mode content
├── ProjectSettings/            # Unity project settings (Unity 5.2.4f1)
├── RandomTownArt/              # Random town art assets
├── i18n.db                     # SQLite localization database (2.6 MB)
├── config                      # Mono DLL mapping config (platform shims)
└── README.md                   # Open source terms of use (Pathea Games)
```

## Directory Purposes

**`Assets/Scripts/PeEntity/`:**
- Purpose: The core entity-component framework; all game actors use this
- Contains: `PeEntity.cs` (partial), `EntityMgr.cs`, `PeCmpt.cs`, `IPeCmpt` interface, `PeEventGlobal.cs`, `CmptMgr.cs`
- Key files: `Assets/Scripts/PeEntity/PeEntity.cs`, `Assets/Scripts/PeEntity/EntityMgr.cs`, `Assets/Scripts/PeEntity/PeCmpt.cs`

**`Assets/Scripts/PeEntity/Cmpt/`:**
- Purpose: All concrete entity components (126 files)
- Contains: `CommonCmpt.cs`, `ViewCmpt.cs`, `BiologyViewCmpt.cs`, `NpcCmpt.cs`, `MotionMgrCmpt.cs`, `MainPlayerCmpt.cs`, `PeTrans.cs`, `PESkEntity.cs`, `PackageCmpt.cs`, etc.

**`Assets/Scripts/SkillSystem/`:**
- Purpose: Attribute/skill/buff system decoupled from entity type
- Contains: `SkEntity.cs` (attribute holder), `SkAttribs.cs`, `SkBuffs.cs`, `SkEffect.cs`, `SkillRunner.cs`

**`Assets/PeLauncher/`:**
- Purpose: Game lifecycle orchestration; mode, scene, save coordination
- Key files: `Assets/PeLauncher/PeGameMgr.cs`, `Assets/PeLauncher/PeGameLoader.cs`, `Assets/PeLauncher/SingleGame.cs`

**`Assets/GameArchive/`:**
- Purpose: All persistence/save-game logic
- Key files: `Assets/GameArchive/ArchiveMgr.cs`, `Assets/GameArchive/Archive.cs`, `Assets/GameArchive/PeRecordWriter.cs`

**`Assets/Scripts/GameNetwork/`:**
- Purpose: Multiplayer networking; message routing, packet types, chunk sync
- Key files: `Assets/Scripts/GameNetwork/NetworkManager.cs`, `Assets/Scripts/GameNetwork/MessageHandlers.cs`, `Assets/Scripts/GameNetwork/PacketType.cs`

**`Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/`:**
- Purpose: All in-game HUD and window UI
- Contains: Health bars, minimap, game menu, NPC talk history, phone window

**`Assets/Block45/`:**
- Purpose: Voxel engine; chunk data, LOD octree, mesh merging, terrain I/O
- Key files: `Assets/Block45/Scripts/B45ChunkData.cs`, `Assets/Block45/Scripts/Block45Building.cs`

**`Assets/Scripts/Database/`:**
- Purpose: Game data loading (SQLite access, local data)
- Key files: `Assets/Scripts/Database/LocalDatabase.cs`, `Assets/Scripts/Database/SqliteAccessCS.cs`

**`Assets/Scripts/Assist/`:**
- Purpose: Reusable base classes and utility scripts used across all modules
- Key files: `Assets/Scripts/Assist/Singleton.cs` (defines `Singleton<T>` and `PESingleton<T>`)

## Key File Locations

**Entry Points:**
- `Assets/Scripts/Global/GlobalBehaviour.cs`: Application bootstrap (Awake, Update, LateUpdate)
- `Assets/PeLauncher/PeGameMgr.cs`: Game mode/scene state machine (static class)
- `Assets/PeLauncher/PeGameLoader.cs`: Scene loading orchestration

**Configuration:**
- `Assets/Scripts/System/GameConfig.cs`: All path constants, scene name constants, layer masks, mode flags
- `ConfigFiles/ClientConfig.conf`: Runtime client configuration
- `ConfigFiles/version.txt`: Game version string
- `ProjectSettings/ProjectSettings.asset`: Unity project settings (Unity 5.2.4f1)

**Core Logic:**
- `Assets/Scripts/PeEntity/PeEntity.cs`: Entity base (partial class)
- `Assets/Scripts/PeEntity/EntityMgr.cs`: Entity registry singleton
- `Assets/Scripts/PeEntity/PeCmpt.cs`: Component base/interface
- `Assets/Scripts/PeEntity/PeEventGlobal.cs`: Global lifecycle event bus
- `Assets/Scripts/PeEntity/PeLogicGlobal.cs`: Entity death/revive/destroy handler

**Networking:**
- `Assets/Scripts/GameNetwork/NetworkManager.cs`
- `Assets/Scripts/GameNetwork/MessageHandlers.cs`
- `Assets/Scripts/GameNetwork/PacketType.cs`

**Save/Load:**
- `Assets/GameArchive/ArchiveMgr.cs`
- `Assets/GameArchive/Archive.cs`

**Testing:**
- No test directory or test framework detected

## Naming Conventions

**Files:**
- PascalCase for all `.cs` files: `EntityMgr.cs`, `PeEventGlobal.cs`
- Suffix pattern: `Mgr` for managers (`EntityMgr`, `NetworkManager`, `ItemManager`), `Cmpt` for components (`NpcCmpt`, `MotionMgrCmpt`), `Wnd` for UI windows
- Network variants suffixed `Network` or `Net`: `AiMonsterNetwork`, `SkAttribsNet`
- `I` prefix for interfaces: `IPeCmpt`, `IPeMsg`, `ISkBase`
- Developer-named folders for personal work: `Chenzhi/`, `ZhouXun/`, `Jaluca/`, etc.

**Directories:**
- PascalCase: `Scripts/`, `PeEntity/`, `GameNetwork/`
- Prefixed with `Pe` for Pathea-namespaced engine systems: `PeLauncher`, `PeAudio`, `PeCamera`, `PeMap`, `PeWorld`
- Prefixed with `Pe` or `SK` in script names to signal subsystem ownership

**Namespaces:**
- Core framework: `namespace Pathea` (229+ files)
- Skill system: `namespace SkillSystem`
- Tools: `namespace PETools`
- Most older scripts: no namespace (global scope)

## Where to Add New Code

**New Entity Component:**
- Implementation: `Assets/Scripts/PeEntity/Cmpt/YourNewCmpt.cs`
- Extend `PeCmpt` (from `Assets/Scripts/PeEntity/PeCmpt.cs`) and implement `Serialize`/`Deserialize`
- Register with entity via `entity.Add<YourNewCmpt>()` or attach in prefab

**New Gameplay System:**
- Create a new subdirectory under `Assets/Scripts/YourSystem/`
- Add a manager as `Singleton<T>` (`Assets/Scripts/Assist/Singleton.cs`)
- Subscribe to `PeEventGlobal` events for entity lifecycle hooks

**New Network Message:**
- Add entry to `Assets/Scripts/GameNetwork/PacketType.cs`
- Add handler in `Assets/Scripts/GameNetwork/MessageHandlers.cs`
- Add Owner/Proxy script pair under `Assets/Network/`

**New UI Window:**
- Add under `Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/GameWnd/`
- Follow NGUI-based window pattern from existing windows in that directory

**New Mission/Story Script:**
- Add under `Assets/Scripts/Mission/MissionScript/`
- Register with `StroyManager.cs` or `MissionManager`

**New Item:**
- Data definition via `Assets/Scripts/Database/LocalDatabase.cs`
- Runtime logic via `Assets/Scripts/Items/ItemManager.cs`

**Utilities:**
- Shared helpers: `Assets/Scripts/Assist/` (if broadly reusable) or `Assets/Scripts/PETools/`
- Math utilities: `Assets/Scripts/PETools/PEMath.cs`

## Special Directories

**`Assets/Resources/`:**
- Purpose: Runtime-loaded assets accessible via `Resources.Load()`
- Generated: No
- Committed: Yes

**`Assets/Editor/`:**
- Purpose: Unity Editor-only scripts (not compiled into builds)
- Generated: No
- Committed: Yes

**`ProjectSettings/`:**
- Purpose: Unity engine settings (physics, input, quality, tags)
- Generated: Partially (by Unity Editor)
- Committed: Yes

**`ConfigFiles/`:**
- Purpose: Runtime text configs read by `GameConfig.cs` at startup
- Generated: No
- Committed: Yes

**`CustomGames/`:**
- Purpose: Bundled custom game definitions (Dragon Desert, Lost Planet Ch/En, Story Demo, Mainland)
- Generated: No
- Committed: Yes

**`.planning/`:**
- Purpose: GSD planning documents (codebase maps, phase plans)
- Generated: By GSD tooling
- Committed: Yes

---

*Structure analysis: 2026-03-28*
