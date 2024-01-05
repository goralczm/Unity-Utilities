using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QualityInput : DropdownSetting
{
    public override void Setup()
    {
        List<string> options = QualitySettings.names.ToList();

        _dropdown.ClearOptions();
        _dropdown.AddOptions(options);
    }

    public void SetQualityLevel(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);

        OnValueChanged();
    }
}
