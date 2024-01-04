using UnityEngine;
using TMPro;

public class ValueText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public virtual void SetStringValue(string value)
    {
        _text.SetText(value);
    }

    public virtual void SetFloatValue(float value)
    {
        _text.SetText(value.ToString());
    }

    public virtual void SetIntValue(float value)
    {
        _text.SetText(((int)value).ToString());
    }
}
