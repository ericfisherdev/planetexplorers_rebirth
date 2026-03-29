using UnityEngine;

/// <summary>
/// NGUI stub: allows dragging a target object by mouse/touch input.
/// Used in 4 files for draggable UI windows.
/// Will be replaced with functional uGUI drag handler in Phase 8.
/// </summary>
public class UIDragObject : MonoBehaviour
{
    public Transform target;
    public Vector3 scale = Vector3.one;
    public float momentumAmount = 35f;
    public bool restrictWithinPanel;
    public Transform contentRect;
    public DragEffect dragEffect = DragEffect.MomentumAndSpring;

    public enum DragEffect
    {
        None,
        Momentum,
        MomentumAndSpring,
    }
}
