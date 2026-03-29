# Requirements: Planet Explorers Rebirth

**Defined:** 2026-03-28
**Core Value:** The game launches, loads a world, and lets you play the core gameplay loop (explore, build, craft, fight) as a standalone executable on Linux and Windows.

## v1 Requirements

Requirements for initial release. Each maps to roadmap phases.

### Environment

- [x] **ENV-01**: Project migrated from Unity 5.2.4f1 to Unity 6+
- [ ] **ENV-02**: All proprietary plugins replaced with OSS alternatives or stubs (NGUI, uLink, uLobby, FMOD, FinalIK, A* Pro, Behave, Steamworks)
- [ ] **ENV-03**: Project compiles with zero errors on modern Unity
- [x] **ENV-04**: Native LZ4 DLL replaced with managed C# implementation

### Core Systems

- [ ] **CORE-01**: Voxel terrain generation and rendering operational (Voxelform2/Block45)
- [ ] **CORE-02**: Player and entity spawning with game loop bootstrap
- [ ] **CORE-03**: Player movement and camera controls functional
- [ ] **CORE-04**: Networking code stubbed for single-player operation

### Gameplay

- [ ] **GAME-01**: Melee and ranged combat system functional
- [ ] **GAME-02**: Recipe-based replication crafting system operational
- [ ] **GAME-03**: Inventory management and equipment system works
- [ ] **GAME-04**: NPC/creature AI with behavior trees and pathfinding

### UI

- [ ] **UI-01**: Main menu with game start flow
- [ ] **UI-02**: In-game HUD (health, stamina, minimap)
- [ ] **UI-03**: Inventory and crafting UI screens

### Platform

- [ ] **PLAT-01**: Standalone Linux build launches and is playable
- [ ] **PLAT-02**: Standalone Windows build launches and is playable
- [ ] **PLAT-03**: Save and load game state works

## v2 Requirements

Deferred to future release. Tracked but not in current roadmap.

### Advanced Gameplay

- **ADV-01**: Voxel Creation System (custom weapons, vehicles, armor, robots — 16+ categories)
- **ADV-02**: Story/Adventure mode scripting (PatheaScript/ScenarioRTL)
- **ADV-03**: Colony and settlement management
- **ADV-04**: Building system advanced features (complex structures, automation)

### Multiplayer

- **MP-01**: uLink networking replaced with modern networking solution
- **MP-02**: Multiplayer lobby and matchmaking
- **MP-03**: Networked gameplay synchronization
- **MP-04**: Dedicated server support

### Polish

- **POL-01**: Original audio/music integration or replacements
- **POL-02**: Performance optimization for large worlds
- **POL-03**: Mod support / workshop integration

## Out of Scope

Explicitly excluded. Documented to prevent scope creep.

| Feature | Reason |
|---------|--------|
| Multiplayer networking | High complexity, deferred until single-player is stable |
| Steam integration | Not needed for standalone builds |
| Mobile/console builds | Linux and Windows only for v1 |
| Voxel Creation System | Extremely complex (16+ categories), defer to v2 |
| Story mode scripting | Requires full PatheaScript pipeline, sandbox first |
| Original proprietary assets | May need placeholders if originals unavailable |

## Traceability

Which phases cover which requirements. Updated during roadmap creation.

| Requirement | Phase | Status |
|-------------|-------|--------|
| ENV-01 | Phase 1 | Complete |
| ENV-02 | Phase 2 | Pending |
| ENV-03 | Phase 3 | Pending |
| ENV-04 | Phase 2 | Complete |
| CORE-01 | Phase 4 | Pending |
| CORE-02 | Phase 5 | Pending |
| CORE-03 | Phase 5 | Pending |
| CORE-04 | Phase 3 | Pending |
| GAME-01 | Phase 6 | Pending |
| GAME-02 | Phase 7 | Pending |
| GAME-03 | Phase 7 | Pending |
| GAME-04 | Phase 6 | Pending |
| UI-01 | Phase 8 | Pending |
| UI-02 | Phase 8 | Pending |
| UI-03 | Phase 8 | Pending |
| PLAT-01 | Phase 9 | Pending |
| PLAT-02 | Phase 9 | Pending |
| PLAT-03 | Phase 9 | Pending |

**Coverage:**
- v1 requirements: 18 total
- Mapped to phases: 18
- Unmapped: 0

---
*Requirements defined: 2026-03-28*
*Last updated: 2026-03-28 after roadmap creation*
