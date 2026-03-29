using UnityEngine;

/// <summary>
/// NGUI stub: image-based button that swaps sprites on hover/press states.
/// Used in 8 files for toolbar and editor buttons.
/// Will be replaced with functional uGUI Button in Phase 8.
/// </summary>
public class UIImageButton : MonoBehaviour
{
    public UISprite target;
    public string normalSprite = "";
    public string hoverSprite = "";
    public string pressedSprite = "";
    public string disabledSprite = "";
    public bool isEnabled = true;
}
