using System.Linq;
using UnityEngine;

/// <summary>
/// Handles audio synthesis and waveform generation
/// </summary>
public class AudioSynthesizer
{
    public enum WaveType
    {
        Sine, Square, Sawtooth, Triangle, Noise
    }

    /// <summary>
    /// Generate a waveform for the given parameters
    /// </summary>
    public static float[] GenerateWaveform(float frequency, float duration, int sampleRate,
        WaveType waveType = WaveType.Sine, float volume = 1f)
    {
        int sampleCount = Mathf.RoundToInt(duration * sampleRate);
        float[] samples = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            float time = (float)i / sampleRate;
            float sample = GenerateSample(frequency, time, waveType) * volume;

            // Apply envelope (ADSR simplified to fade in/out)
            float envelope = CalculateEnvelope(time, duration);
            samples[i] = sample * envelope;
        }

        return samples;
    }

    private static float GenerateSample(float frequency, float time, WaveType waveType)
    {
        float phase = 2f * Mathf.PI * frequency * time;

        switch (waveType)
        {
            case WaveType.Sine:
                return Mathf.Sin(phase);
            case WaveType.Square:
                return Mathf.Sin(phase) > 0 ? 1f : -1f;
            case WaveType.Sawtooth:
                return 2f * (phase / (2f * Mathf.PI) - Mathf.Floor(phase / (2f * Mathf.PI) + 0.5f));
            case WaveType.Triangle:
                return 2f * Mathf.Abs(2f * (phase / (2f * Mathf.PI) - Mathf.Floor(phase / (2f * Mathf.PI) + 0.5f))) - 1f;
            case WaveType.Noise:
                return UnityEngine.Random.Range(-1f, 1f);
            default:
                return Mathf.Sin(phase);
        }
    }

    private static float CalculateEnvelope(float time, float duration)
    {
        float attack = 0.1f;
        float release = 0.2f;

        if (time < attack)
            return time / attack;
        else if (time > duration - release)
            return (duration - time) / release;
        else
            return 1f;
    }

    /// <summary>
    /// Mix multiple audio samples together
    /// </summary>
    public static float[] MixSamples(params float[][] sampleArrays)
    {
        if (sampleArrays.Length == 0) return new float[0];

        int maxLength = sampleArrays.Max(s => s.Length);
        float[] mixed = new float[maxLength];

        for (int i = 0; i < maxLength; i++)
        {
            float sum = 0f;
            int count = 0;

            foreach (var samples in sampleArrays)
            {
                if (i < samples.Length)
                {
                    sum += samples[i];
                    count++;
                }
            }

            mixed[i] = count > 0 ? sum / count : 0f; // Average to prevent clipping
        }

        return mixed;
    }
}