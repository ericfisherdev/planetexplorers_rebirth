# Codebase Concerns

**Analysis Date:** 2026-03-28

---

## Security Issues

**Hardcoded SMTP Credentials in Source:**
- Issue: Live email credentials are hardcoded in source-controlled code
- Files: `Assets/Scripts/System/BugReporter.cs` (line 83)
- Impact: Credential exposure; the `pe.bugreport@pathea.net` mailbox password is committed in plaintext
- Fix approach: Move credentials to a configuration file excluded from version control, or use environment variables / Unity ScriptableObject assets not tracked by git

**SSL Certificate Validation Disabled Globally:**
- Issue: `ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }` bypasses all TLS certificate verification
- Files: `Assets/Scripts/System/BugReporter.cs` (line ~85)
- Impact: All HTTPS connections in the process become vulnerable to MITM attacks for the duration of the session
- Fix approach: Remove the global override; implement per-connection certificate pinning or use proper CA chain validation

---

## Tech Debt

**Incomplete Voxel Save/Load Implementation:**
- Issue: Core chunk data save/load functionality is stubbed with TODO comments and never implemented
- Files: `Assets/Block45/Block45DataStructure/B45ChunkData.cs` (lines 12-13), `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/Block45/Block45Man.cs` (lines 188, 193, 425)
- Impact: Block45 building chunks may not persist correctly; data loss risk for player constructions
- Fix approach: Implement the flagged save/load paths; B45ChunkData has existing IO infrastructure to model from

**FIXME Temporary Bit-Shift Hack in Voxel Math:**
- Issue: Two locations use `>>1` instead of the correct `/VFVoxel.c_SizeVT` division, marked FIXME
- Files: `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/VFVoxelChunkDataHelper.cs` (lines 237, 271)
- Impact: Voxel position calculations are only correct when `c_SizeVT == 2`; any change to voxel size constant will silently produce wrong results
- Fix approach: Replace both instances with the proper divisor constant

**OCL Marching Cube Bug-Fix Workaround:**
- Issue: GPU marching cube triangle generation uses a `while` loop to cap a value at 65535, commented `// TODO :bug fix`
- Files: `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/MarchingCubes/Ocl/oclMarchingCube.cs` (line 433)
- Impact: Loop executes an unknown number of iterations at runtime; could stall or produce incorrect geometry for large active voxel counts
- Fix approach: Identify the root cause of why `activeVoxels` can exceed 65535 and fix at the source

**AssetsLoader Cleanup Coroutine is Empty:**
- Issue: `AssetsPool.Cleanup()` coroutine (called in comments and `AssetsLoader.Start`) only loops forever doing nothing; the `// TODO : clean up` comment inside indicates the cleanup logic was never implemented
- Files: `Assets/Scripts/AssetsLoader/AssetsLoader.cs` (lines 215-222)
- Impact: Asset pool memory is never reclaimed during long play sessions
- Fix approach: Implement eviction of unused pooled assets older than a threshold

**VFDataReader Missing Memory-Mapped File Optimization:**
- Issue: Terrain data reads use standard `FileStream` instead of memory-mapped files; two TODOs note this and the anti-pattern of file switching
- Files: `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/VFDataReader.cs` (lines 9-10)
- Impact: Repeated seek-heavy reads under world streaming cause unnecessary I/O overhead
- Fix approach: Replace with `MemoryMappedFile` for the read-only voxel data files

**Scenario Subsystem Import/Export Not Implemented:**
- Issue: Custom game scenario save/load calls are stubbed with TODO comments
- Files: `Assets/PeCustomGame/Scenario/PEScenario/PeScenario.cs` (lines 234, 251)
- Impact: Custom game scenario state is not persisted; player scenario progress may reset
- Fix approach: Implement Import/Export for each registered subsystem in PeScenario

**Multiplayer Server-Side Event Listeners Not Wired:**
- Issue: Multiple network event listeners note their server-side counterparts are not implemented
- Files: `Assets/PeCustomGame/Scenario/PEScenario/Statements/EventListeners/DamageListener.cs`, `DeathListener.cs`, `PutOutItemListener.cs`, `UseItemListener.cs`
- Impact: Custom game scenario triggers for damage/death/item events only fire for the local client, not for server-authoritative multiplayer
- Fix approach: Implement RPC-based server event dispatch for each listener type

---

## Known Bugs

**Known Game-Breaking Bug in Multiplayer Mode Entry:**
- Symptoms: Garbled comment text (mojibake encoding) next to `BUG` marker indicates a known crash or corruption when exiting multiplayer mode under certain conditions
- Files: `Assets/PeLauncher/PeGameMgr.cs` (lines 109-115)
- Trigger: Appears related to mode switching (single player <-> multiplayer) with commented-out `yirdName` assignment
- Workaround: Code path is partially disabled via comments; full fix requires uncommenting and verifying the yird name flow

---

## Performance Bottlenecks

