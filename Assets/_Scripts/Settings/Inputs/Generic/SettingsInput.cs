using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingsInput : MonoBehaviour
{
    [HideInInspector] public Settings settings;

    [field: SerializeField] public string SettingName { get; protected set; }
    [SerializeField] private bool _confirmation;

    protected Stack<object> _valueHistory = new Stack<object>();

    public virtual void Setup() { }
    public abstract void ResetToDefault();
    public abstract object Save();
    public abstract void Load(object data);

    public virtual void RevertLast()
    {
        _valueHistory.Pop(); // Skip first element, to get last value before confirmation

        if (_confirmation)
            settings.ConfirmSetting();
    }

    public void OnValueChanged()
    {
        if (_confirmation)
            NotifyConfirmation();

        SaveLastValue();
    }

    private void SaveLastValue()
    {
        _valueHistory.Push(Save());
    }

    public void NotifyConfirmation()
    {
        settings.ShowConfirmation(this);
    }
}
