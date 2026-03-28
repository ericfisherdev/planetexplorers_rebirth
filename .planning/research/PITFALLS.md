# Domain Pitfalls

**Domain:** Legacy Unity game revival from open-sourced codebase
**Researched:** 2026-03-28

## Critical Pitfalls

Mistakes that cause rewrites or major issues.

### Pitfall 1: NGUI Rewrite Before Compilation

**What goes wrong:** Attempting to replace all 417 NGUI-dependent files with uGUI before achieving a clean build.
**Why it happens:** It feels productive to "fix" the dependency properly. The scale of NGUI usage (417 files with UILabel, UIPanel, UISprite, UIButton, etc.) makes it seem like the only way forward.
**Consequences:** Months of work modifying code you don't understand, in a project that cannot compile and therefore cannot be tested. Bugs accumulate invisibly.
**Prevention:** Create a stub DLL first. Define all NGUI types as empty MonoBehaviours in the global namespace (NGUI does NOT use a namespace). Compile, then replace incrementally.
**Detection:** If you're modifying game .cs files before the project compiles, you've fallen into this trap.

### Pitfall 2: Wrong Unity Version

**What goes wrong:** Opening the project in any Unity version other than 5.2.4f1.
**Why it happens:** PROJECT.md says "Unity 4.x" (incorrect). Unity Hub defaults to installing recent versions. Developers may think "newer is better."
**Consequences:** Asset serialization format mismatch corrupts .asset files, .prefab files, and scenes. Shader compilation fails. API differences cause hundreds of new errors unrelated to the actual project issues. Once assets are re-serialized by the wrong version, reverting is difficult.
**Prevention:** Verify `ProjectSettings/ProjectVersion.txt` says `m_EditorVersion: 5.2.4f1`. Only open in that exact version.
**Detection:** If you see "This project was last opened with a different version of Unity" dialog, STOP.

### Pitfall 3: Attempting Unity Version Upgrade

**What goes wrong:** Upgrading to Unity 2019+ or Unity 6 for Linux editor support or modern tooling.
**Why it happens:** Unity 5.2 has no Linux editor. Modern Unity has better debugging, profiling, and C# features.
**Consequences:** Unity 5.2 to 2019+ migration involves: hundreds of deprecated API calls, new rendering pipeline (LWRP/URP), different shader model, .NET 4.x runtime (vs. 3.5), new UI system semantics, SceneManager API changes, and serialization format changes. This is a larger project than the game revival itself.
**Prevention:** Accept Windows-only editor. Use a VM, dual boot, or remote machine.
**Detection:** If you're reading Unity migration guides, reconsider.

### Pitfall 4: NGUI Namespace Assumption

**What goes wrong:** Creating NGUI stubs inside a `namespace NGUI { }` block.
**Why it happens:** Most C# libraries use namespaces. NGUI does not -- its types (UILabel, UIPanel, UISprite, etc.) live in the global namespace.
**Consequences:** Every one of the 417 files will fail with CS0246 "type or namespace not found" because they reference `UILabel` not `NGUI.UILabel`.
**Prevention:** Verify by checking existing code. Game files use `UILabel` directly, never `NGUI.UILabel` or `using NGUI;`.
**Detection:** Mass CS0246 errors for types you thought you already defined.

### Pitfall 5: Ignoring uLink.MonoBehaviour Base Class

**What goes wrong:** Stubbing uLink types but missing that many game classes extend `uLink.MonoBehaviour` instead of `UnityEngine.MonoBehaviour`.
**Why it happens:** `using uLink;` only appears in ~9 files, making uLink seem low-impact. But classes that extend `uLink.MonoBehaviour` may not have an explicit `using` statement -- they could be in files that reference the type via fully-qualified name or inheritance.
**Consequences:** Classes that inherit from `uLink.MonoBehaviour` fail to compile. Since this is a base class for networked game objects, it could affect dozens of entity classes.
**Prevention:** The uLink stub must include a `MonoBehaviour` class that extends `UnityEngine.MonoBehaviour` and adds stubs for `networkView`, `isMine`, `isServer`, etc. Single-player stubs should return `isMine = true`, `isServer = true`.
**Detection:** CS0246 errors for `uLink.MonoBehaviour` in entity/network classes.

## Moderate Pitfalls

### Pitfall 1: OpenCL GPU Compute Dependency

**What goes wrong:** Voxelform2's marching cubes uses OpenCLNet for GPU-accelerated terrain generation. This may not be available or compatible on all systems.
**Prevention:** The codebase likely has a CPU fallback path (most Unity voxel systems do). Verify by checking `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/MarchingCubes/` for non-OCL code paths. If no fallback exists, implement one.

### Pitfall 2: Missing Asset Files

**What goes wrong:** Source code compiles but game crashes at runtime because textures, models, animations, audio, and prefabs are missing.
**Why it happens:** The open-source release includes source code but may not include all binary assets (textures, meshes, audio banks, etc.).
**Prevention:** Inventory the `Assets/Resources/` and `Assets/StreamingAssets/` directories. Cross-reference with code that loads resources via `Resources.Load<>()`. Missing assets cause runtime NullReferenceExceptions, not compile errors.

