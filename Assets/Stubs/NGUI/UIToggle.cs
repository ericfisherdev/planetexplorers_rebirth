using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: toggle (checkbox/radio button) with group support.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIToggle : MonoBehaviour
{
    public static UIToggle current;

    public int group;
    public UIWidget activeSprite;
    public Animation activeAnimation;
    public bool startsActive;
    public bool instantTween;
    public bool optionCanBeNone;

    public List<EventDelegate> onChange = new List<EventDelegate>();

    public bool value { get; set; }

    public static UIToggle GetActiveToggle(int group) { return null; }

    public void Set(bool state) { value = state; }

    public void Set(bool state, bool notify) { value = state; }
}
