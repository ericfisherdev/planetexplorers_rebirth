# planetexplorers

The Planet Explorers Source Project Terms of Use

As of Oct 29th 2019, Pathea Games LLC has made the source code of Planet Explorers public. By downloading any portion of this project you agree to the terms in this document.

All assets under this project remain property of Pathea Games. Direct resales of recompilation is prohibited. You are encouraged to use them in non-commercial or hobbyist projects. If you plan to use them in a commercial project, a written consent from Pathea Games is required. This applies to any modified and derivative versions of the assets.

As for the code portion of the project, Pathea Games LLC does not provide technical support for such and is not responsible for any damage caused by use of the code.

All third-party plugins are intentionally excluded to avoid copyright issues. Pathea Games will not provide the plugins.

---

# What is Planet Explorers Rebirth?

Planet Explorers Rebirth is a community effort to get the original Planet Explorers game compiling and running again as a standalone single-player experience on Linux and Windows. Starting from Pathea Games' open-sourced Unity 5.2.4f1 codebase, the project migrates the engine to modern Unity 6, replaces all proprietary third-party plugins with open-source alternatives, and works toward a fully playable core gameplay loop: explore, build, craft, and fight.

## Current Status

**Phases 1-3 complete.** The project opens in Unity 6 and compiles with stub replacements for all proprietary dependencies.

### Phase 1: Unity 6+ Migration (Complete)
- Migrated from Unity 5.2.4f1 to Unity 6000.3.12f1
- Converted 9 UnityScript files to C#
- Replaced deprecated APIs: `Application.LoadLevel` → `SceneManager.LoadScene`, `WWW` → `UnityWebRequest`, `kernel32.dll` P/Invoke → `System.Diagnostics.Stopwatch`
- Replaced legacy `ParticleEmitter`/`ParticleAnimator` with `ParticleSystem` API
- Replaced `FindObjectsOfType` → `FindObjectsByType`

### Phase 2: Proprietary Plugin Stubs (Complete)
- **NGUI** — 39 stub files covering 417+ referencing scripts (UILabel, UISprite, UIPanel, UIButton, tweens, layout, events)
- **uLink/uLobby** — Networking stubs with `isMine=true`, `isServer=true` for single-player
- **Steamworks.NET** — 14 stub files for achievements, workshop, friends
- **FinalIK** — IK solver inheritance chain stubs
- **A\* Pathfinding Pro** — AstarPath singleton, Seeker, path types, RVO stubs
- **Behave** — BehaveResult enum, IAgent interface, Tree class stubs
- **FMOD** — FMOD.Studio namespace stubs
- **LZ4** — Real managed C# implementation (not a stub) replacing native DLL

### Phase 3: Clean Compilation (Complete)
- Fixed `#if UNITY_5` conditional blocks across 12 files
- Created 16 image effect stubs for `UnityStandardAssets.ImageEffects`
- Wired networking for single-player operation (6 `IsMulti` guards)
- Iterative compilation triage: 9 additional stubs, 6 categories of deprecated API fixes

### Phases 4-9 (Planned)
| Phase | Goal |
|-------|------|
| 4 — Terrain & World Generation | Voxel terrain renders in-engine |
| 5 — Player & Game Loop Bootstrap | Player spawns with movement and camera |
| 6 — Combat & AI Systems | Melee/ranged combat and creature AI |
| 7 — Crafting & Inventory Systems | Item pickup, equipment, recipe crafting |
| 8 — UI Implementation | Main menu, HUD, inventory, crafting screens |
| 9 — Platform Builds & Persistence | Save/load, Linux and Windows standalone builds |

## Key Technical Decisions

- **Unity 6+** over staying on 5.2 — enables Linux editor and modern tooling
- **Stub-then-replace** — get compilation working with empty stubs, then incrementally add real functionality
- **Single-player first** — multiplayer networking deferred to v2
- **Adventure mode** (sandbox) before Story mode — self-contained gameplay loop without mission scripting dependencies

## Building

1. Install [Unity Hub](https://unity.com/download) and Unity 6000.3 LTS
2. Clone this repository
3. Open the project in Unity Hub
4. Accept API Updater prompts if shown
5. Expected: compilation errors from missing proprietary plugins are resolved by the stubs in `Assets/Stubs/`
