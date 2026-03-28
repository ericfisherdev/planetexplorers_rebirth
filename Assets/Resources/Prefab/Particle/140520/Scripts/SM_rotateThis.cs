using UnityEngine;
public class SM_rotateThis : MonoBehaviour
{
    public float rotationSpeedX = 90f;
    public float rotationSpeedY;
    public float rotationSpeedZ;
    public bool local = true;

    void Update()
    {
        var rot = new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime;
        if (local) transform.Rotate(rot);
        else transform.Rotate(rot, Space.World);
    }
}
