using UnityEngine;

/// <summary>
/// NGUI stub: legacy component that sends a named message on button click.
/// Used in 2 files for button-to-function binding.
/// Will be replaced with UnityEvent in Phase 8.
/// </summary>
public class UIButtonMessage : MonoBehaviour
{
    public enum Trigger
    {
        OnClick,
        OnMouseOver,
        OnMouseOut,
        OnPress,
        OnRelease,
        OnDoubleClick,
    }

    public GameObject target;
    public string functionName = "";
    public Trigger trigger = Trigger.OnClick;
    public bool includeChildren;
}
