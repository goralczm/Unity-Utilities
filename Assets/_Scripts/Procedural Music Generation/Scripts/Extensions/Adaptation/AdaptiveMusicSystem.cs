using System.Collections;
using UnityEngine;

/// <summary>
/// Real-time music adaptation system
/// </summary>
public class AdaptiveMusicSystem : MonoBehaviour
{
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private float adaptationInterval = 8f; // Seconds
    [SerializeField] private AnimationCurve intensityCurve;

    private float currentIntensity = 0.5f;
    private Coroutine adaptationCoroutine;

    public float CurrentIntensity
    {
        get => currentIntensity;
        set => currentIntensity = Mathf.Clamp01(value);
    }

    private void Start()
    {
        if (musicPlayer != null)
        {
            adaptationCoroutine = StartCoroutine(AdaptationLoop());
        }
    }

    private void OnDestroy()
    {
        if (adaptationCoroutine != null)
        {
            StopCoroutine(adaptationCoroutine);
        }
    }

    private IEnumerator AdaptationLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(adaptationInterval);
            AdaptMusic();
        }
    }

    private void AdaptMusic()
    {
        if (musicPlayer == null) return;

        // Modify music config based on current intensity
        var config = musicPlayer.GetComponent<MusicPlayer>().GetMusicConfig();
        if (config != null)
        {
            // Adjust tempo based on intensity
            float baseTempo = 120f;
            config.tempo = baseTempo + (currentIntensity * 40f); // 120-160 BPM range

            // Adjust complexity
            config.complexity = intensityCurve.Evaluate(currentIntensity);

            // Regenerate music with new parameters
            musicPlayer.GenerateAndPlay();
        }
    }

    /// <summary>
    /// Set music intensity based on game events
    /// </summary>
    public void SetIntensity(float intensity, bool immediate = false)
    {
        currentIntensity = Mathf.Clamp01(intensity);

        if (immediate)
        {
            AdaptMusic();
        }
    }
}