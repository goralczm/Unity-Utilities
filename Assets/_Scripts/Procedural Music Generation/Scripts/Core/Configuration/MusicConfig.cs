using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Configuration data for music generation
/// </summary>
[CreateAssetMenu(fileName = "MusicConfig", menuName = "Procedural Music/Music Config")]
public class MusicConfig : ScriptableObject
{
    [Header("Basic Parameters")]
    public float tempo = 120f; // BPM
    public int rootNote = 60; // C4
    public MusicTheory.Scale scale = MusicTheory.Scale.Major;
    public int timeSignatureNumerator = 4;
    public int timeSignatureDenominator = 4;

    [Header("Structure")]
    public int measuresPerSection = 8;
    public int numberOfSections = 4;
    public string chordProgression = "I-V-vi-IV";

    [Header("Instruments")]
    public List<InstrumentConfig> instruments = new List<InstrumentConfig>();

    [Header("Generation")]
    public int randomSeed = 12345;
    public float complexity = 0.5f; // 0-1, affects note density
    public bool allowSyncopation = true;
    public float dynamicRange = 0.3f; // Velocity variation

    [Header("Audio")]
    public int sampleRate = 44100;
    public float masterVolume = 0.7f;

    [System.Serializable]
    public class InstrumentConfig
    {
        public string name;
        public InstrumentType type;
        public float volume = 1f;
        public int octaveRange = 2;
        public float noteDensity = 0.5f; // Notes per beat
        public bool enabled = true;
    }

    public enum InstrumentType
    {
        Lead, Bass, Chord, Percussion, Arpeggio
    }
}
