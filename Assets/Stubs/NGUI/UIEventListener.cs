using UnityEngine;

/// <summary>
/// NGUI stub: event listener that attaches UI event handlers to GameObjects.
/// Use UIEventListener.Get(gameObject) to obtain or create an instance.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIEventListener : MonoBehaviour
{
    public delegate void VoidDelegate(GameObject go);
    public delegate void BoolDelegate(GameObject go, bool state);
    public delegate void FloatDelegate(GameObject go, float delta);
    public delegate void VectorDelegate(GameObject go, Vector2 delta);
    public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);
    public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
    public delegate void StringDelegate(GameObject go, string text);

    public object parameter;

    public VoidDelegate onClick;
    public VoidDelegate onDoubleClick;
    public BoolDelegate onHover;
    public BoolDelegate onPress;
    public BoolDelegate onSelect;
    public FloatDelegate onScroll;
    public VectorDelegate onDrag;
    public ObjectDelegate onDrop;
    public KeyCodeDelegate onKey;
    public BoolDelegate onTooltip;
    public VoidDelegate onSubmit;
    public StringDelegate onChange;

    public static UIEventListener Get(GameObject go)
    {
        if (go == null) return null;
        var listener = go.GetComponent<UIEventListener>();
        if (listener == null) listener = go.AddComponent<UIEventListener>();
        return listener;
    }

    public static UIEventListener Get(Transform trans)
    {
        if (trans == null) return null;
        return Get(trans.gameObject);
    }
}
