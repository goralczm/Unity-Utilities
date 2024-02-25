/// <summary>
/// Represents a localised string that can be resolved using the <see cref="LocalisationSystem"/>.
/// </summary>
[System.Serializable]
public struct LocalisedString
{
    /// <summary>
    /// The key used to identify the localised string
    /// </summary>
    public string key;

    /// <summary>
    /// Initializes a new instance of the Localised String with the specific localisation key.
    /// </summary>
    /// <param name="key">The key used to indentify the localised string.</param>
    public LocalisedString(string key)
    {
        this.key = key;
    }

    /// <summary>
    /// Gets the localised value associated with the <see cref="key"/>
    /// </summary>
    public string value => LocalisationSystem.GetLocalisedValue(key);

    /// <summary>
    /// Implicitly converts a string to a <see cref="LocalisedString"/>.
    /// </summary>
    /// <param name="key">The key used to identify the localized string.</param>
    /// <returns>A new instance of <see cref="LocalisedString"/> initialized with the specified key.</returns>
    public static implicit operator LocalisedString(string key)
    {
        return new LocalisedString(key);
    }
}
