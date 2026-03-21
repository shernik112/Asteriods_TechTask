using UnityEngine;

namespace Project.Player
{
    public abstract class Model<TData> : Model
    {
        public readonly TData Data;
        
        protected bool IsActive =  true;

        protected Model(TData data)
        {
            Data = data;
        }
        
    }
    public class Model  : ScriptableObject
    {
        
    }
}