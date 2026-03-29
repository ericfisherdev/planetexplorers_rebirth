using UnityEngine;

/// <summary>
/// NGUI stub: tweens a transform's local position from one value to another.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class TweenPosition : UITweener
{
    public Vector3 from = Vector3.zero;
    public Vector3 to = Vector3.zero;
    public bool worldSpace;

    public Vector3 value
    {
        get { return worldSpace ? transform.position : transform.localPosition; }
        set
        {
            if (worldSpace) transform.position = value;
            else transform.localPosition = value;
        }
    }

    public Transform cachedTransform { get { return transform; } }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        value = Vector3.Lerp(from, to, factor);
    }

    public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
    {
        var tween = UITweener.Begin<TweenPosition>(go, duration);
        tween.from = tween.value;
        tween.to = pos;
        return tween;
    }

    public static TweenPosition Begin(GameObject go, float duration, Vector3 from, Vector3 to)
    {
        var tween = UITweener.Begin<TweenPosition>(go, duration);
        tween.from = from;
        tween.to = to;
        return tween;
    }

    [System.Obsolete("Use TweenPosition.Begin instead")]
    public static TweenPosition Play(GameObject go, float duration, Vector3 pos)
    {
        return Begin(go, duration, pos);
    }
}
