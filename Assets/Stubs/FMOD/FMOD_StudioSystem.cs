// Stub: FMOD_StudioSystem MonoBehaviour singleton
// Referenced by DebugCMD as FMOD_StudioSystem.instance.System for bank listing.

using UnityEngine;

/// <summary>
/// FMOD Studio system singleton. Game code accesses instance.System to query
/// loaded banks, events, VCAs, and buses.
/// </summary>
public class FMOD_StudioSystem : MonoBehaviour
{
    private static FMOD_StudioSystem _instance;

    public static FMOD_StudioSystem instance
    {
        get { return _instance; }
    }

    /// <summary>
    /// The underlying FMOD.Studio.System handle for low-level API access.
    /// </summary>
    public FMOD.Studio.System System { get; private set; }

    void Awake()
    {
        _instance = this;
    }
}
