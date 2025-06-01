using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Advanced pattern generator with AI-like pattern evolution
/// </summary>
public class EvolutionaryPatternGenerator : PatternGenerator
{
    private List<Pattern> generationHistory;
    private float fitnessThreshold;

    public EvolutionaryPatternGenerator(MusicConfig config, System.Random random, float fitnessThreshold = 0.7f)
        : base(config, random)
    {
        this.generationHistory = new List<Pattern>();
        this.fitnessThreshold = fitnessThreshold;
    }

    public override Pattern GeneratePattern(int[] scaleNotes, int[] chordProgression)
    {
        Pattern newPattern = GenerateBasePattern(scaleNotes, chordProgression);

        if (generationHistory.Count > 0)
        {
            // Evolve based on previous patterns
            newPattern = EvolvePattern(newPattern, scaleNotes);
        }

        generationHistory.Add(newPattern);

        // Keep only recent history to prevent memory bloat
        if (generationHistory.Count > 10)
        {
            generationHistory.RemoveAt(0);
        }

        return newPattern;
    }

    private Pattern GenerateBasePattern(int[] scaleNotes, int[] chordProgression)
    {
        // Use melody generator as base
        var melodyGen = new MelodyGenerator(config, random);
        return melodyGen.GeneratePattern(scaleNotes, chordProgression);
    }

    private Pattern EvolvePattern(Pattern newPattern, int[] scaleNotes)
    {
        var lastPattern = generationHistory.Last();

        // Apply evolutionary mutations
        for (int i = 0; i < newPattern.phrases.Count && i < lastPattern.phrases.Count; i++)
        {
            if (random.NextDouble() < 0.3f) // 30% chance to inherit from previous
            {
                InheritPhraseCharacteristics(newPattern.phrases[i], lastPattern.phrases[i], scaleNotes);
            }
        }

        return newPattern;
    }

    private void InheritPhraseCharacteristics(Phrase current, Phrase previous, int[] scaleNotes)
    {
        // Inherit rhythmic patterns
        if (previous.notes.Count > 0 && current.notes.Count > 0)
        {
            for (int i = 0; i < Mathf.Min(current.notes.Count, previous.notes.Count); i++)
            {
                if (random.NextDouble() < 0.5f)
                {
                    var currentNote = current.notes[i];
                    var previousNote = previous.notes[i];

                    // Inherit duration but adjust pitch to fit current harmony
                    currentNote.duration = previousNote.duration;
                    current.notes[i] = currentNote;
                }
            }
        }
    }
}