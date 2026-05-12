using System;
using UnityEngine;

namespace Project.UI
{
    public class RecordScore
    {
        private readonly string _key;
        
        private string _jsonRecord;
        private readonly RecordData _recordData;
        
        public RecordScore(string key,int record)
        {
            _key = key;
            _recordData = new RecordData(record);
        }
        
        public int Record
        {
            get
            {
                var json = PlayerPrefs.GetString(_key, _recordData.record.ToString());
                return JsonUtility.FromJson<RecordData>(json).record;
            }
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
        
        public RecordData(int record)
        {
            this.record = record;
        }
    }
}
