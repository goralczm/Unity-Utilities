using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Loads and parses CSV file containing localisation data.
/// </summary>
public class LocalisationCSVLoader
{
    private const string FILE_PATH = "Localisation/localisation";
    private const char LINE_SEPARATOR = '\n';
    private const char VALUES_SEPARATOR = ',';
    private const char SURROUND = '"';

    private TextAsset _csvFile;
    private string _fieldSeparator = $"{SURROUND}{VALUES_SEPARATOR}{SURROUND}";

    /// <summary>
    /// Loads the CSV file containing localisation data.
    /// </summary>
    public void LoadCSV()
    {
        _csvFile = Resources.Load<TextAsset>(FILE_PATH);
    }

    /// <summary>
    /// Parses the CSV file data and creates a dictionary of localised strings for the given language tag.
    /// </summary>
    /// <param name="languageTag">The language tag from the CSV file</param>
    /// <returns>
    /// A dictionary where keys are localisation keys and values are localised strings.
    /// </returns>
    public Dictionary<string, string> GetDictionaryValues(string languageTag)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = _csvFile.text.Split(LINE_SEPARATOR);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(_fieldSeparator, System.StringSplitOptions.None);

        for (int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(languageTag))
            {
                attributeIndex = i;
                break;
            }
        }

        Regex CSVParses = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            if (line.Length > 0 && line[0] == '#')
                continue;

            string[] fields = CSVParses.Split(line);

            for (int f = 0; f < fields.Length; f++)
            {
                fields[f] = fields[f].TrimStart(' ', SURROUND);
                fields[f] = fields[f].TrimEnd('\r');
                fields[f] = fields[f].TrimEnd(SURROUND);
            }

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key))
                    continue;

                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }
}
