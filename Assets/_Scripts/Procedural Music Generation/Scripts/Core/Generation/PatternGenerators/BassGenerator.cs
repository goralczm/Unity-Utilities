/// <summary>
/// Generates bass patterns
/// </summary>
public class BassGenerator : PatternGenerator
{
    public BassGenerator(MusicConfig config, System.Random random) : base(config, random) { }

    public override Pattern GeneratePattern(int[] scaleNotes, int[] chordProgression)
    {
        Pattern pattern = new Pattern(config.timeSignatureNumerator);

        for (int section = 0; section < config.numberOfSections; section++)
        {
            for (int measure = 0; measure < config.measuresPerSection; measure++)
            {
                int chordRoot = chordProgression[measure % chordProgression.Length];
                Phrase phrase = GenerateBassPhrase(chordRoot);
                pattern.AddPhrase(phrase);
            }
        }

        return pattern;
    }

    private Phrase GenerateBassPhrase(int chordRoot)
    {
        Phrase phrase = new Phrase();

        // Simple bass pattern: root on beat 1, fifth on beat 3
        int rootNote = chordRoot - 24; // Two octaves lower
        int fifthNote = rootNote + 7;

        phrase.AddNote(new Note(rootNote, 2f, 0.8f, 0f));
        phrase.AddNote(new Note(fifthNote, 2f, 0.6f, 2f));

        return phrase;
    }
}