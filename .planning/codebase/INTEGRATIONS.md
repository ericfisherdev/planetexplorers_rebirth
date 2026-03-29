# External Integrations

**Analysis Date:** 2026-03-28

## APIs & External Services

**Steam Platform:**
- Steamworks SDK (via Steamworks.NET v1.0.3) - Platform identity, authentication, friends, achievements, and Workshop (UGC)
  - SDK/Client: `Steamworks` namespace; `Assets/Scripts/Steamwork.NET/SteamManager.cs`
  - Auth: Steam App ID (embedded in Steamworks init, no env var — typical Unity/Steam pattern)
  - Features used:
    - Friends list: `Assets/Scripts/SteamFriends/SteamFriendPrcMgr.cs`, `SteamGetFriendsProcess.cs`
    - P2P networking: `Assets/Scripts/SteamFriends/P2PManager.cs`
    - Steam Workshop (UGC upload/download): `Assets/Scripts/GameNetwork/NetworkManager.cs` routes Workshop RPCs via `SteamWorkShop.*`; `Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/UIWorkShop/WorkShopMgr.cs` uses `PublishedFileId_t`, `Steamworks` directly
    - Achievements: `Assets/Scripts/SteamFriends/SteamAchievementsSystem.cs` (filename; file excluded from repo)
    - Steam Chat: `Assets/Scripts/SteamFriends/SteamChatProcess.cs`
    - Random ISO files via Workshop: `SteamRandomGetIsoProcess.cs`, `SteamRefreshVoteDetailProcess.cs`

**Dedicated Lobby/Proxy Server:**
- uLobby server at `119.28.73.40:12534` - Handles matchmaking, room listing, and lobby shop
  - Config: `ConfigFiles/ClientConfig.conf` (`LobbyIP`, `LobbyPort`)
  - Client implementation: `Assets/Scripts/GameNetwork/LobbyInterface.cs`
  - Shop integration: `Assets/Scripts/LobbyShop/Shop.cs` uses both `uLobby` and `uLink`

**Game Server (uLink):**
- uLink proxy server at `119.28.73.40:12535` - Handles in-game multiplayer networking
  - Config: `ConfigFiles/ClientConfig.conf` (`ProxyIP`, `ProxyPort`)
  - Implementation: `Assets/Scripts/GameNetwork/NetworkInterface.cs` — all game objects extend `uLink.MonoBehaviour`
  - Network view RPC system: `Assets/Scripts/GameNetwork/NetworkManager.cs`

## Data Storage

**Databases:**
- SQLite (via `Mono.Data.SqliteClient`) - Local embedded database for world data, name generation, item data
  - Files using it: `Assets/PeMap/MapLabel.cs`, `Assets/PeWorld/NameGenerator.cs`, `Assets/PeWorld/PeEntityCreator.cs`, `Assets/Scripts/PuJi/Town/Tool/VArtifactUtil.cs`, and others (10+ files total)
  - Database file: `i18n.db` (internationalization strings database at repo root)
  - No remote DB — all queries are local

**File Storage:**
- Local filesystem - Game save data, custom creations, and ISO files stored locally
  - Binary serialization used heavily: 152 files use `BinaryReader`/`BinaryWriter`/`BinaryFormatter`
  - XML data files: 57 files use `XmlDocument`/`XmlSerializer`/`XDocument`
  - Steam Workshop acts as remote UGC file storage for player-created content

**Caching:**
- None - No Redis, Memcached, or similar external cache

## Authentication & Identity

**Auth Provider:**
- Steam (via Steamworks.NET) - Primary player identity and authentication
  - Implementation: `Assets/Scripts/Steamwork.NET/SteamManager.cs` — initializes `SteamAPI`, validates ownership
  - All lobby/server sessions are Steam-authenticated via Steam IDs

**Player Prefs:**
- Unity `PlayerPrefs` - Minor local settings storage (2 files); not a primary auth mechanism

## Monitoring & Observability

**Error Tracking:**
- None detected — no Sentry, Raygun, or similar integration

**Logs:**
- Unity `Debug.Log` / `Debug.LogWarning` / `Debug.LogError` — standard Unity logging
- Custom `LogFilter.cs` in `Assets/Scripts/GameNetwork/LogFilter.cs` — filters network log output
- No structured logging or remote log aggregation

## CI/CD & Deployment

**Hosting:**
- Steam (Steamworks distribution) - Primary retail distribution platform
- Dedicated servers hosted on Tencent Cloud (IP `119.28.73.40` is Tencent Cloud Guangzhou region)

**CI Pipeline:**
- None detected — no `.github/`, `.gitlab-ci.yml`, or CI config files present

## Environment Configuration

**Required config files:**
- `ConfigFiles/ClientConfig.conf` - Lobby and proxy server IPs and ports
- `ConfigFiles/MapConfig.xml` - Map layout configuration
- `ConfigFiles/Position.xml` - Spawn/position data

**Third-party plugin secrets:**
- Steam App ID - Must be configured in `steam_appid.txt` (standard Steamworks.NET pattern; not in repo)
- No API keys stored in code — Steamworks.NET handles auth via the Steam client

## Webhooks & Callbacks

**Incoming:**
- None - No HTTP webhook endpoints

**Outgoing:**
- Steam Workshop callbacks - File upload/download completion events handled via `SteamWorkShop.RPC_S2C_*` methods registered in `Assets/Scripts/GameNetwork/NetworkManager.cs`
- uLink RPCs - Bidirectional game state sync between client and dedicated server via registered `EPacketType` handlers in `NetworkManager.cs`

## Steam Workshop (UGC) Detail

The Steam Workshop integration is the primary external content pipeline. Key flow:

1. Player uploads custom ISO/creation via `WorkShopMgr` (`Assets/Scripts/Chenzhi/NewUI/Scripts/PE_GameUI/UIWorkShop/WorkShopMgr.cs`)
2. Server receives upload request via `PT_Common_PreUpload` packet type
3. Files transferred in chunks via `PT_Common_UGCUpload` packets
4. `SteamWorkShop` class (`Assets/Scripts/SteamFriends/`) manages all `PublishedFileId_t` tracking
5. Random map seeding pulls Workshop ISO files for procedural world population via `SteamRandomGetIsoProcess.cs`

---

*Integration audit: 2026-03-28*
