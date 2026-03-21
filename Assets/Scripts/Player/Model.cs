using UnityEngine;

namespace Project.Player
{
    public abstract class Model<TData> : Model
    {
        public TData Data { get; private set; }
        
        protected bool IsActive =  true;

        public void Init(TData data)
        {
            Data = data;
        }
        
    }
    
    public class Model  : ScriptableObject
    {
    }
}