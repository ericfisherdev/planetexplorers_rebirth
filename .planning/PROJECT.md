# Planet Explorers Rebirth

## What This Is

A faithful recreation of the original Planet Explorers game, built from the officially open-sourced Unity 5.2.4f1 C# source code (released by Pathea Games in 2019). The goal is to get the full core gameplay loop — exploration, building, crafting, and combat — running as standalone builds on Linux and Windows, starting with single-player.

## Core Value

The game launches, loads a world, and lets you play the core gameplay loop (explore, build, craft, fight) as a standalone executable on Linux and Windows.

## Requirements

### Validated

(None yet — ship to validate)

### Active

- [ ] Project compiles against Unity 5.2.4f1 without errors
- [ ] Proprietary plugins (uLink, uLobby, NGUI) replaced with open-source alternatives
- [ ] Networking code stubbed/stripped for single-player operation
- [ ] Standalone Linux build launches and reaches main menu
- [ ] Standalone Windows build launches and reaches main menu
- [ ] Player can start a new game and load into a world
- [ ] Terrain/voxel world generation and rendering works
- [ ] Player movement and camera controls function
- [ ] Crafting system operational
- [ ] Building/creation system operational
- [ ] Combat system functional (melee and ranged)
- [ ] NPC interactions and basic AI work
- [ ] Inventory and equipment systems work
- [ ] Save/load game state works

### Out of Scope

- Multiplayer/networking — deferred until single-player is stable
- Story mode scripting — focus on sandbox gameplay first
- Mobile or console builds — Linux and Windows only
- Unity engine upgrade — staying on Unity 5.2.4f1 to minimize porting work
- Original proprietary assets (music, textures) — use placeholders if originals unavailable

## Context

- **Source origin:** Officially open-sourced C# by Pathea Games (October 2019), third-party plugins intentionally stripped
- **Engine:** Unity 5.2.4f1 (no native Linux editor — requires Windows VM or dual boot for development)
- **Codebase state:** ~700+ C# scripts, many referencing proprietary plugins (uLink, uLobby, NGUI, various Unity extensions) that need replacement
- **Key systems:** Voxel terrain (VFVoxel), creation system (CreationSystem), networking (uLink/uLobby), UI (NGUI), AI (Pathea AI), inventory, crafting, combat
- **Platform:** Arch Linux development environment, targeting Linux and Windows builds
- **Codebase map:** See `.planning/codebase/` for detailed analysis

## Constraints

- **Engine version**: Unity 5.2.4f1 — must track down and install this legacy version
- **Proprietary plugins**: uLink, uLobby, NGUI, and other paid plugins must be replaced with open-source alternatives
- **No original assets**: May need placeholder art/audio if original assets aren't available
- **Compilation first**: Must achieve clean compilation before any gameplay work

## Key Decisions

| Decision | Rationale | Outcome |
|----------|-----------|---------|
| Stay on Unity 5.2.4f1 | Minimize code changes vs upgrading to modern Unity | — Pending |
| Replace proprietary plugins with OSS | Original plugins are not freely available | — Pending |
| Single-player first | Reduce complexity — networking adds massive scope | — Pending |
| Target Linux + Windows | Developer is on Linux, Windows has broader audience | — Pending |

## Evolution

This document evolves at phase transitions and milestone boundaries.

**After each phase transition** (via `/gsd:transition`):
1. Requirements invalidated? → Move to Out of Scope with reason
2. Requirements validated? → Move to Validated with phase reference
3. New requirements emerged? → Add to Active
4. Decisions to log? → Add to Key Decisions
5. "What This Is" still accurate? → Update if drifted

**After each milestone** (via `/gsd:complete-milestone`):
1. Full review of all sections
2. Core Value check — still the right priority?
3. Audit Out of Scope — reasons still valid?
4. Update Context with current state

---
*Last updated: 2026-03-28 after initialization*
