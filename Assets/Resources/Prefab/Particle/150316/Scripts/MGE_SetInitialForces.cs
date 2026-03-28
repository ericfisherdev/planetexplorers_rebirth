using UnityEngine;
public class MGE_SetInitialForces : MonoBehaviour
{
    public bool relativeForce = true;
    public float x, xDeviation;
    public float y, yDeviation;
    public float z, zDeviation;
    public bool relativeTorque = true;
    public float torqueScale = 100f;
    public float xRot, xRotDeviation;
    public float yRot, yRotDeviation;
    public float zRot, zRotDeviation;

    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        var force = new Vector3(
            Random.Range(x - xDeviation, x + xDeviation),
            Random.Range(y - yDeviation, y + yDeviation),
            Random.Range(z - zDeviation, z + zDeviation));
        if (relativeForce) rb.AddRelativeForce(force);
        else rb.AddForce(force);

        var torque = new Vector3(
            Random.Range(xRot - xRotDeviation, xRot + xRotDeviation) * torqueScale,
            Random.Range(yRot - yRotDeviation, yRot + yRotDeviation) * torqueScale,
            Random.Range(zRot - zRotDeviation, zRot + zRotDeviation) * torqueScale);
        if (relativeTorque) rb.AddRelativeTorque(torque);
        else rb.AddTorque(torque);
    }
}
