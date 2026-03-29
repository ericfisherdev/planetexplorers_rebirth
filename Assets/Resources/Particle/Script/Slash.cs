using UnityEngine;
public class Slash : MonoBehaviour
{
    public Vector3 speed = Vector3.one;

    void Update()
    {
        var ls = transform.localScale;
        ls.x += speed.x * Time.deltaTime;
        ls.y += speed.y * Time.deltaTime;
        if (ls.y < 0f) ls.y = 0f;
        transform.localScale = ls;
    }
}
