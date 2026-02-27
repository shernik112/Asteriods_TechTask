using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class AssignCameraToCanvas : MonoBehaviour
    {
        [SerializeField] private string layerId; 
        private Camera _mainCamera;
        
        [Inject]
        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }
        
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = _mainCamera;
            canvas.sortingLayerID = SortingLayer.NameToID(layerId);
        }
    }
}
