using UnityEngine.Audio;

[System.Serializable]
public class SoundGroup
{
    public string name;
    public Sound[] sounds;
    public AudioMixerGroup mixerGroup;
}