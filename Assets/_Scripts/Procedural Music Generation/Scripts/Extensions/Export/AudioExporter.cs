using UnityEngine;

/// <summary>
/// Audio export utility for saving generated music
/// </summary>
public static class AudioExporter
{
    /// <summary>
    /// Export AudioClip to WAV file
    /// </summary>
    public static bool ExportToWAV(AudioClip clip, string filepath)
    {
        if (clip == null) return false;

        try
        {
            var samples = new float[clip.samples];
            clip.GetData(samples, 0);

            using (var fileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Create))
            using (var writer = new System.IO.BinaryWriter(fileStream))
            {
                // WAV header
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((short)1); // PCM
                writer.Write((short)clip.channels);
                writer.Write(clip.frequency);
                writer.Write(clip.frequency * clip.channels * 2);
                writer.Write((short)(clip.channels * 2));
                writer.Write((short)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                // Audio data
                foreach (float sample in samples)
                {
                    short intSample = (short)(sample * short.MaxValue);
                    writer.Write(intSample);
                }
            }

            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to export WAV: {e.Message}");
            return false;
        }
    }
}