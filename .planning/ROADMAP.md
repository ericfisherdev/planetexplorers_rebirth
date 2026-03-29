# Roadmap: Planet Explorers Rebirth

## Overview

This roadmap takes the open-sourced Planet Explorers codebase from a non-compiling Unity 5.2.4f1 project with missing proprietary plugins to a playable single-player game on Linux and Windows running on modern Unity 6+. The approach is stub-then-replace: achieve clean compilation first with empty stubs for all proprietary dependencies, then incrementally bring systems online -- terrain, player, gameplay, UI -- before targeting platform builds. NGUI (417 files) and networking (uLink/uLobby) are the biggest blockers, addressed through stubbing early and replacing incrementally.

## Phases

**Phase Numbering:**
- Integer phases (1, 2, 3): Planned milestone work
- Decimal phases (2.1, 2.2): Urgent insertions (marked with INSERTED)

Decimal phases appear between their surrounding integers in numeric order.

- [x] **Phase 1: Unity 6+ Migration** - Migrate project structure and APIs from Unity 5.2.4f1 to Unity 6+
- [ ] **Phase 2: Proprietary Plugin Stubs** - Create stub DLLs/sources for all missing proprietary dependencies
- [ ] **Phase 3: Clean Compilation** - Achieve zero-error build with networking stubbed for single-player
- [ ] **Phase 4: Terrain & World Generation** - Get voxel terrain generating and rendering in-engine
- [ ] **Phase 5: Player & Game Loop Bootstrap** - Player spawns into world with movement and camera
- [ ] **Phase 6: Combat & AI Systems** - Melee/ranged combat and NPC/creature AI operational
- [ ] **Phase 7: Crafting & Inventory Systems** - Crafting recipes and inventory/equipment functional
- [ ] **Phase 8: UI Implementation** - Main menu, HUD, and gameplay UI screens via uGUI replacement
- [ ] **Phase 9: Platform Builds & Persistence** - Save/load works, standalone Linux and Windows builds ship

## Phase Details

### Phase 1: Unity 6+ Migration
**Goal**: The project opens and is structurally valid in the Unity 6+ editor
**Depends on**: Nothing (first phase)
**Requirements**: ENV-01
**Success Criteria** (what must be TRUE):
  1. Project opens in Unity 6+ editor without import-blocking errors (missing plugin errors are expected and acceptable)
  2. All C# scripts use modern Unity API calls (no deprecated 5.x API usage remaining)
  3. Project settings (physics, rendering, input) are configured for Unity 6+ defaults
**Plans**: 4 plans

Plans:
- [x] 01-01-PLAN.md -- Remove UnityScript files and clean ProjectSettings for Unity 6
- [x] 01-02-PLAN.md -- Batch-fix deprecated scene management APIs, WWW, and kernel32 P/Invoke
- [x] 01-03-PLAN.md -- Replace legacy particle system classes and FindObjectsOfType
- [x] 01-04-PLAN.md -- Open project in Unity 6 editor and verify migration

### Phase 2: Proprietary Plugin Stubs
**Goal**: Every proprietary type reference in the codebase resolves to a stub, eliminating missing-type errors
**Depends on**: Phase 1
**Requirements**: ENV-02, ENV-04
**Success Criteria** (what must be TRUE):
  1. NGUI stub library defines all types referenced across the 417 files that use NGUI (UILabel, UIPanel, UISprite, etc.)
  2. uLink and uLobby stubs define all referenced networking types as no-ops
  3. Stubs exist for FMOD, FinalIK, A* Pathfinding Pro, Behave, and Steamworks.NET
  4. Native LZ4 DLL replaced with a managed C# LZ4 implementation that passes basic compress/decompress
**Plans**: 4 plans

Plans:
- [ ] 02-01-PLAN.md -- NGUI stub library (UILabel, UISprite, UIPanel, UIButton, tweens, layout, events)
- [ ] 02-02-PLAN.md -- uLink, uLobby, and Steamworks.NET networking/platform stubs
- [ ] 02-03-PLAN.md -- FinalIK, A* Pathfinding, Behave, and FMOD gameplay system stubs
- [x] 02-04-PLAN.md -- Managed C# LZ4 implementation replacing native DLL

### Phase 3: Clean Compilation
**Goal**: The entire codebase compiles with zero errors and the project enters Play mode without crashes
**Depends on**: Phase 2
**Requirements**: ENV-03, CORE-04
**Success Criteria** (what must be TRUE):
  1. `dotnet build` or Unity compilation produces zero errors across all ~700+ scripts
  2. Networking code paths route through single-player stubs (no live network calls)
  3. Project enters Play mode in the Unity editor without null-reference crashes at startup
**Plans**: 3 plans

Plans:
- [ ] 03-01-PLAN.md -- Fix conditional compilation blocks and Unity Standard Assets image effect references
- [ ] 03-02-PLAN.md -- Wire networking for single-player operation
- [ ] 03-03-PLAN.md -- Iterative compilation error triage and Play mode verification

### Phase 4: Terrain & World Generation
**Goal**: The voxel world generates and renders visibly in the editor and at runtime
**Depends on**: Phase 3
**Requirements**: CORE-01
**Success Criteria** (what must be TRUE):
  1. Voxelform2 terrain generates a visible landscape when a new game starts
  2. Block45 voxel chunks render with correct geometry (no invisible or corrupted terrain)
  3. Player camera can look around and see generated terrain extending to the horizon
**Plans**: 3 plans

Plans:
- [ ] 04-01-PLAN.md -- Neutralize OpenCL dependency and verify managed LZ4 integration
- [ ] 04-02-PLAN.md -- Wire Voxelform2 procedural terrain generation and chunk rendering
- [ ] 04-03-PLAN.md -- Verify Block45 building system and visual terrain checkpoint

