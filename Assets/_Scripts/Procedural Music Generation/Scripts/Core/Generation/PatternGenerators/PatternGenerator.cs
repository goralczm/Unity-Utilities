/// <summary>
/// Base class for pattern generators
/// </summary>
public abstract class PatternGenerator
{
    protected MusicConfig config;
    protected System.Random random;

    public PatternGenerator(MusicConfig config, System.Random random)
    {
        this.config = config;
        this.random = random;
    }

    public abstract Pattern GeneratePattern(int[] scaleNotes, int[] chordProgression);
}