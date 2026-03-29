using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: component that triggers tween playback on UI events.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIPlayTween : MonoBehaviour
{
    public enum Trigger
    {
        OnClick,
        OnHover,
        OnPress,
        OnHoverTrue,
        OnHoverFalse,
        OnPressTrue,
        OnPressFalse,
        OnActivate,
        OnActivateTrue,
        OnActivateFalse,
        OnDoubleClick,
        OnSelect,
        OnSelectTrue,
        OnSelectFalse,
        Manual,
    }

    public enum Direction
    {
        Toggle,
        Forward,
        Reverse,
    }

    public GameObject tweenTarget;
    public int tweenGroup;
    public Trigger trigger = Trigger.OnClick;
    public Direction playDirection = Direction.Forward;
    public bool resetOnPlay;
    public bool resetIfDisabled;
    public EnableCondition ifDisabledOnPlay = EnableCondition.DoNothing;
    public DisableCondition disableWhenFinished = DisableCondition.DoNotDisable;
    public bool includeChildren;

    public List<EventDelegate> onFinished = new List<EventDelegate>();

    public enum EnableCondition
    {
        DoNothing,
        EnableThenPlay,
    }

    public enum DisableCondition
    {
        DoNotDisable,
        DisableAfterForward,
        DisableAfterReverse,
        DisableAfterBoth,
    }

    public void Play(bool forward) { }

    public void ResetToBeginning() { }
}
