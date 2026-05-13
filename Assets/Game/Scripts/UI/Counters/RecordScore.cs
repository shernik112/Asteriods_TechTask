using System;
using UnityEngine;

namespace Project.UI
{
    public class RecordScore
    {
        private readonly string _key;
        
        private string _jsonRecord;
        private readonly RecordData _recordData;
        
        public RecordScore(string key)
        {
            _key = key;
            _recordData = new RecordData();
            
            var json = PlayerPrefs.GetString(_key, JsonUtility.ToJson(_recordData));
            _recordData.record = JsonUtility.FromJson<RecordData>(json).record;
        }
        
        public int Record
        {
            get => 
                _recordData.record;
            set
            {
                if (value <= _recordData.record) 
                    return;
                
                _recordData.record = value;
                _jsonRecord = JsonUtility.ToJson(_recordData);
                PlayerPrefs.SetString(_key, _jsonRecord);
            }
        }

    }

    [Serializable]
    public class RecordData
    {
        public int record;
    }
}
