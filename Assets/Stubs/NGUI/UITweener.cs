using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: base class for all tween animations (position, rotation, scale, alpha, color).
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UITweener : MonoBehaviour
{
    public enum Method
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut,
        BounceIn,
        BounceOut,
    }

    public enum Style
    {
        Once,
        Loop,
        PingPong,
    }

    public static UITweener current;

    public Method method = Method.Linear;
    public Style style = Style.Once;
    public AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
    public float duration = 1f;
    public float delay;
    public int tweenGroup;
    public bool ignoreTimeScale = true;
    public bool steeperCurves;

    public List<EventDelegate> onFinished = new List<EventDelegate>();

    public float tweenFactor { get; set; }

    public bool isActiveAndEnabled { get { return enabled && gameObject.activeInHierarchy; } }

    public float amountPerDelta
    {
        get { return duration > 0f ? 1f / duration : 1000f; }
    }

    public Direction direction { get { return amountPerDelta < 0f ? Direction.Reverse : Direction.Forward; } }

    public enum Direction
    {
        Reverse = -1,
        Toggle = 0,
        Forward = 1,
    }

    public void PlayForward() { Play(true); }

    public void PlayReverse() { Play(false); }

    public void Play(bool forward)
    {
        enabled = true;
    }

    public void ResetToBeginning()
    {
        tweenFactor = 0f;
    }

    public void SetStartToCurrentValue() { }

    public void SetEndToCurrentValue() { }

    public void Sample(float factor, bool isFinished) { }

    public void Toggle() { Play(tweenFactor < 0.5f); }

    protected virtual void OnUpdate(float factor, bool isFinished) { }

    public static T Begin<T>(GameObject go, float duration) where T : UITweener
    {
        var tween = go.GetComponent<T>();
        if (tween == null) tween = go.AddComponent<T>();
        tween.duration = duration;
        tween.tweenFactor = 0f;
        tween.enabled = true;
        return tween;
    }
}
