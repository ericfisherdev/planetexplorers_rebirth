/// <summary>
/// NGUI stub: legacy filled sprite type (merged into UISprite in newer NGUI).
/// A UISprite with type=Filled as the default. Used in 34+ files for progress bars,
/// cooldown overlays, and health indicators.
/// Will be replaced with functional uGUI Image (filled) in Phase 8.
/// </summary>
public class UIFilledSprite : UISprite
{
    public UIFilledSprite()
    {
        type = Type.Filled;
    }
}
