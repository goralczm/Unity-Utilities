using System.Collections.Generic;
using UnityEngine;

public abstract class SettingsInput : MonoBehaviour
{
    [field: SerializeField] public string SettingName { get; protected set; }

    public virtual void Setup() { }
    public abstract void ResetToDefault();
    public abstract object Save();
    public abstract void Load(object data);
}
