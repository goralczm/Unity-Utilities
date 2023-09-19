using UnityEngine;

/// <summary>
/// Class that instantiates the given initializator from Resources folder inside project structure, even before the scene is loaded.
/// Comes handy when you need to be sure, that for example singleton classes are present already before the scene is loaded.
/// </summary>
public static class Bootstrapper
{
    public const string INITIALIZATOR_NAME = "Systems";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute() => Object.Instantiate(Resources.Load(INITIALIZATOR_NAME));
}