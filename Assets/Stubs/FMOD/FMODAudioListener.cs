// Stub: FMODAudioListener MonoBehaviour
// Referenced by FMODAudioSourceRTE.cs via FMODAudioListener.listener static property.

using UnityEngine;

/// <summary>
/// FMOD audio listener singleton. FMODAudioSourceRTE accesses the static listener
/// property to calculate distance from the listener to audio sources.
/// </summary>
public class FMODAudioListener : MonoBehaviour
{
    private static FMODAudioListener _listener;

    public static FMODAudioListener listener
    {
        get { return _listener; }
    }

    void Awake()
    {
        _listener = this;
    }

    void OnDestroy()
    {
        if (_listener == this)
        {
            _listener = null;
        }
    }
}
