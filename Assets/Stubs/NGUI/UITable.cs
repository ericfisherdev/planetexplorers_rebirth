using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: layout component that arranges children in a table with variable sizes.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UITable : MonoBehaviour
{
    public enum Direction
    {
        Down,
        Up,
    }

    public enum Sorting
    {
        None,
        Alphabetic,
        Horizontal,
        Vertical,
        Custom,
    }

    public int columns;
    public Direction direction = Direction.Down;
    public Sorting sorting = Sorting.None;
    public UIWidget.Pivot pivot = UIWidget.Pivot.TopLeft;
    public bool hideInactive = true;
    public bool keepWithinPanel;
    public Vector2 padding = Vector2.zero;
    public bool repositionNow;
    public bool mVariableHeight;

    public System.Comparison<Transform> onCustomSort;
    public System.Action<Transform, int> onReposition;

    public List<Transform> children
    {
        get
        {
            var list = new List<Transform>();
            var t = transform;
            for (int i = 0; i < t.childCount; ++i)
                list.Add(t.GetChild(i));
            return list;
        }
    }

    public void Reposition() { }

    public List<Transform> GetChildList()
    {
        return children;
    }
}
