# Research Summary: Planet Explorers Rebirth

**Domain:** Legacy Unity game revival from open-sourced codebase
**Researched:** 2026-03-28
**Overall confidence:** MEDIUM

## Executive Summary

Planet Explorers was open-sourced by Pathea Games in October 2019 as a goodwill gesture to the community. The source release intentionally excludes all third-party plugins (NGUI, uLink, uLobby, FMOD, FinalIK, A* Pro, Behave, Steamworks.NET) due to licensing restrictions. This is the central challenge: the codebase compiles against ~10 proprietary DLLs that are not in the repo, making it impossible to build without replacements.

The project uses **Unity 5.2.4f1** (not 4.x as stated in PROJECT.md -- this is a critical correction confirmed by the developer and ProjectVersion.txt). Unity 5.2 has no native Linux editor, so development requires a Windows environment (VM, dual boot, or remote machine). The Unity editor itself is available from the official Unity archive.

The most impactful dependency is NGUI, referenced in 417 C# files. The recommended approach is to create stub DLLs that define all missing types as empty implementations, achieving compilation first, then incrementally replacing stubs with working code. This "compile first, run later" strategy is standard for reviving Unity projects with missing dependencies.

The single-player focus (deferring multiplayer) dramatically simplifies the effort by letting us stub uLink and uLobby as complete no-ops. The existing in-house systems (Voxelform2 terrain, Block45 building, PatheaScript, ScenarioRTL) appear to have source code in the repo and should work without external dependencies.

## Key Findings

**Stack:** Unity 5.2.4f1 (not 4.x), Windows editor only, stub DLLs for all missing proprietary plugins
**Architecture:** Stub-then-replace pattern -- get compilation clean, then incrementally add functionality
**Critical pitfall:** NGUI is in 417 files. Do NOT attempt a full UI rewrite before the game compiles. Stub it.

## Implications for Roadmap

Based on research, suggested phase structure:

1. **Environment Setup & Stub DLLs** - Get Unity 5.2.4f1 running, create all stub DLLs, achieve clean compilation
   - Addresses: "Project compiles against Unity 5.x without errors"
   - Avoids: Premature UI rewrite paralysis

2. **Core Systems Verification** - Verify terrain/voxel, entity, and game loop systems function with stubs
   - Addresses: "Player can start a new game and load into a world", terrain rendering, player movement
   - Avoids: Building on broken foundations

3. **Gameplay Systems Revival** - Replace stubs for pathfinding (A* Free), behavior trees (NPBehave), IK (basic Unity IK), audio (Unity AudioSource)
   - Addresses: Combat, NPC AI, crafting, building systems
   - Avoids: Trying to replace everything at once

4. **UI Implementation** - Port NGUI usage to Unity uGUI or implement functional NGUI stubs
   - Addresses: "Standalone build launches and reaches main menu", all UI-dependent features
   - Avoids: Blocking on UI before game logic works

5. **Platform Builds & Polish** - Linux and Windows standalone builds, save/load, final integration
   - Addresses: "Standalone Linux/Windows build launches", save/load
   - Avoids: Late-stage platform surprises

**Phase ordering rationale:**
- Phase 1 must come first because nothing else works without compilation
- Phase 2 before Phase 3 because we need to know which systems are actually broken vs. just need stubs
- Phase 3 before Phase 4 because game logic should be testable headless (without UI)
- Phase 4 is the largest effort (417 files) and benefits from understanding the game flow first
- Phase 5 last because platform-specific issues are easier to diagnose when the game works

**Research flags for phases:**
- Phase 1: Likely needs deeper research into exact NGUI/uLink API surfaces to stub
- Phase 3: A* Pathfinding Free version compatibility with Unity 5.2 needs verification
- Phase 4: NGUI-to-uGUI migration tooling maturity is uncertain for this scale

## Confidence Assessment

| Area | Confidence | Notes |
|------|------------|-------|
| Stack (Unity version) | HIGH | Confirmed by developer and ProjectVersion.txt |
| Stack (Linux editor) | HIGH | Well-documented that Unity 5.2 has no Linux editor |
| Features (what needs stubbing) | HIGH | Grep analysis of codebase is definitive |
| Architecture (stub approach) | MEDIUM | Standard pattern but untested on this specific codebase |
| Pitfalls (NGUI scale) | HIGH | 417 files is a hard count from the codebase |
| Plugin replacements | MEDIUM | A* Free and NPBehave are proven but compatibility with Unity 5.2 needs testing |

## Gaps to Address

- Exact API surface of NGUI types used (which UILabel properties, UIPanel methods, etc.) -- needed for stub DLL
- Whether Camera Forge and Nova Environment source code is actually in the repo
- OpenCL dependency in Voxelform2 -- may need GPU compute fallback
- Whether Unity 5.2.4f1 is still downloadable from the archive (page blocked during research)
- FMOD bank files / audio assets -- are original audio assets available or do we need placeholders?
- Exact .NET 3.5 compatibility requirements for stub DLLs

---

*Research summary: 2026-03-28*
