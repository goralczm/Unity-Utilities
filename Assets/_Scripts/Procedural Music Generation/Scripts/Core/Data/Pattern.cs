using System.Collections.Generic;

/// <summary>
/// Represents a complete musical pattern for one instrument
/// </summary>
[System.Serializable]
public class Pattern
{
    public List<Phrase> phrases;
    public int beatsPerMeasure;
    public float totalBeats;

    public Pattern(int beatsPerMeasure = 4)
    {
        phrases = new List<Phrase>();
        this.beatsPerMeasure = beatsPerMeasure;
        totalBeats = 0f;
    }

    public void AddPhrase(Phrase phrase)
    {
        phrases.Add(phrase);
        totalBeats += phrase.totalDuration;
    }
}