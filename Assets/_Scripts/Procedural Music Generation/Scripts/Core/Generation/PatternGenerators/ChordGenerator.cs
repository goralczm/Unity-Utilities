/// <summary>
/// Generates chord patterns
/// </summary>
public class ChordGenerator : PatternGenerator
{
    public ChordGenerator(MusicConfig config, System.Random random) : base(config, random) { }

    public override Pattern GeneratePattern(int[] scaleNotes, int[] chordProgression)
    {
        Pattern pattern = new Pattern(config.timeSignatureNumerator);

        for (int section = 0; section < config.numberOfSections; section++)
        {
            for (int measure = 0; measure < config.measuresPerSection; measure++)
            {
                int chordRoot = chordProgression[measure % chordProgression.Length];
                Phrase phrase = GenerateChordPhrase(chordRoot);
                pattern.AddPhrase(phrase);
            }
        }

        return pattern;
    }

    private Phrase GenerateChordPhrase(int chordRoot)
    {
        Phrase phrase = new Phrase();
        var chordNotes = MusicTheory.GetChordNotes(chordRoot, MusicTheory.ChordType.Major);

        // Strum pattern: play all chord notes with slight timing offset
        for (int i = 0; i < chordNotes.Length; i++)
        {
            float startTime = i * 0.05f; // Slight strum effect
            phrase.AddNote(new Note(chordNotes[i], 3.5f, 0.5f, startTime));
        }

        return phrase;
    }
}