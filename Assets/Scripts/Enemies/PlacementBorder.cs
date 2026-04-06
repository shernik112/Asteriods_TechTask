using Project.Scene;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class PlacementBorder : MonoBehaviour
    {
        [SerializeField] private GameObject borderPrefab;
        
        private readonly float _placementOffset = 1f;
        private Camera _mainCamera;
        private float _heightCreate;
        private float _widthCreate;
        
        [Inject]
        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        private void Awake()
        {
            _heightCreate = _mainCamera.orthographicSize;
            _widthCreate = _heightCreate * _mainCamera.aspect;
            _heightCreate += _placementOffset;
            _widthCreate += _placementOffset;
        }

        private void Start()
        {
            for (var i = 1; i <= 4; i++)
            {
                var obj = Instantiate(borderPrefab);
                obj.transform.SetParent(transform); 
                
                switch (i)
                {
                    case 1:
                        obj.transform.position = new Vector2(_widthCreate, 0);
                        obj.transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case 2:
                        obj.transform.position = new Vector2(-_widthCreate, 0);
                        obj.transform.rotation = Quaternion.Euler(0, 0, 90);    
                        break;
                    case 3:
                        obj.transform.position = new Vector2(0, _heightCreate);
                        break;
                    case 4:
                        obj.transform.position = new Vector2(0, -_heightCreate);
                        break;
                }
                obj.AddComponent<TeleportBorder>();
            }
        }
    }
}