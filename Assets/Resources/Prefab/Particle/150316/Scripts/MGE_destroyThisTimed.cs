using UnityEngine;
public class MGE_destroyThisTimed : MonoBehaviour
{
    public float destroyTime = 5f;
    void Start() { Destroy(gameObject, destroyTime); }
}
