using UnityEngine;

/// <summary>
/// NGUI stub: spring-based panel movement for scroll snap-back.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class SpringPanel : MonoBehaviour
{
    public Vector3 target = Vector3.zero;
    public float strength = 10f;

    public delegate void OnFinished();

    public OnFinished onFinished;

    public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
    {
        var sp = go.GetComponent<SpringPanel>();
        if (sp == null) sp = go.AddComponent<SpringPanel>();
        sp.target = pos;
        sp.strength = strength;
        sp.enabled = true;
        return sp;
    }

    public void Stop() { enabled = false; }
}
