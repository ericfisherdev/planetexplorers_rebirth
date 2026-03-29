using UnityEngine;

/// <summary>
/// NGUI stub: tweens a transform's local scale from one value to another.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class TweenScale : UITweener
{
    public Vector3 from = Vector3.one;
    public Vector3 to = Vector3.one;
    public bool updateTable;

    public Vector3 value
    {
        get { return transform.localScale; }
        set { transform.localScale = value; }
    }

    public Transform cachedTransform { get { return transform; } }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        value = Vector3.Lerp(from, to, factor);
    }

    public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
    {
        var tween = UITweener.Begin<TweenScale>(go, duration);
        tween.from = tween.value;
        tween.to = scale;
        return tween;
    }

    public static TweenScale Begin(GameObject go, float duration, Vector3 from, Vector3 to)
    {
        var tween = UITweener.Begin<TweenScale>(go, duration);
        tween.from = from;
        tween.to = to;
        return tween;
    }

    [System.Obsolete("Use TweenScale.Begin instead")]
    public static TweenScale Play(GameObject go, float duration, Vector3 scale)
    {
        return Begin(go, duration, scale);
    }
}
