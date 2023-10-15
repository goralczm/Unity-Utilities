using System.Collections.Generic;

[System.Serializable]
public class SavableData
{
    public Dictionary<string, object> savedDatas;

    public SavableData()
    {
        savedDatas = new Dictionary<string, object>();
    }

    public void SaveToFile(string fileName)
    {
        SaveSystem.SaveData(this, fileName);
    }

    public void SaveData(string key, object obj)
    {
        if (!savedDatas.ContainsKey(key))
        {
            savedDatas.Add(key, obj);
            return;
        }

        savedDatas[key] = obj;
    }

    public void LoadData()
    {
        
    }

    public object GetData(string key)
    {
        if (!savedDatas.ContainsKey(key))
            throw new KeyNotFoundException();

        return savedDatas[key];
    }
}
