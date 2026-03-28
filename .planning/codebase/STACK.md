# Technology Stack

**Analysis Date:** 2026-03-28

## Languages

**Primary:**
- C# (.NET/Mono) - All gameplay, networking, UI, and engine scripting in `Assets/Scripts/` and `Assets/PeCustomGame/`

**Secondary:**
- HLSL/ShaderLab - GPU shaders in `Assets/Resources/ShadersForDebug/`, `Assets/Terrain/Voxelform2/Voxelform/Source/Shaders/`, `Assets/PeAudio/Resources/`
- XML - Data configuration files; `ConfigFiles/MapConfig.xml`, `ConfigFiles/Position.xml`
- JSON - Network/server config; `ConfigFiles/ClientConfig.conf`

## Runtime

**Environment:**
- Unity Engine 5.2.4f1 (confirmed via `ProjectSettings/ProjectVersion.txt`)
- Mono runtime (standard Unity 5.x scripting backend)

**Package Manager:**
- Not applicable - Unity 5.x uses manual asset placement (no Package Manager in this era)
- Third-party plugins intentionally excluded from repo (per `README.md`)

## Frameworks

**Core:**
- Unity 5.2.4f1 - Game engine, rendering, physics, animation

**Networking:**
- uLink - RPC-based multiplayer networking; core network layer in `Assets/Scripts/GameNetwork/NetworkInterface.cs`, `NetworkManager.cs`
- uLobby - Lobby/matchmaking server layer; used in `Assets/Scripts/GameNetwork/LobbyInterface.cs`, `Assets/Scripts/LobbyShop/Shop.cs`

**UI:**
- NGUI (Next-Gen UI) - Unity UI framework; used extensively across 200+ files with `UILabel`, `UIPanel`, `UISprite`, `UIButton` components in `Assets/Scripts/Chenzhi/NewUI/`

**Audio:**
- FMOD Studio - Professional audio middleware; `Assets/PeAudio/PeFmodEditor/FMODAudioSourceRTE.cs` uses `FMOD.Studio` namespace

**Animation/IK:**
- RootMotion Final IK - Full-body inverse kinematics; `Assets/Scripts/Ik/IKHumanMgr.cs`, `Assets/Scripts/Assist/IKCombat.cs`
- Unity Mecanim - Standard Unity animator system

**AI/Pathfinding:**
- A* Pathfinding Project - Pathfinding and RVO steering; `Assets/Scripts/Assist/PEPathfinder.cs` (with `Pathfinding.RVO`)
- Behave - Behavior tree runtime; `Assets/Scripts/BehaviorTree/Scripts/BTLauncher.cs`, namespace `Behave.Runtime`
- UnitySteer - Steering behaviors for autonomous agents; `Assets/Scripts/AiScripts/UnitySteer/`

**Terrain/Voxels:**
- Voxelform2 - Marching cubes/transvoxel terrain system; `Assets/Terrain/Voxelform2/`
- Block45 - Custom voxel building/editing system; `Assets/Block45/`
- Transvoxel algorithm - Smooth voxel surface extraction; `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/TransVoxel/`
- OpenCLNet (Win/Mac variants) - GPU-accelerated marching cubes; `Assets/Terrain/Voxelform2/Voxelform/Source/Scripts/Voxel/MarchingCubes/Ocl/`

**Environment:**
- Nova Environment - Weather, sky, and environment effects; `Assets/Nova Environment/`

**Camera:**
- Camera Forge - Cinematic camera controller system; `Assets/Camera Forge/`

**Custom Scripting:**
- ScenarioRTL - In-house scenario/mission scripting runtime; `Assets/PeCustomGame/Scenario/ScenarioRTL/`
- PatheaScript - Custom script language runtime for modding; `Assets/PatheaScript/Core/`

**Post-Processing:**
- Unity Standard Assets Image Effects - Motion blur, grayscale; `Assets/Editor/ImageEffects/`

**Vehicle Physics:**
- Custom VehiclePhysics - Internal vehicle simulation using Unity Rigidbody; `Assets/Scripts/VehiclePhysics/`

**Build/Dev:**
- Unity Editor 5.2.4f1 - IDE and build tooling
- AssetStore Tools - Asset management editor extensions; `Assets/AssetStoreTools/`

## Key Dependencies

**Critical:**
- `uLink` - Core multiplayer transport; every networked object extends `uLink.MonoBehaviour`; found in `Assets/Scripts/GameNetwork/NetworkInterface.cs`
- `uLobby` - Server lobby and matchmaking; `Assets/Scripts/GameNetwork/LobbyInterface.cs`
- `Steamworks.NET` (v1.0.3) - Steam platform integration; `Assets/Scripts/Steamwork.NET/SteamManager.cs`
- `Mono.Data.SqliteClient` - SQLite database access via Mono; `Assets/PeMap/MapLabel.cs`, `Assets/PeWorld/NameGenerator.cs`
- `RootMotion.FinalIK` - Full-body IK; used in 5+ files in `Assets/Scripts/Assist/` and `Assets/Scripts/MountsMonster/`
- `FMOD` + `FMOD.Studio` - All non-trivial audio playback; `Assets/PeAudio/`
- `Pathfinding` + `Pathfinding.RVO` - All NPC pathfinding; `Assets/Scripts/Assist/PEPathfinder.cs`

**Infrastructure:**
- `Behave.Runtime` - NPC and entity behavior trees; `Assets/Scripts/BehaviorTree/`
- `ScenarioRTL` - Custom game scenario execution; `Assets/PeCustomGame/Scenario/`

## Configuration

**Environment:**
- `ConfigFiles/ClientConfig.conf` - JSON file with LobbyIP, LobbyPort, ProxyIP, ProxyPort
- `ConfigFiles/MapConfig.xml` - Map configuration
- `ConfigFiles/Position.xml` - World position configuration
- `ConfigFiles/version.txt` - Current version: `Ver 1.1.0`

**Build:**
- `ProjectSettings/ProjectSettings.asset` - Unity project settings (binary)
- `ProjectSettings/EditorBuildSettings.asset` - Scene build list
- `ProjectSettings/GraphicsSettings.asset` - Rendering settings
- `ProjectSettings/QualitySettings.asset` - Quality tiers

## Platform Requirements

**Development:**
- Unity 5.2.4f1 (exact version required; `ProjectSettings/ProjectVersion.txt`)
- Third-party plugins must be sourced separately (uLink, uLobby, FMOD, Steamworks.NET, FinalIK, NGUI, A* Pathfinding Project, Behave — all excluded from repo per `README.md`)

**Production:**
- Windows PC (primary platform; `ClientConfig.conf` references Windows lobby server IPs)
- Steam distribution via Steamworks
- Dedicated server model via uLobby (lobby at `119.28.73.40:12534`, proxy at `119.28.73.40:12535`)

---

*Stack analysis: 2026-03-28*
