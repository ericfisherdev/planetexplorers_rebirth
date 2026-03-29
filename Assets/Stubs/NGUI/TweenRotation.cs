using UnityEngine;

/// <summary>
/// NGUI stub: tweens a transform's local rotation from one value to another.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class TweenRotation : UITweener
{
    public Vector3 from = Vector3.zero;
    public Vector3 to = Vector3.zero;
    public bool quaternionLerp;

    public Quaternion value
    {
        get { return transform.localRotation; }
        set { transform.localRotation = value; }
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        transform.localRotation = Quaternion.Euler(Vector3.Lerp(from, to, factor));
    }

    public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
    {
        var tween = UITweener.Begin<TweenRotation>(go, duration);
        tween.from = tween.value.eulerAngles;
        tween.to = rot.eulerAngles;
        return tween;
    }

    public static TweenRotation Begin(GameObject go, float duration, Vector3 from, Vector3 to)
    {
        var tween = UITweener.Begin<TweenRotation>(go, duration);
        tween.from = from;
        tween.to = to;
        return tween;
    }
}
