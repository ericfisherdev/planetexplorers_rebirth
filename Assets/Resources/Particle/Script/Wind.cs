using UnityEngine;
public class Wind : MonoBehaviour
{
    public Vector3 speed;
    public Vector3 speedRedirect;
    void Start()
    {
        speed += new Vector3(
            Random.Range(-speedRedirect.x, speedRedirect.x),
            Random.Range(-speedRedirect.y, speedRedirect.y),
            Random.Range(-speedRedirect.z, speedRedirect.z));
    }
    void Update()
    {
        GetComponent<Rigidbody>().velocity += speed * Time.deltaTime;
    }
}
