using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = .2f;
    [Range(.1f, 3f)] public float pitch = 1;
    public bool loop;
    [HideInInspector] public AudioSource source;
}