**`GameObject.Find` Called During Runtime Update Loops:**
- Problem: Multiple scripts call `GameObject.Find` from within `Update()` and related per-frame methods
- Files: `Assets/Scripts/Mission/StroyManager.cs` (14 calls), `Assets/Scripts/Mission/PlayerMission.cs` (7 calls), `Assets/Scripts/Mission/MissionScript/MissionManager.cs` (2 calls), `Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/GameMain/UIMinMapCtrl.cs`, `Assets/Scripts/MineralScanner/MSScan.cs`, `Assets/Scripts/TG42_Script/AutoRunCamera.cs`
- Cause: `GameObject.Find` performs a linear scan of the entire scene hierarchy every call
- Improvement path: Cache references in `Awake`/`Start` using serialized fields or `[SerializeField]` inspector references

**`Camera.main` Accessed Repeatedly Per Frame:**
- Problem: 48 scripts access `Camera.main`, which performs a `FindObjectOfType` call internally on each access prior to Unity 2020
- Files: Top offenders: `Assets/Scripts/TG42_Script/FreeCamera.cs`, `Assets/PeLauncher/PeGameLoader.cs`, `Assets/PeCamera/Scripts/PeCamera.cs`, `Assets/PeAudio/PeFmodEditor/PeFmodEditor.cs` (5 calls each)
- Cause: Unity 5.x (confirmed `m_EditorVersion: 5.2.4f1`) caches `Camera.main` only from Unity 2020+; in Unity 5 each access is an object search
- Improvement path: Cache `Camera.main` into a local variable in `Awake`/`Start`

**Excessive `GetComponent` Calls at Runtime:**
- Problem: Several scripts call `GetComponent` dozens of times without caching
- Files: `Assets/Scripts/Mission/StroyManager.cs` (44 calls), `Assets/Scripts/Chenzhi/NewUI/Scripts/GameUISystem/GameUI.cs` (38 calls), `Assets/Scripts/PeEntity/PeEntityPartial.cs` (30 calls)
- Cause: `GetComponent` iterates the component list on each call; calling it in Update or hot paths is expensive
- Improvement path: Cache component references as private fields, initialized in `Awake`

**uLink RPC Calls Use String-Based Method Lookup:**
- Problem: All 100+ files using uLink dispatch RPCs by method name string (e.g., `p2p.RPC(func, peer, args)`)
- Files: `Assets/Scripts/P2P/P2PManager.cs`, `Assets/Scripts/GameNetwork/Player/PlayerNetwork.cs`, and ~98 other files
- Cause: uLink's string RPC dispatch uses reflection at runtime for every network message
- Improvement path: Replace with typed RPC handlers or migrate to a more modern networking solution

**VFDataReaderClone Acknowledged as Unoptimized:**
- Problem: Clone reader for voxel data has an explicit `// TODO: optimization` comment
- Files: `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/VFDataReaderClone.cs` (line 114)
- Cause: Appears to perform redundant copies of decompressed chunk data
- Improvement path: Profile during world streaming; likely candidate for pooled buffer reuse

---

## Fragile Areas

**StroyManager.cs — Monolithic Mission God Object:**
- Files: `Assets/Scripts/Mission/StroyManager.cs`
- Why fragile: 6,151 lines, single class, 14 `GameObject.Find` calls, 44 `GetComponent` calls, 11 localization calls, implements story state, shop data, passenger routing, and NPC camp patrol — violates SRP severely
- Safe modification: Any change requires tracing through the full file; always run a full play session after edits
- Test coverage: No automated tests detected

**PlayerNetwork.cs — Partial Class Network Hub:**
- Files: `Assets/Scripts/GameNetwork/Player/PlayerNetwork.cs` (2,545 lines across partial classes)
- Why fragile: Handles player sync, RPC dispatch, approval flows, damage events, animation sync, team management, and scenario events in a single class hierarchy; several TODO stubs for unimplemented approval/denial logic
- Safe modification: Changes to RPC handlers can silently break multiplayer sync; test with at least 2 clients
- Test coverage: None detected

**Voxel Terrain Terrain LOD System — Threading Without Full Guards:**
- Files: `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/LODDataUpdate.cs`, `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/VFVoxelChunkDataHelper.cs`
- Why fragile: LOD update runs on background threads (`#if !DEBUG_LDUSingleThread`); the FIXME bit-shift workaround operates on shared chunk data without clear lock boundaries
- Safe modification: Do not change chunk data access patterns without reviewing all callers in `VFVoxelChunkDataHelper.cs`
- Test coverage: Debug single-thread mode exists (`#define DEBUG_LDUSingleThread`) but is disabled in production builds

**HighStopwatch — Windows-Only Native Interop:**
- Files: `Assets/Scripts/ZhouXun/Misc/HighStopwatch.cs`
- Why fragile: Uses `kernel32.dll` P/Invoke for `QueryPerformanceCounter`; falls back to `Environment.TickCount` on non-Windows but the fallback has lower resolution and can wrap at ~25 days
- Safe modification: Do not use in cross-platform timing-critical paths; prefer `System.Diagnostics.Stopwatch`
- Test coverage: None