### Phase 5: Player & Game Loop Bootstrap
**Goal**: A player entity spawns into the generated world and can move around
**Depends on**: Phase 4
**Requirements**: CORE-02, CORE-03
**Success Criteria** (what must be TRUE):
  1. Starting a new game spawns a player entity at a valid world position
  2. WASD/arrow keys move the player through the terrain
  3. Mouse controls the camera (look, rotate) with expected FPS-style behavior
  4. Player interacts with terrain collision (stands on ground, cannot walk through solid voxels)
**Plans**: 3 plans

Plans:
- [ ] 05-01-PLAN.md -- Bootstrap player entity creation with simplified spawn pipeline
- [ ] 05-02-PLAN.md -- Implement WASD movement with CharacterController and terrain collision
- [ ] 05-03-PLAN.md -- Implement third-person camera with mouse look and player follow

### Phase 6: Combat & AI Systems
**Goal**: Player can fight enemies that behave with basic intelligence
**Depends on**: Phase 5
**Requirements**: GAME-01, GAME-04
**Success Criteria** (what must be TRUE):
  1. Player can perform melee attacks that deal damage to NPCs/creatures
  2. Player can perform ranged attacks (projectile weapons) that hit targets
  3. NPCs/creatures spawn, idle, patrol, and aggro toward the player
  4. Creatures use behavior tree AI to chase, attack, and retreat appropriately
**Plans**: 4 plans

Plans:
- [ ] 06-01-PLAN.md -- Wire SkillSystem damage pipeline and melee attack triggers
- [ ] 06-02-PLAN.md -- Make behavior tree runtime functional (BTLauncher, BTResolver, BehaveCmpt)
- [ ] 06-03-PLAN.md -- Wire ranged combat projectile system (bullets, arrows, trajectories)
- [ ] 06-04-PLAN.md -- Wire AI spawning, enemy detection, and full combat behavior loop

### Phase 7: Crafting & Inventory Systems
**Goal**: Player can collect items, manage inventory, equip gear, and craft new items
**Depends on**: Phase 5
**Requirements**: GAME-02, GAME-03
**Success Criteria** (what must be TRUE):
  1. Player can pick up items from the world and see them in inventory
  2. Player can equip weapons and armor that affect gameplay stats
  3. Player can open the replication crafting interface and craft items from recipes
  4. Crafted items appear in inventory and are usable
**Plans**: 3 plans

Plans:
- [ ] 07-01-PLAN.md -- Item data loading pipeline and inventory storage (ItemMgr, ItemPackage, PlayerPackageCmpt)
- [ ] 07-02-PLAN.md -- Equipment system with stat buff application (EquipmentCmpt, SkEntity integration)
- [ ] 07-03-PLAN.md -- Replication crafting system and world item pickup (Replicator, LootItemMgr)

### Phase 8: UI Implementation
**Goal**: All essential game UI screens work, replacing NGUI stubs with functional uGUI equivalents
**Depends on**: Phase 6, Phase 7
**Requirements**: UI-01, UI-02, UI-03
**Success Criteria** (what must be TRUE):
  1. Main menu displays with New Game, Load Game, and Quit options that function
  2. In-game HUD shows health, stamina, and minimap updating in real-time
  3. Inventory screen opens, displays items, and supports drag-and-drop equipping
  4. Crafting UI displays available recipes and allows crafting operations
**Plans**: 5 plans

Plans:
- [ ] 08-01-PLAN.md -- NGUI-to-uGUI bridge layer (functional UILabel, UISprite, UIButton, UISlider, etc.)
- [ ] 08-02-PLAN.md -- Main menu screen (New Game, Load Game, Quit)
- [ ] 08-03-PLAN.md -- In-game HUD (health, stamina, minimap, hotbar)
- [ ] 08-04-PLAN.md -- Inventory screen with drag-and-drop equipping
- [ ] 08-05-PLAN.md -- Crafting UI with recipe browsing and crafting operations

### Phase 9: Platform Builds & Persistence
**Goal**: The game saves/loads state and ships as standalone executables on Linux and Windows
**Depends on**: Phase 8
**Requirements**: PLAT-01, PLAT-02, PLAT-03
**Success Criteria** (what must be TRUE):
  1. Player can save current game state (world, inventory, position) and quit
  2. Player can load a saved game and resume with all state intact
  3. Linux standalone build launches, reaches main menu, and plays through the core loop
  4. Windows standalone build launches, reaches main menu, and plays through the core loop
**Plans**: 3 plans

Plans:
- [ ] 09-01-PLAN.md -- Audit and fix save/load persistence system for Unity 6 and cross-platform
- [ ] 09-02-PLAN.md -- Create modern build pipeline for Linux x64 and Windows x64
- [ ] 09-03-PLAN.md -- Verify builds and end-to-end save/load on both platforms

## Progress

**Execution Order:**
Phases execute in numeric order: 1 -> 2 -> 3 -> 4 -> 5 -> 6/7 (parallel eligible) -> 8 -> 9

| Phase | Plans Complete | Status | Completed |
|-------|----------------|--------|-----------|
| 1. Unity 6+ Migration | 0/4 | Planning complete | - |
| 2. Proprietary Plugin Stubs | 0/4 | Planning complete | - |
| 3. Clean Compilation | 0/3 | Planning complete | - |
| 4. Terrain & World Generation | 0/3 | Planning complete | - |
| 5. Player & Game Loop Bootstrap | 0/3 | Planning complete | - |
| 6. Combat & AI Systems | 0/4 | Planning complete | - |
| 7. Crafting & Inventory Systems | 0/3 | Planning complete | - |
| 8. UI Implementation | 0/5 | Planning complete | - |
| 9. Platform Builds & Persistence | 0/3 | Planning complete | - |