### Pitfall 3: A* Pathfinding Free vs Pro API Differences

**What goes wrong:** Game code uses Pro-only features like `RVOController` (local avoidance) that don't exist in the free version.
**Prevention:** The codebase uses `Pathfinding.RVO` in several files. The free version of A* Pathfinding Project does NOT include RVO. Options: (a) stub RVO types as no-ops, (b) write simplified local avoidance, or (c) use an older Pro version if legally obtainable. Start with (a).

### Pitfall 4: .NET 3.5 Runtime Constraints

**What goes wrong:** Stub DLLs compiled against .NET 4.x or .NET Standard fail to load in Unity 5.2.
**Why it happens:** Modern C# tooling defaults to .NET 6+ or .NET Standard 2.0. Unity 5.2 uses Mono with .NET 3.5 class library profile.
**Prevention:** All stub DLLs must target .NET Framework 3.5. Use `<TargetFramework>net35</TargetFramework>` in .csproj files. Reference UnityEngine.dll from the Unity 5.2.4f1 installation (typically `Unity/Editor/Data/Managed/UnityEngine.dll`).

### Pitfall 5: Scene File References to Missing Scripts

**What goes wrong:** Opening scenes in Unity shows "Missing Script" warnings on GameObjects that referenced proprietary plugin MonoBehaviours (NGUI panels, uLink network views, etc.).
**Why it happens:** Unity scene files reference scripts by GUID. If the stub DLL has different GUIDs than the original plugin, Unity can't find the scripts.
**Prevention:** When creating stub DLLs, Unity will assign new GUIDs. You may need to use a GUID remapping tool or manually update .meta files. Alternatively, if stubs are compiled as part of the project (source files in Assets/ rather than pre-compiled DLLs), Unity handles GUID assignment naturally.
**Detection:** Yellow "Missing Script" warnings in the Unity console when opening scenes.

### Pitfall 6: Binary Serialization Version Mismatch

**What goes wrong:** Save files and binary data formats assume specific class layouts that may change when stubs are introduced.
**Why it happens:** `BinaryFormatter` serialization (used in 152 files) is sensitive to assembly names, namespaces, and type definitions. If a stub type's assembly name differs from the original, deserialization fails.
**Prevention:** For save/load compatibility with original PE save files, stub DLLs must match original assembly names. If starting fresh (no original save file compatibility needed), this is not an issue.

## Minor Pitfalls

### Pitfall 1: Editor Script Failures

**What goes wrong:** Custom editor scripts in `Assets/Editor/` reference proprietary types and fail first during compilation.
**Prevention:** Editor scripts can be temporarily disabled or wrapped in `#if UNITY_EDITOR` with additional stubs. They're not needed for builds.

### Pitfall 2: Shader Compilation Warnings

**What goes wrong:** Unity 5.2 shaders may produce warnings on modern GPU drivers but still work.
**Prevention:** Ignore shader warnings initially. Only fix actual shader compilation errors. ShaderLab syntax changed between Unity versions but 5.2 shaders should work as-is within 5.2.

### Pitfall 3: SQLite Native Library Platform Issues

**What goes wrong:** `Mono.Data.SqliteClient` depends on a native SQLite library (`sqlite3.dll` on Windows, `libsqlite3.so` on Linux) that may not be bundled.
**Prevention:** Ensure the native SQLite binary is in the build output directory. Unity 5.2's Mono should include this, but verify for Linux builds.

## Phase-Specific Warnings

| Phase Topic | Likely Pitfall | Mitigation |
|-------------|---------------|------------|
| Environment Setup | Wrong Unity version, no Linux editor | Verify ProjectVersion.txt, use Windows VM |
| Stub DLL Creation | NGUI namespace error, .NET version mismatch | Global namespace, target .NET 3.5 |
| Core Systems | OpenCL missing, missing assets | Check for CPU fallback, inventory assets |
| Gameplay Revival | A* Pro features missing in Free | Stub RVO as no-op |
| UI Implementation | 417 files is overwhelming | Port one screen at a time, start with main menu |
| Platform Builds | SQLite native lib missing on Linux | Bundle libsqlite3.so |

## Sources

- Codebase analysis (grep for dependency usage patterns)
- [GitHub Issue #3: Plugin Headers](https://github.com/pathea-games/planetexplorers/issues/3) -- community confirms stubs are needed
- [GitHub Issue #1: Unity Version](https://github.com/pathea-games/planetexplorers/issues/1) -- developer confirms 5.2.4f1
- [A* Pathfinding Free vs Pro](https://arongranberg.com/astar/freevspro) -- feature comparison
- [Unity 5.2 Documentation](https://docs.unity3d.com/520/Documentation/Manual/InstallingUnity.html)

---

*Pitfalls research: 2026-03-28*
