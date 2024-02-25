using System;
using System.Collections.Generic;

/// <summary>
/// Enum representing available languages.
/// </summary>
public enum Language
{
    English,
    Polish
}

/// <summary>
/// Handles localisation functionality for multiple languages.
/// </summary>
public class LocalisationSystem
{
    public static Language CurrentLanguage { get; private set; } = Language.English;

    private static Dictionary<Language, Dictionary<string, string>> _localisedTexts = new Dictionary<Language, Dictionary<string, string>>();
    private static bool _isInitialized;
    public static LocalisationCSVLoader csvLoader;

    /// <summary>
    /// Action triggered whenever the language is changed.
    /// </summary>
    public static Action OnLanguageChanged;

    /// <summary>
    /// Changes the <see cref="CurrentLanguage"/>
    /// </summary>
    /// <param name="language">The new language to set.</param>
    public static void ChangeLanguage(Language language)
    {
        if (CurrentLanguage == language)
            return;

        CurrentLanguage = language;
        OnLanguageChanged?.Invoke();
    }

    /// <summary>
    /// Initializes the Localisation System with localised values loaded by the <see cref="LocalisationCSVLoader"/>.
    /// </summary>
    public static void Init()
    {
        csvLoader = new LocalisationCSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionaries();

        _isInitialized = true;
    }

    /// <summary>
    /// Updates the internal dictionaries with localised texts for each language.
    /// </summary>
    public static void UpdateDictionaries()
    {
        UpdateSingleLanguage(Language.English, "en");
        UpdateSingleLanguage(Language.Polish, "pl");
    }

    /// <summary>
    /// Updates the dictionary with localised string for a single language.
    /// </summary>
    /// <param name="language">The language to update.</param>
    /// <param name="tag">The CSV file language tag.</param>
    private static void UpdateSingleLanguage(Language language, string tag)
    {
        if (!_localisedTexts.ContainsKey(language))
            _localisedTexts.Add(language, csvLoader.GetDictionaryValues(tag));
        else
            _localisedTexts[language] = csvLoader.GetDictionaryValues(tag);
    }

    /// <summary>
    /// Retrevies the localised value for a given key based on the current language.
    /// </summary>
    /// <param name="key">The localisation key from CSV file</param>
    /// <returns>The localised value corresponding to the key, or an empty string if not found.</returns>
    public static string GetLocalisedValue(string key)
    {
        if (!_isInitialized)
            Init();

        string value = "";
        _localisedTexts[CurrentLanguage].TryGetValue(key, out value);

        return value;
    }
}
