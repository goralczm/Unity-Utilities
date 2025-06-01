/// <summary>
/// Represents a musical note with pitch, duration, and velocity
/// </summary>
[System.Serializable]
public struct Note
{
    public int pitch;      // MIDI note number (60 = C4)
    public float duration; // Duration in beats
    public float velocity; // Volume (0-1)
    public float startTime; // Start time in beats

    public Note(int pitch, float duration, float velocity = 1f, float startTime = 0f)
    {
        this.pitch = pitch;
        this.duration = duration;
        this.velocity = velocity;
        this.startTime = startTime;
    }
}