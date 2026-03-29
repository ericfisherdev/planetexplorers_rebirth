using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: root component that controls UI scaling and resolution adaptation.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIRoot : MonoBehaviour
{
    public enum Scaling
    {
        PixelPerfect,
        FixedSize,
        FixedSizeOnMobiles,
        Flexible,
        Constrained,
    }

    public enum Constraint
    {
        Fit,
        Fill,
        FitWidth,
        FitHeight,
    }

    public static List<UIRoot> list = new List<UIRoot>();

    public Scaling scalingStyle = Scaling.Flexible;
    public int manualHeight = 720;
    public int minimumHeight = 320;
    public int maximumHeight = 1536;
    public bool fitWidth;
    public bool fitHeight = true;
    public bool adjustByDPI;
    public bool shrinkPortraitUI;
    public Constraint constraint = Constraint.Fit;
    public int manualWidth = 1280;

    public int activeHeight { get { return manualHeight; } }

    public float pixelSizeAdjustment { get { return 1f; } }

    public static UIRoot GetRoot(GameObject go) { return null; }
}
