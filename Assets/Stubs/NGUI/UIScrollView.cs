using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: scrollable view container with momentum and drag support.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIScrollView : MonoBehaviour
{
    public enum Movement
    {
        Horizontal,
        Vertical,
        Unrestricted,
        Custom,
    }

    public enum DragEffect
    {
        None,
        Momentum,
        MomentumAndSpring,
    }

    public enum ShowCondition
    {
        Always,
        OnlyIfNeeded,
        WhenDragging,
    }

    public Movement movement = Movement.Horizontal;
    public DragEffect dragEffect = DragEffect.MomentumAndSpring;
    public float scrollWheelFactor = 0.25f;
    public float momentumAmount = 35f;
    public float dampenStrength = 9f;
    public bool restrictWithinPanel = true;
    public bool disableDragIfFits;
    public bool smoothDragStart = true;
    public bool iOSDragEmulation = true;
    public float contentPivot = 1f;

    public UIPanel panel;
    public UIScrollBar horizontalScrollBar;
    public UIScrollBar verticalScrollBar;
    public ShowCondition showScrollBars = ShowCondition.OnlyIfNeeded;
    public Vector3 currentMomentum = Vector3.zero;

    public UIProgressBar verticalBar { get { return verticalScrollBar; } }
    public UIProgressBar horizontalBar { get { return horizontalScrollBar; } }

    public Bounds bounds { get { return default; } }

    public bool canMoveHorizontally { get { return movement == Movement.Horizontal || movement == Movement.Unrestricted; } }
    public bool canMoveVertically { get { return movement == Movement.Vertical || movement == Movement.Unrestricted; } }

    public bool shouldMoveHorizontally { get { return false; } }
    public bool shouldMoveVertically { get { return false; } }

    public bool RestrictWithinBounds(bool instant) { return false; }

    public bool RestrictWithinBounds(bool instant, bool horizontal, bool vertical) { return false; }

    public void DisableSpring() { currentMomentum = Vector3.zero; }

    public void UpdateScrollbars() { }

    public void UpdateScrollbars(bool recalculateBounds) { }

    public void MoveRelative(Vector3 relative) { }

    public void MoveAbsolute(Vector3 absolute) { }

    public void Press(bool pressed) { }

    public void Drag() { }

    public void Scroll(float delta) { }

    public void ResetPosition() { }

    public void UpdatePosition() { }

    public void InvalidateBounds() { }

    public void SetDragAmount(float x, float y, bool updateScrollbars) { }
}
