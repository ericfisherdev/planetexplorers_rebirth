// Stub: uLink.MasterServer - master server for server discovery
// All methods are no-ops in single-player.

namespace uLink
{
    public static class MasterServer
    {
        private static readonly HostData[] _emptyHostList = new HostData[0];

        public static string ipAddress { get; set; }
        public static int port { get; set; }
        public static string password { get; set; }
        public static float updateRate { get; set; }

        public static void RequestHostList(string gameTypeName) { }
        public static HostData[] PollHostList() { return _emptyHostList; }
        public static void ClearHostList() { }
        public static void DiscoverLocalHosts(int remotePort) { }
        public static HostData[] PollDiscoveredHosts() { return _emptyHostList; }
        public static void ClearDiscoveredHosts() { }
    }
}
