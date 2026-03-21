using Project.Player;
using UnityEngine;

namespace Project
{
    public abstract class Controller<TModel> : MonoBehaviour
        where TModel : Model
    {
        protected TModel Model { get; private set; }

        protected virtual void Awake()
        {
            Model = CreateModel();
        }

        protected abstract TModel CreateModel();
    }
}
