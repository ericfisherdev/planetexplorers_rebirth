using UnityEngine;

/// <summary>
/// NGUI stub: tweens a UIWidget's color from one value to another.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class TweenColor : UITweener
{
    public Color from = Color.white;
    public Color to = Color.white;

    public Color value
    {
        get
        {
            var widget = GetComponent<UIWidget>();
            return widget != null ? widget.color : Color.white;
        }
        set
        {
            var widget = GetComponent<UIWidget>();
            if (widget != null) widget.color = value;
        }
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        value = Color.Lerp(from, to, factor);
    }

    public static TweenColor Begin(GameObject go, float duration, Color color)
    {
        var tween = UITweener.Begin<TweenColor>(go, duration);
        tween.from = tween.value;
        tween.to = color;
        return tween;
    }

    public static TweenColor Begin(GameObject go, float duration, Color from, Color to)
    {
        var tween = UITweener.Begin<TweenColor>(go, duration);
        tween.from = from;
        tween.to = to;
        return tween;
    }
}
