# Usage Examples

## Basic Setup
```csharp
// Create and configure music player
var musicPlayer = gameObject.AddComponent<MusicPlayer>();
musicPlayer.musicConfig = myMusicConfig;
musicPlayer.GenerateAndPlay();
```

## Adaptive Music
```csharp
// Adjust music based on game state
var adaptiveSystem = GetComponent<AdaptiveMusicSystem>();
adaptiveSystem.SetIntensity(0.8f, immediate: true);
```

## Custom Pattern Generator
```csharp
public class MyCustomGenerator : PatternGenerator
{
    public override Pattern GeneratePattern(int[] scaleNotes, int[] chordProgression)
    {
        // Your custom generation logic
        return pattern;
    }
}
```

## Export Audio
```csharp
// Export generated music to WAV
AudioClip clip = musicPlayer.GetComponent<AudioSource>().clip;
AudioExporter.ExportToWAV(clip, "my_music.wav");
```
