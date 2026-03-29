using UnityEngine;

/// <summary>
/// NGUI stub: internal draw call batching component.
/// Provides clipping enum used by UIPanel.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIDrawCall : MonoBehaviour
{
    public enum Clipping
    {
        None,
        TextureMask,
        SoftClip,
        ConstrainButDontClip,
    }
}
