using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: base progress bar widget with fill value and foreground/background references.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIProgressBar : UIWidget
{
    public enum FillDirection
    {
        LeftToRight,
        RightToLeft,
        BottomToTop,
        TopToBottom,
    }

    public static UIProgressBar current;

    public Transform thumb;
    public UIWidget foregroundWidget;
    public UIWidget backgroundWidget;
    public FillDirection fillDirection = FillDirection.LeftToRight;
    public int numberOfSteps;

    public List<EventDelegate> onValueChange = new List<EventDelegate>();
    public List<EventDelegate> onChange = new List<EventDelegate>();

    public float value { get; set; }

    public float sliderValue
    {
        get { return value; }
        set { this.value = value; }
    }

    public bool isValid { get { return true; } }

    public UIWidget foreground { get { return foregroundWidget; } set { foregroundWidget = value; } }

    public UIWidget background { get { return backgroundWidget; } set { backgroundWidget = value; } }

    public void ForceUpdate() { }
}
