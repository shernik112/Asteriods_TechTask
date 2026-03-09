using UnityEngine;

namespace Project.Player
{
    public class View<T> : MonoBehaviour
        where T : ScriptableObject
    {
        protected Model<T> Model;

        public void Init(Model<T> model)
        {
            Model = model;
        }
    }
}