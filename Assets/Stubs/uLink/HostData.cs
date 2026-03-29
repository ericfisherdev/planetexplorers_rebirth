// Stub: uLink.HostData - server information returned from master server queries

namespace uLink
{
    public class HostData
    {
        public string gameName { get; set; }
        public string gameType { get; set; }
        public string comment { get; set; }
        public string ipAddress { get; set; }
        public int port { get; set; }
        public int connectedPlayers { get; set; }
        public int playerLimit { get; set; }
        public bool useNat { get; set; }
        public bool passwordProtected { get; set; }
        public string[] ip { get; set; }

        public HostData()
        {
            gameName = string.Empty;
            gameType = string.Empty;
            comment = string.Empty;
            ipAddress = "127.0.0.1";
            ip = new string[0];
        }
    }
}
