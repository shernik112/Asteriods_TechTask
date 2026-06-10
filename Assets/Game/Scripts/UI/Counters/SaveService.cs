using System;
using UnityEngine;

namespace Project.UI
{
    public class SaveService
    {
        public void Save(string key, int value)
        {
            var data = new SaveData { value = value };
            PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
        }

        public int Load(string key)
        {
            var json = PlayerPrefs.GetString(key, JsonUtility.ToJson(new SaveData()));
            return JsonUtility.FromJson<SaveData>(json).value;
        }
    }

    [Serializable]
    public class SaveData
    {
        public int value;
    }
}
