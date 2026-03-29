using UnityEngine;

/// <summary>
/// NGUI stub: tooltip display utility with static ShowText method.
/// Used in 7+ files for item/button hover descriptions.
/// Will be replaced with functional uGUI tooltip in Phase 8.
/// </summary>
public class UITooltip : MonoBehaviour
{
    public static UITooltip instance;

    public UILabel text;

    public static void ShowText(string tooltipText)
    {
        // Stub: no-op in compilation stub
    }

    public static void Show(string tooltipText)
    {
        ShowText(tooltipText);
    }
}
