using UnityEngine;

/// <summary>
/// Controls the language used by the <see cref="LocalisationSystem"/> based on the assigned language field.
/// </summary>
public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private Language _language;

    private void Start()
    {
        LocalisationSystem.ChangeLanguage(Language.English);
    }

    private void Update()
    {
        if (LocalisationSystem.CurrentLanguage != _language)
            LocalisationSystem.ChangeLanguage(_language);
    }
}
