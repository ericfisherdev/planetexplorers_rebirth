# Feature Landscape

**Domain:** Legacy Unity game revival (open-world survival/crafting)
**Researched:** 2026-03-28

## Table Stakes

Features that must work for the game to be considered "running." Without these, there is no game.

| Feature | Why Expected | Complexity | Notes |
|---------|--------------|------------|-------|
| Clean compilation | Nothing works without it | High | ~10 proprietary plugin stubs needed |
| Main menu loads | First thing users see | Medium | Depends on NGUI stub quality |
| World generation | Core gameplay foundation | Medium | Voxelform2 source is in repo |
| Player spawns in world | Without this, no game | Medium | Depends on entity system + terrain |
| Player movement & camera | Basic interaction | Low | Motor/camera code is in repo |
| Terrain rendering | Can't play what you can't see | Medium | Voxelform2 + shaders need to compile |
| Basic inventory | Core loop depends on items | Medium | UI-heavy (NGUI dependent) |
| Save/load | Players expect persistence | Medium | BinaryReader/Writer serialization already in codebase |
| Standalone build (Linux) | Project goal | Low | Unity 5.2 supports Linux build target |
| Standalone build (Windows) | Project goal | Low | Native Unity build target |

## Differentiators

Features that make Planet Explorers special vs. generic survival games. These justify the revival effort.

| Feature | Value Proposition | Complexity | Notes |
|---------|-------------------|------------|-------|
| Voxel terrain deformation | Mine, dig, reshape terrain -- not just build on flat ground | Already implemented | Voxelform2 + Transvoxel in repo |
| ISO creation system | Design custom items/vehicles/buildings from voxels | Already implemented | CreationSystem source is in repo |
| Vehicle building & physics | Build and drive custom vehicles | Already implemented | VehiclePhysics source in repo |
| NPC colony management | Recruit and manage NPC colonists | High | Depends on AI (A*), behavior trees (Behave), and UI (NGUI) |
| Custom scripting/modding | PatheaScript runtime for user mods | Already implemented | PatheaScript source in repo |
| Procedural world generation | Each playthrough has unique terrain | Already implemented | Seed-based generation in repo |

## Anti-Features

Features to explicitly NOT build in this revival effort.

| Anti-Feature | Why Avoid | What to Do Instead |
|--------------|-----------|-------------------|
| Multiplayer networking | Massive complexity (uLink replacement, server auth, sync), blocks single-player progress | Stub all networking code as no-ops. Defer to a future project phase. |
| Steam Workshop integration | Requires active Steam app ID, Steamworks SDK, and Steam client running | Stub Steamworks calls. Local mod loading is sufficient. |
| Story mode scripting | Complex quest chains depend on full UI, NPC, and dialog systems working first | Focus on sandbox mode. Story mode can come after core systems work. |
| Achievement system | Depends on Steam or custom backend | Remove/stub. No value for single-player MVP. |
| Lobby/matchmaking | Server infrastructure for multiplayer | Completely stub out. No servers needed. |
| Original audio fidelity | FMOD bank files may not be available | Use Unity AudioSource with placeholder sounds. Replace with real audio later. |

## Feature Dependencies

```
Clean Compilation
  -> Terrain Rendering -> World Generation -> Player Spawns
  -> Player Movement & Camera
  -> Entity System -> NPC AI (needs A* + Behave replacements)
  -> Inventory System (needs NGUI functional)
  -> Crafting System (needs Inventory + UI)
  -> Building System (needs Inventory + Block45)
  -> Combat System (needs Entity + IK stubs working)
  -> Save/Load (needs all above stable)
  -> Standalone Builds (needs everything above)
```

Key insight: Nearly everything depends on clean compilation, and UI-dependent features form the longest dependency chain due to NGUI's 417-file footprint.

## MVP Recommendation

Prioritize (in order):
1. Clean compilation with all stub DLLs
2. World generation and terrain rendering (the visual payoff that proves the project works)
3. Player movement and camera (interactive proof of life)
4. Basic inventory and crafting (core gameplay loop)
5. Voxel terrain deformation (the differentiator that makes PE special)

Defer:
- **NPC colony management:** Depends on AI, behavior trees, and full UI -- too many systems
- **Vehicle building:** Cool but not core to proving the game works
- **Custom scripting:** Modding support is a polish feature
- **Combat:** Can come after movement and inventory work

## Sources

- Codebase analysis (grep counts of plugin usage across .cs files)
- [Planet Explorers GitHub](https://github.com/pathea-games/planetexplorers)
- [Planet Explorers Steam page](https://steamcommunity.com/app/237870)

---

*Feature landscape: 2026-03-28*
