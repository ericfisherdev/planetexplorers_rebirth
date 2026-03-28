using UnityEngine;
public class SM_trailFade : MonoBehaviour
{
    public float fadeInTime = 0.1f;
    public float stayTime = 1f;
    public float fadeOutTime = 0.7f;
    public TrailRenderer thisTrail;
    private float timeElapsed;
    private float timeElapsedLast;
    private float percent;

    void Start()
    {
        thisTrail.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 1f));
        if (fadeInTime < 0.01f) fadeInTime = 0.01f;
        percent = timeElapsed / fadeInTime;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed <= fadeInTime)
        {
            percent = timeElapsed / fadeInTime;
            thisTrail.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, percent));
        }
        else if (timeElapsed < fadeInTime + stayTime)
        {
            thisTrail.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 1f));
        }
        else if (timeElapsed < fadeInTime + stayTime + fadeOutTime)
        {
            timeElapsedLast += Time.deltaTime;
            percent = 1f - (timeElapsedLast / fadeOutTime);
            thisTrail.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, percent));
        }
    }
}
