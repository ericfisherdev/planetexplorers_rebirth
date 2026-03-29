using UnityEngine;

/// <summary>
/// NGUI stub: base class for sprite-based widgets.
/// Provides fill, type, and flip properties.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIBasicSprite : UIWidget
{
    public enum Type
    {
        Simple,
        Sliced,
        Tiled,
        Filled,
        Advanced,
    }

    public enum FillDirection
    {
        Horizontal,
        Vertical,
        Radial90,
        Radial180,
        Radial360,
    }

    public enum Flip
    {
        Nothing,
        Horizontally,
        Vertically,
        Both,
    }

    public enum AdvancedType
    {
        Invisible,
        Sliced,
        Tiled,
    }

    public Type type = Type.Simple;
    public FillDirection fillDirection = FillDirection.Horizontal;
    public float fillAmount = 1f;
    public bool invert;
    public Flip flip = Flip.Nothing;
}
