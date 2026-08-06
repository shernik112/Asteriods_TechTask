using UnityEngine;
using Zenject;

namespace Project.System
{
    public sealed class LoadBootstrap : MonoBehaviour
    {
        [Inject]
        public void Construct()
        {
            
        }
        
        private void Start()
        {
        }
    }
}
