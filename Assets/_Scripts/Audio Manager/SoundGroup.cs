using System.Collections.Generic;
using UnityEngine.Audio;

[System.Serializable]
public class SoundGroup
{
    public string name;
    public Sound[] sounds;
    public AudioMixerGroup mixerGroup;

    private Dictionary<string, Sound> _sounds = new Dictionary<string, Sound>();

    public void AddSoundToDicitonary(Sound sound)
    {
        _sounds.Add(sound.name, sound);
    }

    public Sound GetSound(string name)
    {
        if (!_sounds.ContainsKey(name))
            return null;

        return _sounds[name];
    }
}