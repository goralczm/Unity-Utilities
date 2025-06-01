using System.Collections.Generic;

using System.Linq;
using UnityEngine;

/// <summary>
/// Music visualization utility for debugging and analysis
/// </summary>
public static class MusicVisualizer
{
    /// <summary>
    /// Generate a simple piano roll representation
    /// </summary>
    public static string GeneratePianoRoll(Pattern pattern, int minNote = 48, int maxNote = 84)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int noteRange = maxNote - minNote + 1;
        float timeResolution = 0.25f; // Quarter note resolution

        float totalTime = pattern.totalBeats;
        int timeSteps = Mathf.CeilToInt(totalTime / timeResolution);

        // Create grid
        bool[,] grid = new bool[noteRange, timeSteps];

        // Fill grid with notes
        foreach (var phrase in pattern.phrases)
        {
            foreach (var note in phrase.notes)
            {
                if (note.pitch >= minNote && note.pitch <= maxNote)
                {
                    int noteIndex = note.pitch - minNote;
                    int startTime = Mathf.FloorToInt(note.startTime / timeResolution);
                    int duration = Mathf.CeilToInt(note.duration / timeResolution);

                    for (int t = startTime; t < startTime + duration && t < timeSteps; t++)
                    {
                        grid[noteIndex, t] = true;
                    }
                }
            }
        }

        // Generate text representation
        for (int n = noteRange - 1; n >= 0; n--)
        {
            sb.Append($"{(minNote + n):D2} |");
            for (int t = 0; t < timeSteps; t++)
            {
                sb.Append(grid[n, t] ? "█" : "·");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    /// <summary>
    /// Analyze pattern complexity
    /// </summary>
    public static float AnalyzeComplexity(Pattern pattern)
    {
        float complexity = 0f;
        int totalNotes = 0;

        foreach (var phrase in pattern.phrases)
        {
            totalNotes += phrase.notes.Count;

            // Rhythmic complexity
            var uniqueDurations = phrase.notes.Select(n => n.duration).Distinct().Count();
            complexity += uniqueDurations * 0.1f;

            // Pitch complexity
            var pitchRange = phrase.notes.Max(n => n.pitch) - phrase.notes.Min(n => n.pitch);
            complexity += pitchRange * 0.01f;

            // Velocity variation
            var velocityVariance = CalculateVariance(phrase.notes.Select(n => n.velocity));
            complexity += velocityVariance;
        }

        // Note density
        complexity += (totalNotes / pattern.totalBeats) * 0.5f;

        return Mathf.Clamp01(complexity);
    }

    private static float CalculateVariance(IEnumerable<float> values)
    {
        var valueArray = values.ToArray();
        if (valueArray.Length == 0) return 0f;

        float mean = valueArray.Average();
        float variance = valueArray.Select(v => (v - mean) * (v - mean)).Average();
        return variance;
    }
}