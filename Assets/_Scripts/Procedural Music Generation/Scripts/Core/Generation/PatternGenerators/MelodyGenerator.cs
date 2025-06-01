/// <summary>
/// Generates melody patterns
/// </summary>
public class MelodyGenerator : PatternGenerator
{
    public MelodyGenerator(MusicConfig config, System.Random random) : base(config, random) { }

    public override Pattern GeneratePattern(int[] scaleNotes, int[] chordProgression)
    {
        Pattern pattern = new Pattern(config.timeSignatureNumerator);

        for (int section = 0; section < config.numberOfSections; section++)
        {
            for (int measure = 0; measure < config.measuresPerSection; measure++)
            {
                Phrase phrase = GenerateMelodyPhrase(scaleNotes, chordProgression[measure % chordProgression.Length]);
                pattern.AddPhrase(phrase);
            }
        }

        return pattern;
    }

    private Phrase GenerateMelodyPhrase(int[] scaleNotes, int chordRoot)
    {
        Phrase phrase = new Phrase();
        float currentTime = 0f;
        float measureDuration = config.timeSignatureNumerator;

        while (currentTime < measureDuration)
        {
            float noteDuration = GetRandomNoteDuration();
            if (currentTime + noteDuration > measureDuration)
                noteDuration = measureDuration - currentTime;

            // Bias towards chord tones
            int pitch = SelectMelodyNote(scaleNotes, chordRoot);
            float velocity = 0.7f + (float)random.NextDouble() * config.dynamicRange;

            Note note = new Note(pitch, noteDuration, velocity, currentTime);
            phrase.AddNote(note);

            currentTime += noteDuration;

            // Add occasional rests
            if (random.NextDouble() < 0.2f)
            {
                float restDuration = GetRandomNoteDuration() * 0.5f;
                if (currentTime + restDuration <= measureDuration)
                    currentTime += restDuration;
            }
        }

        return phrase;
    }

    private int SelectMelodyNote(int[] scaleNotes, int chordRoot)
    {
        // 60% chance for chord tones, 40% for passing tones
        if (random.NextDouble() < 0.6f)
        {
            var chordNotes = MusicTheory.GetChordNotes(chordRoot, MusicTheory.ChordType.Major);
            return chordNotes[random.Next(chordNotes.Length)] + (random.Next(3) * 12); // Add octave variation
        }
        else
        {
            return scaleNotes[random.Next(scaleNotes.Length)] + (random.Next(3) * 12);
        }
    }

    private float GetRandomNoteDuration()
    {
        float[] durations = { 0.25f, 0.5f, 1f, 1.5f, 2f };
        return durations[random.Next(durations.Length)];
    }
}