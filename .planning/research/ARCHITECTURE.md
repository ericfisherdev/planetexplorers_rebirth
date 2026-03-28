# Architecture Patterns

**Domain:** Legacy Unity game revival from decompiled/open-sourced C# codebase
**Researched:** 2026-03-28

## Recommended Architecture

### Overall Pattern: Stub-Then-Replace

The project cannot be compiled from source because ~10 proprietary plugin DLLs are missing. The architecture strategy is:

1. **Create stub DLLs** that define all missing types with empty implementations
2. **Achieve clean compilation** with stubs (game loads but features don't work)
3. **Replace stubs incrementally** with working implementations, one system at a time
4. **Test each replacement** against the existing game code before moving to the next

This is a dependency inversion at the build level: game code depends on interfaces (type signatures), not implementations.

### Component Boundaries

| Component | Responsibility | Dependencies | Status |
|-----------|---------------|--------------|--------|
| **Stub DLLs** (`Assets/Plugins/`) | Provide type definitions for missing plugins | None (leaf dependencies) | Must create |
| **Voxelform2** (`Assets/Terrain/Voxelform2/`) | Terrain generation, marching cubes, rendering | OpenCLNet (GPU compute), Unity built-ins | Source in repo |
| **Block45** (`Assets/Block45/`) | Voxel building/editing system | Unity built-ins | Source in repo |
| **Entity System** (`Assets/Scripts/PeEntity/`) | Player, NPC, creature entities and components | FinalIK (stub), Pathfinding (stub) | Source in repo, needs stubs |
| **AI System** (`Assets/Scripts/AiScripts/`, `Assets/Scripts/BehaviorTree/`) | NPC behavior, pathfinding, steering | A* Pathfinding, Behave, UnitySteer | Partially in repo, needs replacements |
| **UI Layer** (`Assets/Scripts/Chenzhi/NewUI/`, `Assets/Scripts/GameUITest/`) | All game UI | NGUI (417 files) | Source in repo but depends entirely on NGUI |
| **Networking** (`Assets/Scripts/GameNetwork/`) | Multiplayer (stubbed for single-player) | uLink, uLobby | Stub completely |
| **Audio** (`Assets/PeAudio/`) | Sound effects, music, ambient | FMOD Studio | Replace with Unity AudioSource |
| **Creation System** (`Assets/Scripts/CreationSystem/`) | ISO creation, item/vehicle design | Unity built-ins | Source in repo |
| **World Management** (`Assets/PeWorld/`, `Assets/PeLauncher/`) | Scene loading, world state, entity spawning | SQLite, XML configs | Source in repo |
| **Steam Integration** (`Assets/Scripts/SteamWorks/`, `Assets/Scripts/Steamwork.NET/`) | Platform services | Steamworks.NET | Stub completely |

### Data Flow

```
Game Boot
  -> PeLauncher/PeGameLoader.cs loads world config (XML, SQLite)
  -> Voxelform2 generates terrain from seed
  -> PeEntityCreator spawns player entity
  -> Entity system initializes components (Motor, Animation, IK, Combat)
  -> UI system (NGUI) renders HUD, inventory, menus
  -> Game loop: Input -> Entity updates -> AI ticks -> Physics -> Rendering

Save/Load
  -> BinaryWriter serializes world state, entity state, inventory
  -> BinaryReader deserializes on load
  -> SQLite stores structured data (names, i18n, map labels)
```

## Patterns to Follow

### Pattern 1: Stub DLL with Matching API Surface

**What:** Create a .NET 3.5 class library that defines all types referenced by game code, with empty method bodies.
**When:** For every missing proprietary plugin.
**Why:** Achieves compilation without requiring any game code changes. Game code calls into stubs harmlessly.

**Example:**
```csharp
// In NGUI_Stub/UILabel.cs
using UnityEngine;

namespace NGUI // or whatever namespace NGUI uses
{
    public class UILabel : MonoBehaviour
    {
        public string text { get; set; }
        public Color color { get; set; }
        public int fontSize { get; set; }
        public float alpha { get; set; }
        // ... add properties as compilation errors reveal them
    }
}
```

**Key detail:** NGUI types like UILabel, UISprite, UIPanel are NOT namespaced -- they live in the global namespace. The stub must match this exactly.

### Pattern 2: Iterative Stub Expansion

**What:** Start with minimal stubs, add members as the compiler tells you what's missing.
**When:** Building stub DLLs.
**Why:** You cannot predict every property and method used across 417 files. Let the compiler guide you.

**Process:**
1. Create stub with class name only
2. Compile project
3. Read first batch of errors (CS0246, CS0117, CS1061)
4. Add missing types/members to stub
5. Repeat until zero errors

### Pattern 3: Conditional Compilation for Network Code

**What:** Use `#if` preprocessor directives or runtime flags to bypass networking code paths.
**When:** Network-related code that can't be simply stubbed.
**Why:** Some network code may be interleaved with gameplay logic (e.g., checking `if (isServer)` before spawning entities).

**Example:**
```csharp
// In uLink stub
public class NetworkView : MonoBehaviour
{
    public bool isMine { get { return true; } } // Always "my" object in single-player
    public bool isServer { get { return true; } } // Act as server in single-player
}
```

### Pattern 4: Adapter Layer for Plugin Replacement

**What:** When replacing a stub with a real implementation, create an adapter that matches the original API but delegates to the new library.
**When:** Replacing A* Pathfinding Pro with A* Free, Behave with NPBehave.
**Why:** Minimizes changes to game code. Game code keeps calling the same API; only the adapter implementation changes.

## Anti-Patterns to Avoid

### Anti-Pattern 1: Rewriting Game Code to Remove Dependencies

**What:** Modifying 417 UI files to stop using NGUI types.
**Why bad:** Months of tedious work before anything compiles. High risk of introducing bugs in code you don't fully understand.
**Instead:** Stub NGUI first, achieve compilation, understand the codebase, then port incrementally.

### Anti-Pattern 2: Upgrading Unity Version

**What:** Migrating to Unity 2019+ or Unity 6 for better tooling and Linux editor support.
**Why bad:** Unity 5.2 -> Unity 2019+ involves: deprecated API migration, new rendering pipeline, different UI system, serialization format changes, shader language changes, and .NET runtime changes. This is a larger project than the game revival itself.
**Instead:** Stay on Unity 5.2.4f1. Accept the Windows-only editor constraint.

### Anti-Pattern 3: Building All Stubs at Once Without Compiling

**What:** Trying to define every stub type perfectly before attempting compilation.
**Why bad:** You will miss things. Some types are used implicitly (base classes, generic constraints). Only the compiler knows the full dependency graph.
**Instead:** Use iterative stub expansion (Pattern 2).

### Anti-Pattern 4: Replacing All Plugins Simultaneously

**What:** Trying to get A*, FinalIK, Behave, FMOD, and NGUI all working at once.
**Why bad:** Too many variables. When something breaks, you won't know which replacement caused it.
**Instead:** Replace one plugin at a time. Test after each replacement. Keep the rest as stubs.

## Stub DLL Inventory

Based on codebase analysis, these stub DLLs are needed:

| Stub DLL | Types to Define | Estimated Complexity | Priority |
|----------|-----------------|---------------------|----------|
| NGUI (no namespace) | UILabel, UIPanel, UISprite, UIButton, UIInput, UIGrid, UITable, UIAtlas, UIWidget, UICamera, UIRoot, UIScrollView, UIWrapContent, UIToggle, UISlider, UIPopupList, UIProgressBar, TweenAlpha, TweenPosition, TweenScale, TweenColor, SpringPanel, UIPlayTween, UIEventListener, EventDelegate, UIRect, UIBasicSprite, UIDrawCall | High (30+ types) | P0 |
| uLink | MonoBehaviour (uLink.MonoBehaviour), NetworkView, NetworkPlayer, RPCMode, BitStream, NetworkMessageInfo | Medium (10+ types) | P0 |
| uLobby | Lobby, LobbyPlayer, LobbyPeer | Low (5+ types) | P0 |
| RootMotion.FinalIK | FullBodyBipedIK, LookAtIK, AimIK, IKSolverFullBodyBiped, IKEffector, FBBIKHeadEffector, Interactor, InteractionSystem, HitReaction, Inertia, OffsetModifier | Medium (15+ types) | P1 |
| FMOD/FMODUnity | Studio.EventInstance, Studio.System, Studio.Bank, FMOD.RESULT, RuntimeManager | Medium (10+ types) | P1 |
| Pathfinding (A*) | AstarPath, ABPath, Path, Seeker, GraphNode, RVOController, RVOSimulator | Medium (10+ types) | P1 |
| Behave.Runtime | Tree, BLBlackboard, Agent | Low (5+ types) | P1 |
| Steamworks | SteamAPI, SteamManager, SteamFriends, SteamUser, SteamUGC, PublishedFileId_t, CallResult, Callback | Medium (15+ types) | P2 |

## Scalability Considerations

Not applicable in the traditional sense (this is a single-player game, not a web service). However:

| Concern | Current State | If Multiplayer Added Later |
|---------|--------------|---------------------------|
| Networking | Stubbed (no-op) | Would need full networking library. Mirror requires Unity 2019+, so would need a Unity-5.2-compatible solution or custom implementation. |
| World size | Original game handles it | No change needed |
| Entity count | Original game handles it | Server authority model needed |
| Save file size | Binary serialization | Would need sync protocol |

## Sources

- Codebase grep analysis for dependency mapping
- [PEPatch source](https://github.com/radistmorse/PEPatch) -- demonstrates Harmony patching approach (different from our source rebuild)
- [ngui-to-ugui](https://github.com/mamoniem/ngui-to-ugui) -- NGUI to uGUI conversion tool
- [A* Pathfinding Project](https://arongranberg.com/astar/) -- free version available
- [NPBehave](https://github.com/meniku/NPBehave) -- behavior tree replacement

---

*Architecture research: 2026-03-28*
