using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: layout component that arranges children in a grid pattern.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIGrid : MonoBehaviour
{
    public enum Arrangement
    {
        Horizontal,
        Vertical,
        CellSnap,
    }

    public enum Sorting
    {
        None,
        Alphabetic,
        Horizontal,
        Vertical,
        Custom,
    }

    public Arrangement arrangement = Arrangement.Horizontal;
    public Sorting sorting = Sorting.None;
    public UIWidget.Pivot pivot = UIWidget.Pivot.TopLeft;
    public int maxPerLine;
    public float cellWidth = 200f;
    public float cellHeight = 200f;
    public bool animateSmoothly;
    public bool hideInactive = true;
    public bool keepWithinPanel;
    public bool repositionNow;
    public bool sorted;

    public System.Comparison<Transform> onCustomSort;
    public System.Action<Transform, int> onReposition;

    public bool repositionOnStart { get; set; }

    public void Reposition() { }

    public List<Transform> GetChildList()
    {
        var list = new List<Transform>();
        var t = transform;
        for (int i = 0; i < t.childCount; ++i)
            list.Add(t.GetChild(i));
        return list;
    }

    public Transform GetChild(int index)
    {
        var list = GetChildList();
        return (index >= 0 && index < list.Count) ? list[index] : null;
    }

    public int GetIndex(Transform trans)
    {
        var list = GetChildList();
        return list.IndexOf(trans);
    }

    public void AddChild(Transform trans) { }

    public void AddChild(Transform trans, bool sort) { }

    public bool RemoveChild(Transform trans) { return false; }

    public void ConstrainWithinPanel() { }

    public void SortAlphabetically() { }

    public void SortByName() { }
}
