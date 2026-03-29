using UnityEngine;

/// <summary>
/// NGUI stub: static utility methods for common NGUI operations.
/// Will be replaced with functional uGUI equivalents in Phase 8.
/// </summary>
public static class NGUITools
{
    public static GameObject AddChild(GameObject parent)
    {
        return AddChild(parent, true);
    }

    public static GameObject AddChild(GameObject parent, bool undo)
    {
        var go = new GameObject("New Child");
        if (parent != null)
        {
            var t = go.transform;
            t.SetParent(parent.transform, false);
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    public static GameObject AddChild(GameObject parent, GameObject prefab)
    {
        if (prefab == null) return null;
        var go = Object.Instantiate(prefab);
        if (parent != null)
        {
            var t = go.transform;
            t.SetParent(parent.transform, false);
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    public static T AddChild<T>(GameObject parent) where T : Component
    {
        var go = AddChild(parent);
        return go.AddComponent<T>();
    }

    public static T AddChild<T>(GameObject parent, T prefab) where T : Component
    {
        var go = AddChild(parent, prefab.gameObject);
        return go != null ? go.GetComponent<T>() : null;
    }

    public static void SetActive(GameObject go, bool state)
    {
        if (go != null) go.SetActive(state);
    }

    public static void SetActive(GameObject go, bool state, bool compatibilityMode)
    {
        SetActive(go, state);
    }

    public static bool GetActive(GameObject go)
    {
        return go != null && go.activeInHierarchy;
    }

    public static bool GetActive(Behaviour mb)
    {
        return mb != null && mb.enabled && mb.gameObject.activeInHierarchy;
    }

    public static void Destroy(Object obj)
    {
        if (obj != null)
        {
            if (Application.isPlaying)
                Object.Destroy(obj);
            else
                Object.DestroyImmediate(obj);
        }
    }

    public static void DestroyImmediate(Object obj)
    {
        if (obj != null) Object.DestroyImmediate(obj);
    }

    public static T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        return go.GetComponentInParent<T>();
    }

    public static T FindInParents<T>(Transform trans) where T : Component
    {
        if (trans == null) return null;
        return trans.GetComponentInParent<T>();
    }

    public static void SetLayer(GameObject go, int layer)
    {
        if (go == null) return;
        go.layer = layer;
        var t = go.transform;
        for (int i = 0, imax = t.childCount; i < imax; ++i)
            SetLayer(t.GetChild(i).gameObject, layer);
    }

    public static void MakePixelPerfect(Transform t) { }

    public static void AddWidgetCollider(GameObject go) { }

    public static void AddWidgetCollider(GameObject go, bool considerInactive) { }

    public static UIRoot GetRoot(GameObject go)
    {
        return go != null ? go.GetComponentInParent<UIRoot>() : null;
    }

    public static string GetTypeName<T>()
    {
        return typeof(T).Name;
    }

    public static string GetTypeName(Object obj)
    {
        return obj != null ? obj.GetType().Name : "Null";
    }

    public static string GetHierarchy(GameObject go)
    {
        if (go == null) return "";
        var path = go.name;
        var parent = go.transform.parent;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }

    public static AudioSource PlaySound(AudioClip clip)
    {
        return PlaySound(clip, 1f, 1f);
    }

    public static AudioSource PlaySound(AudioClip clip, float volume)
    {
        return PlaySound(clip, volume, 1f);
    }

    public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
    {
        return null;
    }

    public static Camera FindCameraForLayer(int layer)
    {
        return null;
    }

    public static string EncodeColor(Color c)
    {
        return EncodeColor24(c);
    }

    public static string EncodeColor24(Color c)
    {
        int r = Mathf.Clamp(Mathf.RoundToInt(c.r * 255f), 0, 255);
        int g = Mathf.Clamp(Mathf.RoundToInt(c.g * 255f), 0, 255);
        int b = Mathf.Clamp(Mathf.RoundToInt(c.b * 255f), 0, 255);
        return r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
    }

    public static Color ParseColor(string text, int offset)
    {
        return Color.white;
    }

    public static int CalculateNextDepth(GameObject go)
    {
        return 0;
    }

    public static int CalculateNextDepth(GameObject go, bool considerInactive)
    {
        return 0;
    }

    public static T AddWidget<T>(GameObject go) where T : UIWidget
    {
        return go != null ? go.AddComponent<T>() : null;
    }

    public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
    {
        var sprite = go != null ? go.AddComponent<UISprite>() : null;
        if (sprite != null)
        {
            sprite.atlas = atlas;
            sprite.spriteName = spriteName;
        }
        return sprite;
    }

    public static Bounds CalculateRelativeWidgetBounds(Transform root)
    {
        return default;
    }

    public static Bounds CalculateRelativeWidgetBounds(Transform root, bool considerInactive)
    {
        return default;
    }

    public static Bounds CalculateAbsoluteWidgetBounds(Transform trans)
    {
        return default;
    }

    public static void UpdateWidgetCollider(GameObject go) { }

    public static void UpdateWidgetCollider(BoxCollider box, bool considerInactive) { }

    public static int AdjustDepth(GameObject go, int adjustment)
    {
        return 0;
    }

    public static void BringForward(GameObject go) { }

    public static void PushBack(GameObject go) { }

    public static void NormalizeDepths() { }

    public static void NormalizeWidgetDepths() { }
}
