using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Sound Groups")]
    [SerializeField] private SoundGroup[] _soundGroups;

    private Dictionary<string, SoundGroup> _soundGroupsDictionary = new Dictionary<string, SoundGroup>();

    protected override void Awake()
    {
        base.Awake();
        foreach (SoundGroup group in _soundGroups)
        {
            _soundGroupsDictionary.Add(group.name, group);
            foreach (Sound sound in group.sounds)
            {
                group.AddSoundToDicitonary(sound);
                CreateSoundInstance(sound, group.mixerGroup);
            }
        }
    }

    private void CreateSoundInstance(Sound sound, AudioMixerGroup mixerGroup)
    {
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.outputAudioMixerGroup = mixerGroup;

        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
    }

    private SoundGroup FindGroup(string groupName)
    {
        if (!_soundGroupsDictionary.ContainsKey(groupName))
        {
            Debug.LogError("Sound Group: " + groupName + " not found!");
            return null;
        }

        return _soundGroupsDictionary[groupName];
    }

    private Sound FindSound(string groupName, string soundName)
    {
        SoundGroup g = FindGroup(groupName);
        if (g == null)
        {
            Debug.LogError("Sound Group: " + groupName + " not found!");
            return null;
        }

        Sound s = g.GetSound(soundName);

        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return null;
        }

        return s;
    }

    public void PlaySoundFromGroup(string groupName, string soundName)
    {
        Sound s = FindSound(groupName, soundName);
        if (s == null)
            return;

        s.source.Play();
    }

    public void PlaySoundFromGroupIfNotPlaying(string groupName, string soundName)
    {
        Sound s = FindSound(groupName, soundName);
        if (s == null)
            return;

        if (s.source.isPlaying)
            return;

        s.source.Play();
    }

    public void PlaySoundRandomFromGroup(string groupName)
    {
        SoundGroup g = FindGroup(groupName);
        if (g == null)
        {
            Debug.LogError("Sound Group: " + groupName + " not found!");
            return;
        }

        int randomSoundIndex = UnityEngine.Random.Range(0, g.sounds.Length);
        PlaySoundFromGroup(groupName, g.sounds[randomSoundIndex].name);
    }

    public void StopSound(string groupName, string soundName)
    {
        Sound s = FindSound(groupName, soundName);
        if (s == null)
            return;

        s.source.Stop();
    }
}
