using static UnityEngine.GraphicsBuffer;
using UnityEditor;

using UnityEngine;

/// <summary>
/// Custom inspector for MusicConfig with validation
/// </summary>
[CustomEditor(typeof(MusicConfig))]
public class MusicConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MusicConfig config = (MusicConfig)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Configuration Validation", EditorStyles.boldLabel);

        if (config.instruments.Count == 0)
        {
            EditorGUILayout.HelpBox("No instruments configured. Add at least one instrument to generate music.", MessageType.Warning);
        }

        if (config.tempo < 60 || config.tempo > 200)
        {
            EditorGUILayout.HelpBox("Tempo should typically be between 60-200 BPM for best results.", MessageType.Info);
        }

        if (GUILayout.Button("Add Default Instruments"))
        {
            AddDefaultInstruments(config);
        }
    }

    private void AddDefaultInstruments(MusicConfig config)
    {
        config.instruments.Clear();

        config.instruments.Add(new MusicConfig.InstrumentConfig
        {
            name = "Lead",
            type = MusicConfig.InstrumentType.Lead,
            volume = 0.8f,
            noteDensity = 0.6f
        });

        config.instruments.Add(new MusicConfig.InstrumentConfig
        {
            name = "Bass",
            type = MusicConfig.InstrumentType.Bass,
            volume = 0.7f,
            noteDensity = 0.3f
        });

        config.instruments.Add(new MusicConfig.InstrumentConfig
        {
            name = "Chords",
            type = MusicConfig.InstrumentType.Chord,
            volume = 0.5f,
            noteDensity = 0.2f
        });

        EditorUtility.SetDirty(config);
    }
}