using UnityEngine;
public class randomRotate : MonoBehaviour
{
    public float rotateEverySecond = 1f;
    private Quaternion rotTarget;

    void Start()
    {
        RandomRot();
        InvokeRepeating(nameof(RandomRot), 0f, rotateEverySecond);
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotTarget, Time.time * Time.deltaTime);
    }

    void RandomRot()
    {
        rotTarget = Random.rotation;
    }
}
