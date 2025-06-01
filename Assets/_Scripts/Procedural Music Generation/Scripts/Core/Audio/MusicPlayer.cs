using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Handles music playback and audio generation
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicConfig musicConfig;
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool loopMusic = true;

    private AudioSource audioSource;
    private MusicGenerator generator;
    private Dictionary<string, Pattern> currentComposition;
    private Coroutine playbackCoroutine;

    public MusicConfig GetMusicConfig() => musicConfig;

    #region Unity Lifecycle

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicConfig != null)
        {
            generator = new MusicGenerator(musicConfig);

            if (playOnStart)
            {
                GenerateAndPlay();
            }
        }
        else
        {
            Debug.LogError("MusicPlayer: No MusicConfig assigned!");
        }
    }

    private void OnDestroy()
    {
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);
        }
    }

    #endregion

    #region Public Interface

    /// <summary>
    /// Generate new music and start playing
    /// </summary>
    public void GenerateAndPlay()
    {
        if (generator == null) return;

        StopPlayback();
        currentComposition = generator.GenerateComposition();
        StartPlayback();
    }

    /// <summary>
    /// Stop current playback
    /// </summary>
    public void StopPlayback()
    {
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);
            playbackCoroutine = null;
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Start playing the current composition
    /// </summary>
    public void StartPlayback()
    {
        if (currentComposition != null && currentComposition.Count > 0)
        {
            playbackCoroutine = StartCoroutine(PlayComposition());
        }
    }

    /// <summary>
    /// Generate new music with a different seed
    /// </summary>
    public void Regenerate()
    {
        if (musicConfig != null)
        {
            musicConfig.randomSeed = UnityEngine.Random.Range(1, 100000);
            generator = new MusicGenerator(musicConfig);
            GenerateAndPlay();
        }
    }

    #endregion

    #region Audio Generation and Playback

    private IEnumerator PlayComposition()
    {
        float totalDuration = CalculateTotalDuration();
        var audioClip = GenerateAudioClip(totalDuration);

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.loop = loopMusic;
            audioSource.volume = musicConfig.masterVolume;
            audioSource.Play();

            if (!loopMusic)
            {
                yield return new WaitForSeconds(totalDuration);
                playbackCoroutine = null;
            }
        }
    }

    private float CalculateTotalDuration()
    {
        float maxDuration = 0f;
        foreach (var pattern in currentComposition.Values)
        {
            maxDuration = Mathf.Max(maxDuration, pattern.totalBeats);
        }

        // Convert beats to seconds
        return (maxDuration * 60f) / musicConfig.tempo;
    }

    private AudioClip GenerateAudioClip(float duration)
    {
        int sampleCount = Mathf.RoundToInt(duration * musicConfig.sampleRate);
        var mixedSamples = new float[sampleCount];

        var instrumentSamples = new List<float[]>();

        foreach (var kvp in currentComposition)
        {
            var pattern = kvp.Value;
            var samples = GeneratePatternAudio(pattern, duration, musicConfig.sampleRate);
            instrumentSamples.Add(samples);
        }

        if (instrumentSamples.Count > 0)
        {
            mixedSamples = AudioSynthesizer.MixSamples(instrumentSamples.ToArray());
        }

        var audioClip = AudioClip.Create("Generated Music", sampleCount, 1, musicConfig.sampleRate, false);
        audioClip.SetData(mixedSamples, 0);

        return audioClip;
    }

    private float[] GeneratePatternAudio(Pattern pattern, float totalDuration, int sampleRate)
    {
        int sampleCount = Mathf.RoundToInt(totalDuration * sampleRate);
        var samples = new float[sampleCount];

        float beatsPerSecond = musicConfig.tempo / 60f;

        foreach (var phrase in pattern.phrases)
        {
            foreach (var note in phrase.notes)
            {
                float noteStartTime = note.startTime / beatsPerSecond;
                float noteDuration = note.duration / beatsPerSecond;
                float frequency = MusicTheory.MidiToFrequency(note.pitch);

                var noteSamples = AudioSynthesizer.GenerateWaveform(
                    frequency, noteDuration, sampleRate,
                    AudioSynthesizer.WaveType.Sine, note.velocity);

                int startSample = Mathf.RoundToInt(noteStartTime * sampleRate);

                for (int i = 0; i < noteSamples.Length && startSample + i < samples.Length; i++)
                {
                    samples[startSample + i] += noteSamples[i];
                }
            }
        }

        return samples;
    }

    #endregion
}