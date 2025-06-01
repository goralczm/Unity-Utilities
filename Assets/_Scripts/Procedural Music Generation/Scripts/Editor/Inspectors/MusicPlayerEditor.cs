using static UnityEngine.GraphicsBuffer;
using UnityEditor;

using UnityEngine;

/// <summary>
/// Custom inspector for MusicPlayer with preview controls
/// </summary>
[CustomEditor(typeof(MusicPlayer))]
public class MusicPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MusicPlayer player = (MusicPlayer)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Music Generation Controls", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate and Play"))
        {
            player.GenerateAndPlay();
        }

        if (GUILayout.Button("Stop"))
        {
            player.StopPlayback();
        }

        if (GUILayout.Button("Regenerate (New Seed)"))
        {
            player.Regenerate();
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "Click 'Generate and Play' to create and play procedural music based on the assigned MusicConfig. " +
            "Use 'Regenerate' to create variations with different random seeds.",
            MessageType.Info);
    }
}