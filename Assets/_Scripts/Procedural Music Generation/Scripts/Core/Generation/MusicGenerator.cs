using System.Collections.Generic;

/// <summary>
/// Main music generation system
/// </summary>
public class MusicGenerator
{
    private MusicConfig config;
    private System.Random random;
    private Dictionary<string, PatternGenerator> generators;

    public MusicGenerator(MusicConfig config)
    {
        this.config = config;
        this.random = new System.Random(config.randomSeed);
        InitializeGenerators();
    }

    private void InitializeGenerators()
    {
        generators = new Dictionary<string, PatternGenerator>
        {
            { "melody", new MelodyGenerator(config, random) },
            { "bass", new BassGenerator(config, random) },
            { "chords", new ChordGenerator(config, random) }
        };
    }

    /// <summary>
    /// Generate a complete musical composition
    /// </summary>
    public Dictionary<string, Pattern> GenerateComposition()
    {
        var scaleNotes = MusicTheory.GetScaleNotes(config.rootNote, config.scale);
        var chordProgression = MusicTheory.GetChordProgression(config.rootNote, config.scale, config.chordProgression);

        var composition = new Dictionary<string, Pattern>();

        foreach (var instrumentConfig in config.instruments)
        {
            if (!instrumentConfig.enabled) continue;

            string generatorKey = GetGeneratorKey(instrumentConfig.type);
            if (generators.ContainsKey(generatorKey))
            {
                var pattern = generators[generatorKey].GeneratePattern(scaleNotes, chordProgression);
                composition[instrumentConfig.name] = pattern;
            }
        }

        return composition;
    }

    private string GetGeneratorKey(MusicConfig.InstrumentType type)
    {
        switch (type)
        {
            case MusicConfig.InstrumentType.Lead:
            case MusicConfig.InstrumentType.Arpeggio:
                return "melody";
            case MusicConfig.InstrumentType.Bass:
                return "bass";
            case MusicConfig.InstrumentType.Chord:
                return "chords";
            default:
                return "melody";
        }
    }
}