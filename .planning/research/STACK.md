# Technology Stack

**Project:** Planet Explorers Rebirth
**Researched:** 2026-03-28

## Critical Correction: Unity Version

The PROJECT.md states "Unity 4.x" but this is **wrong**. The actual engine version is **Unity 5.2.4f1**, confirmed by:
- `ProjectSettings/ProjectVersion.txt` reads `m_EditorVersion: 5.2.4f1`
- Pathea Games developer Yixin confirmed "It uses Unity 5.2.4f1" in [GitHub Issue #1](https://github.com/pathea-games/planetexplorers/issues/1#issuecomment-610172984)

This matters because Unity 5.x has different API surfaces, shader systems, and plugin compatibility than Unity 4.x.

## Recommended Stack

### Core Engine

| Technology | Version | Purpose | Why | Confidence |
|------------|---------|---------|-----|------------|
| Unity Editor | 5.2.4f1 | Game engine, IDE, build tool | Must match original project exactly. Mismatched versions cause serialization errors with .asset files, prefabs, and scenes | HIGH |

**How to obtain Unity 5.2.4f1:**

1. **Unity Hub (preferred):** Install [Unity Hub](https://unity.com/download) via `unityhub` AUR package on Arch Linux. Use Archive tab to install 5.2.4f1.
2. **Official Archive:** Visit [unity.com/releases/editor/archive](https://unity.com/releases/editor/archive), navigate to 5.2.x section. Windows and Mac installers are available.
3. **Direct URL pattern:** `https://download.unity3d.com/download_unity/[hash]/UnitySetup-5.2.4f1.exe` -- the hash must be discovered from the archive page.

**Linux Editor Availability: NOT AVAILABLE for 5.2.x** (MEDIUM confidence)

Unity's Linux editor was experimental starting ~5.4 and officially supported from 2019.1+. Unity 5.2.4f1 has **no native Linux editor**. Options:

| Approach | Feasibility | Notes |
|----------|-------------|-------|
| Windows VM (recommended) | HIGH | Run Unity 5.2.4f1 in a Windows VM (QEMU/KVM with GPU passthrough or VirtualBox). Most reliable. |
| Wine/Proton | MEDIUM | Unity 5.x has been reported working under Wine with `-force-opengl` flag. The [Unity3D-on-Wine](https://github.com/Unity3D-Wine-Support/Unity3D-on-Wine) project (archived 2018) documented this. Expect shader preview issues. |
| Dual boot Windows | HIGH | Most friction-free for actual editor work. Keep Linux as primary, boot Windows for Unity. |
| Remote Windows build machine | HIGH | SSH/RDP into a Windows machine running Unity. Best of both worlds. |

**Recommendation:** Use a Windows VM with GPU passthrough for editor work. Build targets for both Linux and Windows from the Windows editor (Unity 5.2 supports Linux build targets from Windows).

### Proprietary Plugin Replacements

The codebase references ~10 proprietary plugins that are intentionally excluded from the repo. These must be replaced or stubbed. Listed by priority (impact on compilation and gameplay):

#### Tier 1: Must Replace for Compilation

| Original Plugin | Replacement Strategy | Why This Approach | Files Affected | Confidence |
|----------------|---------------------|-------------------|----------------|------------|
| **NGUI** (Next-Gen UI) | Write stub/shim DLL matching NGUI's public API | 417 files reference NGUI types (UILabel, UIPanel, UISprite, UIButton, etc.). Rewriting 417 files to uGUI is a multi-month effort. A stub DLL that provides empty implementations lets the project compile immediately. Functional UI comes later. | ~417 .cs files | HIGH |
| **uLink** (networking) | Write stub DLL with empty implementations | Core networking library. Since we're targeting single-player first, all uLink types (MonoBehaviour, NetworkView, RPCMode, BitStream) can be stubbed as no-ops. Only ~9 files have `using uLink` but many more use uLink types implicitly. | ~20+ .cs files | HIGH |
| **uLobby** (matchmaking) | Write stub DLL with empty implementations | Lobby/matchmaking -- entirely unnecessary for single-player. 4 files with `using uLobby`. | ~4 .cs files | HIGH |

#### Tier 2: Must Replace for Gameplay

| Original Plugin | Replacement Strategy | Why This Approach | Files Affected | Confidence |
|----------------|---------------------|-------------------|----------------|------------|
| **FMOD Studio** | Replace with Unity AudioSource wrapper | FMOD is used in ~9 files for audio playback. FMOD offers a free indie license (<$200k revenue), but for an open-source project, wrapping calls to Unity's built-in AudioSource/AudioClip system is cleaner. Write an adapter layer matching the `FMOD.Studio` API surface used. | ~9 .cs files | MEDIUM |
| **A* Pathfinding Project** | Use [A* Pathfinding Project Free](https://arongranberg.com/astar/freevspro) | The free version exists and is maintained by the same author. It supports Unity 5.x. Covers grid graphs, navmesh graphs, and basic pathfinding. The codebase uses `Pathfinding.RVO` (local avoidance) which is Pro-only -- stub or remove RVO calls. | ~20 .cs files | MEDIUM |
| **RootMotion FinalIK** | Stub initially, then implement basic IK using Unity's built-in `Animator.SetIKPosition`/`SetIKRotation` | FinalIK is used in ~39 files across IK, combat, animation, and entity systems. Unity 5.x has basic built-in IK via Mecanim. Start with stubs, then implement the specific solvers used (FullBodyBipedIK, LookAtIK, AimIK) with simplified versions. | ~39 .cs files | LOW |
| **Behave** (behavior trees) | Replace with [NPBehave](https://github.com/meniku/NPBehave) | Behave runtime is used in 4 files (23 occurrences). NPBehave is MIT-licensed, event-driven, and designed for Unity. The Behave API surface is small enough to write an adapter. | ~4 .cs files | MEDIUM |

#### Tier 3: Can Defer

| Original Plugin | Replacement Strategy | Why This Approach | Files Affected | Confidence |
|----------------|---------------------|-------------------|----------------|------------|
| **Steamworks.NET** | Stub all Steam calls | Not needed for local single-player. Stub `SteamManager`, `SteamWorkShop`, achievements, friends. Can re-add later if Steam distribution is desired. Steamworks.NET is actually free/MIT but requires Steam client. | ~28 .cs files | HIGH |
| **Camera Forge** | Assess if source is included or stub | Camera controller. If the source files are in `Assets/Camera Forge/`, may already be present. Otherwise write a basic third-person camera. | TBD | LOW |
| **Nova Environment** | Assess if source is included or stub | Weather/sky system. If source files are in `Assets/Nova Environment/`, may already be present. Otherwise use Unity skybox. | TBD | LOW |

### NGUI Strategy: Deep Dive

NGUI is the single largest dependency by file count (417 files). Three options exist:

| Option | Effort | Risk | Recommendation |
|--------|--------|------|----------------|
| **A) Stub DLL** -- empty MonoBehaviour subclasses matching NGUI API | 2-3 days | UI won't render but game compiles and runs headless | **Do this first** |
| **B) Port to uGUI** -- use [ngui-to-ugui](https://github.com/mamoniem/ngui-to-ugui) converter | Weeks to months | Converter handles basic cases but 417 files need manual review | Do this after game logic works |
| **C) Obtain NGUI source** -- NGUI 2.7.0 is free, NGUI 3.x is paid ($95) | Days | NGUI 2.7 API doesn't match 3.x; buying 3.x ties to proprietary asset | Only if budget allows and want fastest path |

**Recommendation: Option A first, then Option B.** Write a stub DLL that defines all NGUI types as empty MonoBehaviours. This unblocks compilation immediately. Then incrementally port to uGUI using the conversion tool as a starting point.

### Custom/In-House Systems (Already in Repo)

These systems appear to have their source code included in the repo and should NOT need replacement:

| System | Location | Status |
|--------|----------|--------|
| Voxelform2 (terrain) | `Assets/Terrain/Voxelform2/` | Source present |
| Block45 (voxel building) | `Assets/Block45/` | Source present |
| ScenarioRTL (missions) | `Assets/PeCustomGame/Scenario/ScenarioRTL/` | Source present |
| PatheaScript (modding) | `Assets/PatheaScript/Core/` | Source present |
| UnitySteer (steering) | `Assets/Scripts/AiScripts/UnitySteer/` | Source present |
| VehiclePhysics | `Assets/Scripts/VehiclePhysics/` | Source present |

### Data Layer

| Technology | Version | Purpose | Why | Confidence |
|------------|---------|---------|-----|------------|
| SQLite via Mono.Data.SqliteClient | (bundled with Mono) | Local data storage for world data, i18n, items | Already used by the original game. Mono.Data.SqliteClient ships with Unity's Mono runtime. No replacement needed. | HIGH |
| BinaryReader/BinaryWriter | .NET built-in | Save/load serialization | Used in 152 files. Built-in, no dependencies. | HIGH |
| XmlDocument/XmlSerializer | .NET built-in | Config data | Used in 57 files. Built-in. | HIGH |

### Development Tools

| Tool | Purpose | Why |
|------|---------|-----|
| Unity 5.2.4f1 Editor (Windows) | IDE, scene editing, building | Required exact version |
| Visual Studio 2015 or MonoDevelop | C# editing, debugging | VS 2015 was the recommended IDE for Unity 5.2. VS Code with OmniSharp also works. |
| Git | Version control | Already in use |
| Windows 10/11 VM | Running Unity editor | No Linux editor for 5.2.x |

## Alternatives Considered

| Category | Recommended | Alternative | Why Not |
|----------|-------------|-------------|---------|
| Engine version | Unity 5.2.4f1 | Upgrade to Unity 2019+ | Would require massive API migration (deprecated APIs, new rendering pipeline, new UI system). Defeats the purpose of building from existing source. |
| UI Framework | Stub NGUI then port to uGUI | Buy NGUI 3.x license | Adds proprietary dependency. uGUI is built into Unity 5.x and free. |
| UI Framework | Stub NGUI then port to uGUI | FairyGUI | Requires Unity 5.6+, incompatible with 5.2.4f1 |
| Networking | Stub uLink (single-player) | Mirror Networking | Mirror requires Unity 2019+. If multiplayer is revisited, will need a different approach. |
| Audio | Unity AudioSource wrapper | Keep FMOD | FMOD indie license is free but adds non-open-source dependency. Unity built-in audio is sufficient for single-player. |
| Pathfinding | A* Free version | Unity NavMesh | Codebase is deeply integrated with A* API. Swapping to NavMesh would touch 20+ files. Free version of same library minimizes changes. |
| Behavior Trees | NPBehave | UniBT | NPBehave has more community usage, simpler API, and is easier to adapt from Behave's runtime model. |
| IK | Stub then basic Unity IK | Buy FinalIK | Same reasoning as NGUI -- avoid proprietary dependencies. |
| Linux editor | Windows VM | Wine | Wine has known issues with Unity shader preview, asset import, and scene rendering. VM is more reliable. |

## Build & Run Configuration

### First-Time Setup (estimated)

```bash
# 1. Install Unity Hub on Arch Linux (for managing Unity installations)
yay -S unityhub

# 2. OR set up Windows VM for Unity editor
# Use QEMU/KVM with virtio drivers
# Install Unity 5.2.4f1 via Unity Hub or archive installer

# 3. Clone the project
git clone https://github.com/pathea-games/planetexplorers.git
# (or use this rebirth fork)

# 4. Create stub DLLs for missing plugins (see ARCHITECTURE.md)
# Place in Assets/Plugins/:
#   - NGUI.dll (stub)
#   - uLink.dll (stub)
#   - uLobby.dll (stub)
#   - FMODUnity.dll (stub)
#   - RootMotion.FinalIK.dll (stub)
#   - Behave.Runtime.dll (stub)

# 5. Open project in Unity 5.2.4f1
# Expect initial compilation errors from missing types
# Resolve iteratively by expanding stubs

# 6. Target platforms: Windows x64, Linux x64
# Build Settings > Platform > PC, Mac & Linux Standalone
```

### Stub DLL Approach

For each proprietary plugin, create a C# class library project targeting .NET 3.5 (Unity 5.2's runtime) that defines the public types used by the codebase. The stubs should:

1. Define all classes, enums, delegates, and interfaces referenced by the game code
2. Implement methods as no-ops (return default values)
3. Compile to a DLL and place in `Assets/Plugins/`
4. Mark with `[assembly: InternalsVisibleTo("Assembly-CSharp")]` if needed

This is a proven pattern for reviving Unity projects with missing dependencies -- it lets you achieve compilation without functional implementations, then replace stubs incrementally.

## Sources

- [Unity 5.2.4f1 Release Notes](https://unity.com/releases/editor/whats-new/5.2.4f1) -- confirmed version exists in archive
- [Unity Download Archive](https://unity.com/releases/editor/archive) -- official source for legacy editors
- [Planet Explorers GitHub](https://github.com/pathea-games/planetexplorers) -- official source release by Pathea Games
- [GitHub Issue #1: Unity Version](https://github.com/pathea-games/planetexplorers/issues/1) -- developer confirmed 5.2.4f1
- [GitHub Issue #3: Plugin Headers](https://github.com/pathea-games/planetexplorers/issues/3) -- community requesting plugin stubs
- [ngui-to-ugui converter](https://github.com/mamoniem/ngui-to-ugui) -- NGUI to Unity UI migration tool
- [A* Pathfinding Project Free vs Pro](https://arongranberg.com/astar/freevspro) -- free version comparison
- [NPBehave](https://github.com/meniku/NPBehave) -- open-source behavior tree library
- [NGUI 2.7.0 Free Edition](https://forum.unity.com/threads/ngui-free-edition.124032/) -- last free NGUI version (too old for this project)
- [Mirror Networking](https://mirror-networking.com/) -- requires Unity 2019+ (incompatible)
- [PEPatch](https://github.com/radistmorse/PEPatch) -- Harmony-based runtime patcher for compiled PE binaries (different approach than ours)
- [Unity3D-on-Wine](https://github.com/Unity3D-Wine-Support/Unity3D-on-Wine) -- archived Wine support scripts
- [FMOD Indie License](https://discussions.unity.com/t/fmod-studio-audio-tools-now-completely-free-for-indies/531459) -- free for <$200k revenue

---

*Stack research: 2026-03-28*
