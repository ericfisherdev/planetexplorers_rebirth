using UnityEngine;
public class ExplosionObject : MonoBehaviour
{
    public Vector3 Force;
    public GameObject Objcet; // preserve original typo for prefab reference compatibility
    public int Num;
    public int Scale = 20;
    public int ScaleMin = 10;
    public AudioClip[] Sounds;
    public float LifeTimeObject = 2f;
    public float postitionoffset = 2f; // preserve original typo
    public bool random;

    void Start()
    {
        Physics.IgnoreLayerCollision(2, 2);
        if (Sounds.Length > 0)
            AudioSource.PlayClipAtPoint(Sounds[Random.Range(0, Sounds.Length)], transform.position);
        if (Objcet != null)
        {
            int num = random ? Random.Range(1, Num) : Num;
            for (int i = 0; i < num; i++)
            {
                var pos = new Vector3(
                    Random.Range(-postitionoffset, postitionoffset),
                    Random.Range(-postitionoffset, postitionoffset),
                    Random.Range(-postitionoffset, postitionoffset)) / 10f;
                var obj = Instantiate(Objcet, transform.position + pos, Random.rotation);
                Destroy(obj, LifeTimeObject);
                if (Scale > 0)
                {
                    float scale = Random.Range(ScaleMin, Scale);
                    obj.transform.localScale = new Vector3(scale, scale, scale);
                }
                var rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddForce(new Vector3(
                        Random.Range(-Force.x, Force.x),
                        Random.Range(-Force.y, Force.y),
                        Random.Range(-Force.z, Force.z)));
            }
        }
    }
}
