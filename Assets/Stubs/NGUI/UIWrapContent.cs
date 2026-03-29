using UnityEngine;

/// <summary>
/// NGUI stub: virtual scrolling component that reuses children for large lists.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIWrapContent : MonoBehaviour
{
    public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);

    public int itemSize = 100;
    public int minIndex;
    public int maxIndex;
    public bool cullContent = true;

    public OnInitializeItem onInitializeItem;

    public void SortBasedOnScrollMovement() { }

    public void SortAlphabetically() { }

    public void WrapContent() { }

    public void ResetChildPositions() { }
}
