// Stub: FMODAsset ScriptableObject
// Referenced by FMODEventInspector as the inspected asset type.
// Holds event path and GUID for FMOD event references.

using UnityEngine;

/// <summary>
/// FMOD event asset reference. Holds path and id for editor inspection
/// and runtime event lookup.
/// </summary>
public class FMODAsset : ScriptableObject
{
    public string path = "";
    public string id = "";
}
