using UnityEngine;

namespace Project.Player
{
    public class Model<T> where T : ScriptableObject
    {
        public readonly T Data;
        
        protected bool IsActive =  true;

        protected Model(T data)
        {
            Data = data;
        }
        
    }
}