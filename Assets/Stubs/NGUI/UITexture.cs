using UnityEngine;

/// <summary>
/// NGUI stub: widget that displays a Texture2D or RenderTexture.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UITexture : UIWidget
{
    public Texture mainTexture;
    public Material material;
    public Shader shader;
    public Rect uvRect = new Rect(0f, 0f, 1f, 1f);
    public bool fixedAspect;
}
