// Stub: uLobby.ServerInfo - server information from lobby

namespace uLobby
{
    public class ServerInfo
    {
        public string name { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public int playerCount { get; set; }
        public int maxPlayers { get; set; }
        public string gameMode { get; set; }
        public string mapName { get; set; }

        public ServerInfo()
        {
            name = string.Empty;
            host = string.Empty;
            gameMode = string.Empty;
            mapName = string.Empty;
        }
    }
}
