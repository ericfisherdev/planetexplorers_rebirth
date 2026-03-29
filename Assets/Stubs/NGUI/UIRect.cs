using UnityEngine;

/// <summary>
/// NGUI stub: base class for all NGUI UI elements.
/// Provides anchor and dimension functionality.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIRect : MonoBehaviour
{
    public GameObject leftAnchor;
    public GameObject rightAnchor;
    public GameObject bottomAnchor;
    public GameObject topAnchor;

    public int width;
    public int height;

    public virtual float alpha { get; set; }

    public float finalAlpha { get { return alpha; } }

    public virtual void SetRect(float x, float y, float width, float height) { }

    public virtual void ParentHasChanged() { }

    public virtual void SetAnchor(Transform t) { }

    public virtual void SetAnchor(GameObject go) { }

    public virtual void ResetAnchors() { }

    public virtual void Update() { }

    public Camera anchorCamera { get { return null; } }

    public bool isAnchored { get { return false; } }

    public UIRect parent { get { return null; } }

    public UIRoot root { get { return null; } }
}
