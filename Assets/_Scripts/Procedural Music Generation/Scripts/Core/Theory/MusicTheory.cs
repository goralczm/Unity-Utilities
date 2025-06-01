using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains music theory utilities and data
/// </summary>
public static class MusicTheory
{
    public enum Scale
    {
        Major, Minor, Dorian, Mixolydian, Pentatonic, Blues
    }

    public enum ChordType
    {
        Major, Minor, Diminished, Augmented, Sus2, Sus4, Major7, Minor7, Dominant7
    }

    // Scale intervals (semitones from root)
    private static readonly Dictionary<Scale, int[]> ScaleIntervals = new Dictionary<Scale, int[]>
    {
        { Scale.Major, new int[] { 0, 2, 4, 5, 7, 9, 11 } },
        { Scale.Minor, new int[] { 0, 2, 3, 5, 7, 8, 10 } },
        { Scale.Dorian, new int[] { 0, 2, 3, 5, 7, 9, 10 } },
        { Scale.Mixolydian, new int[] { 0, 2, 4, 5, 7, 9, 10 } },
        { Scale.Pentatonic, new int[] { 0, 2, 4, 7, 9 } },
        { Scale.Blues, new int[] { 0, 3, 5, 6, 7, 10 } }
    };

    // Chord intervals (semitones from root)
    private static readonly Dictionary<ChordType, int[]> ChordIntervals = new Dictionary<ChordType, int[]>
    {
        { ChordType.Major, new int[] { 0, 4, 7 } },
        { ChordType.Minor, new int[] { 0, 3, 7 } },
        { ChordType.Diminished, new int[] { 0, 3, 6 } },
        { ChordType.Augmented, new int[] { 0, 4, 8 } },
        { ChordType.Sus2, new int[] { 0, 2, 7 } },
        { ChordType.Sus4, new int[] { 0, 5, 7 } },
        { ChordType.Major7, new int[] { 0, 4, 7, 11 } },
        { ChordType.Minor7, new int[] { 0, 3, 7, 10 } },
        { ChordType.Dominant7, new int[] { 0, 4, 7, 10 } }
    };

    /// <summary>
    /// Get notes in a scale starting from root note
    /// </summary>
    public static int[] GetScaleNotes(int rootNote, Scale scale)
    {
        var intervals = ScaleIntervals[scale];
        var notes = new int[intervals.Length];
        for (int i = 0; i < intervals.Length; i++)
        {
            notes[i] = rootNote + intervals[i];
        }
        return notes;
    }

    /// <summary>
    /// Get chord notes from root note and chord type
    /// </summary>
    public static int[] GetChordNotes(int rootNote, ChordType chordType)
    {
        var intervals = ChordIntervals[chordType];
        var notes = new int[intervals.Length];
        for (int i = 0; i < intervals.Length; i++)
        {
            notes[i] = rootNote + intervals[i];
        }
        return notes;
    }

    /// <summary>
    /// Convert MIDI note to frequency in Hz
    /// </summary>
    public static float MidiToFrequency(int midiNote)
    {
        return 440f * Mathf.Pow(2f, (midiNote - 69f) / 12f);
    }

    /// <summary>
    /// Get a common chord progression in the given scale
    /// </summary>
    public static int[] GetChordProgression(int rootNote, Scale scale, string progressionType = "I-V-vi-IV")
    {
        var scaleNotes = GetScaleNotes(rootNote, scale);

        switch (progressionType)
        {
            case "I-V-vi-IV":
                return new int[] { scaleNotes[0], scaleNotes[4], scaleNotes[5], scaleNotes[3] };
            case "vi-IV-I-V":
                return new int[] { scaleNotes[5], scaleNotes[3], scaleNotes[0], scaleNotes[4] };
            case "I-vi-IV-V":
                return new int[] { scaleNotes[0], scaleNotes[5], scaleNotes[3], scaleNotes[4] };
            default:
                return new int[] { scaleNotes[0], scaleNotes[4], scaleNotes[5], scaleNotes[3] };
        }
    }
}