using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour
{
    [SerializeField] private LocalisedString _localisedString;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        LocalisationSystem.OnLanguageChanged += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        LocalisationSystem.OnLanguageChanged -= UpdateText;
    }

    private void UpdateText()
    {
        _text.SetText(_localisedString.value);
    }
}
