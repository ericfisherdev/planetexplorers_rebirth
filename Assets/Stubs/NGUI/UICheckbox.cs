using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: legacy checkbox component (replaced by UIToggle in newer NGUI versions).
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UICheckbox : MonoBehaviour
{
    public static UICheckbox current;

    public UISprite checkSprite;
    public Animation checkAnimation;
    public bool startsChecked;
    public int optionGroup;

    public List<EventDelegate> onStateChange = new List<EventDelegate>();

    public bool isChecked { get; set; }

    public bool value
    {
        get { return isChecked; }
        set { isChecked = value; }
    }
}
