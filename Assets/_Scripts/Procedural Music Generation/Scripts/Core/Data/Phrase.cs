using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a musical phrase containing multiple notes
/// </summary>
[System.Serializable]
public class Phrase
{
    public List<Note> notes;
    public float totalDuration;

    public Phrase()
    {
        notes = new List<Note>();
        totalDuration = 0f;
    }

    public void AddNote(Note note)
    {
        notes.Add(note);
        totalDuration = Mathf.Max(totalDuration, note.startTime + note.duration);
    }
}