**LocalDatabase — Temp File Write Pattern:**
- Files: `Assets/Scripts/Database/LocalDatabase.cs`
- Why fragile: Writes the entire SQLite database to a temp file on every load; second `catch` block attempts a random filename fallback with no cleanup of the orphaned temp file on crash
- Safe modification: Verify temp file cleanup in `OnDestroy`; the orphaned file accumulation could fill disk on repeated crashes
- Test coverage: None

---

## Scaling Limits

**Block45 Voxel Chunk Count Hard Cap at 65535:**
- Current capacity: GPU marching cube pass supports at most 65535 active voxels per dispatch
- Limit: `oclMarchingCube.cs` line 433 clamps beyond this via a loop workaround
- Scaling path: Rewrite as multi-pass dispatch splitting voxel sets above 65535

---

## Dependencies at Risk

**Unity 5.2.4f1 — End of Life Engine:**
- Risk: Unity 5.2.4f1 (confirmed via `ProjectSettings/ProjectVersion.txt`) has been end-of-life since 2016; no security patches, no API compatibility guarantees with modern platform SDKs
- Impact: Cannot target modern iOS/Android API levels; SteamWorks.NET and uLink versions pinned to Unity 5-era APIs; modern C# language features unavailable
- Migration plan: Upgrade path is non-trivial; minimum viable target would be Unity 2019 LTS; all NGUI (258 files) would need migration to Unity UI or uGUI

**uLink Networking — Abandoned Library:**
- Risk: uLink (Unify Network) is an abandoned third-party networking library; no updates, no Unity version support beyond 5.x
- Impact: All 100 network-dependent scripts are tied to uLink's API; migrating is a full networking rewrite
- Migration plan: Mirror, Fish-Net, or Unity Netcode for GameObjects would be the modern replacement

**NGUI — Superseded by Unity uGUI:**
- Risk: NGUI is used in 258 files; it predates Unity's built-in UI system and requires the NGUI asset package
- Impact: Mixed UI system (NGUI + some uGUI) adds complexity; NGUI is no longer actively maintained
- Migration plan: Migrate UI panels incrementally to Unity UI; highest-impact files are under `Assets/Scripts/Chenzhi/NewUI/`

**LZ4 Native DLL — Platform-Specific Binary:**
- Risk: LZ4 compression is loaded via `DllImport("lz4_dll")`; the DLL binary must be present and compatible with the target platform
- Impact: `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/VFDataReader.cs` and `VFDataReaderClone.cs` will throw `DllNotFoundException` if the binary is missing or wrong architecture
- Migration plan: Replace with managed LZ4 NuGet package (`K4os.Compression.LZ4`) which requires no native binary

---

## Structural Concerns

**Developer-Named Directories in Production Code:**
- Issue: Over 1,200 source files live in directories named after individual developers: `Scripts/Chenzhi/` (331 files), `Scripts/ZhouXun/` (562 files), `Scripts/WuYiqiu/` (226 files), `Scripts/Jaluca/` (64 files), `Scripts/Lilj/` (21 files), `Scripts/LiuZhichen/` (5 files), `Scripts/ZhangShunBo/` (23 files), `Scripts/TG42_Script/` (9 files)
- Impact: Code organization reflects team ownership rather than domain; makes it difficult to reason about what feature area a file belongs to; cross-feature dependencies are untracked
- Fix approach: Reorganize by domain (UI/, Network/, AI/, Terrain/, etc.) in a major refactoring pass

**GameUITest Code in Production Build:**
- Issue: `Assets/Scripts/GameUITest/` contains 58 C# files in a directory named "Test" that appears to be prototype/scratch UI work, not automated tests
- Impact: Prototype code ships in production builds; unclear which systems this code interacts with
- Fix approach: Audit each file; move genuinely unused prototypes to an Editor-only assembly or delete

**Obsolete Code Not Removed:**
- Issue: `Assets/Scripts/PuJi/Town/~Obsolete/` contains 8 active `.cs` files (prefixed `~`) that Unity still compiles due to lacking `.asmdef` exclusions
- Impact: Dead code increases compile times and creates confusing name collisions
- Fix approach: Delete the `~Obsolete` directory entirely or add an assembly definition that excludes it

**488 Files Contain Hardcoded Chinese-Language Strings:**
- Issue: A majority of comment text and some user-facing strings are written in Chinese throughout the codebase
- Impact: Non-Chinese-speaking contributors cannot understand inline documentation; localization audit is difficult when strings are embedded in code
- Fix approach: Move user-facing Chinese strings to localization tables; translate or transliterate critical developer comments

---

## Test Coverage Gaps

**No Automated Tests Detected:**
- What's not tested: The entire codebase of 2,888 `.cs` files has no test files (`*.test.cs`, `*.spec.cs`, or an `Assets/Tests/` directory detected)
- Files: All of `Assets/Scripts/`, `Assets/PeCustomGame/`, `Assets/Terrain/`, `Assets/Block45/`
- Risk: Any refactoring or bug fix has no regression safety net; the voxel math FIXME hacks and stub save/load code could be broken without detection
- Priority: High — especially for `VFVoxelChunkDataHelper.cs`, `B45ChunkData.cs`, and `PlayerNetwork.cs`

---

*Concerns audit: 2026-03-28*
