using UnityEngine;

/// <summary>
/// NGUI stub: camera component that handles UI input events (click, hover, drag, etc.).
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UICamera : MonoBehaviour
{
    public delegate void VoidDelegate(GameObject go);
    public delegate void BoolDelegate(GameObject go, bool state);
    public delegate void FloatDelegate(GameObject go, float delta);
    public delegate void VectorDelegate(GameObject go, Vector2 delta);
    public delegate void ObjectDelegate(GameObject go, GameObject obj);
    public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

    public static UICamera current;
    public static Camera currentCamera;
    public static Camera mainCamera;
    public static MouseOrTouch currentTouch;
    public static GameObject hoveredObject;
    public static GameObject selectedObject;
    public static bool inputHasFocus;
    public static bool isOverUI;
    public static int currentTouchID;
    public static RaycastHit lastHit;

    public LayerMask eventReceiverMask = -1;
    public bool useKeyboard = true;
    public bool useMouse = true;
    public bool useTouch = true;
    public bool stickyTooltip = true;
    public float tooltipDelay = 1f;
    public float mouseDragThreshold = 4f;
    public float mouseClickThreshold = 10f;
    public float touchDragThreshold = 40f;
    public float touchClickThreshold = 40f;

    public VoidDelegate onClick;
    public BoolDelegate onHover;
    public BoolDelegate onPress;
    public BoolDelegate onSelect;
    public FloatDelegate onScroll;
    public VectorDelegate onDrag;
    public ObjectDelegate onDrop;
    public KeyCodeDelegate onKey;
    public BoolDelegate onTooltip;

    public Camera cachedCamera { get { return GetComponent<Camera>(); } }

    public static bool IsHighlighted(GameObject go) { return false; }

    public static void Notify(GameObject go, string funcName, object obj) { }

    public static Ray ScreenPointToRay(Vector2 pos)
    {
        if (mainCamera != null) return mainCamera.ScreenPointToRay(pos);
        return default;
    }

    public static bool Raycast(Vector3 inPos) { return false; }
}

/// <summary>
/// NGUI stub: touch/mouse tracking data.
/// </summary>
public class MouseOrTouch
{
    public KeyCode key = KeyCode.None;
    public Vector2 pos = Vector2.zero;
    public Vector2 lastPos = Vector2.zero;
    public Vector2 delta = Vector2.zero;
    public Vector2 totalDelta = Vector2.zero;
    public Camera pressedCam;
    public GameObject current;
    public GameObject pressed;
    public GameObject dragged;
    public float clickTime;
    public bool touchBegan;
    public bool pressStarted;
    public bool dragStarted;
}
