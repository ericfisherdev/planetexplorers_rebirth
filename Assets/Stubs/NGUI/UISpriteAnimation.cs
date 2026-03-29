using UnityEngine;

/// <summary>
/// NGUI stub: sprite animation component that cycles through atlas sprites by name prefix.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UISpriteAnimation : MonoBehaviour
{
    public int framesPerSecond = 30;
    public string namePrefix = "";
    public bool loop = true;

    public bool isPlaying { get; set; }

    public int frames { get { return 0; } }

    public void RebuildSpriteList() { }

    public void Play() { isPlaying = true; }

    public void Pause() { isPlaying = false; }

    public void ResetToBeginning() { }

    public void Stop() { isPlaying = false; }
}
