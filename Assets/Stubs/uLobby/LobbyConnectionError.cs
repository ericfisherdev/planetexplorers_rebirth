// Stub: uLobby.LobbyConnectionError - error codes for lobby connections

namespace uLobby
{
    public enum LobbyConnectionError
    {
        NoError,
        ConnectionFailed,
        Timeout,
        InvalidCredentials,
        ServerFull,
        Banned,
        RSAPublicKeyMismatch,
        CreateSocketOrThreadFailure,
        ConnectionTimeout
    }
}
