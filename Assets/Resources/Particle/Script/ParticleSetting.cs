using UnityEngine;
public class ParticleSetting : MonoBehaviour
{
    public float LightIntensityMult = -0.5f;
    public float LifeTime = 1f;
    public bool RandomRotation;
    public Vector3 PositionOffset;
    public GameObject SpawnEnd;
    private float timetemp;

    void Start()
    {
        timetemp = Time.time;
        if (RandomRotation)
        {
            transform.rotation = Random.rotation;
        }
    }

    void Update()
    {
        if (Time.time > timetemp + LifeTime)
        {
            if (SpawnEnd != null)
                Instantiate(SpawnEnd, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        var light = GetComponent<Light>();
        if (light != null)
            light.intensity += LightIntensityMult * Time.deltaTime;
    }
}
