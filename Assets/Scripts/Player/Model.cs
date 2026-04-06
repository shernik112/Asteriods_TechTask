using UnityEngine;

namespace Project.Player
{
    public class Model<T> where T : ScriptableObject
    {
        public readonly T Data;

        protected Model(T data)
        {
            this.Data = data;
        }
        
    }
}