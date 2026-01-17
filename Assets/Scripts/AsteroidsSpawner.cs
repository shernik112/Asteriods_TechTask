using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AsteroidsSpawner : ManagedBehaviour
{
        [Inject]
        private GameObject _prefabAsteroid;

        private readonly float _rotateOffset = 30f;
        private readonly float _posOffset = 0.5f;

        private readonly Vector2 _lookTarget = new Vector2(0, 0);
        private float _halfHeight;
        private float _halfWidth;
        
        private void Start()
        {
                var cam = Camera.main;
                if (cam != null)
                {
                        _halfHeight = cam.orthographicSize;
                        _halfWidth = _halfHeight * cam.aspect;
                        _halfHeight += _posOffset;
                        _halfWidth += _posOffset;
                }
        }

        public void CreateAsteroid(int count)
        {
                SetPointCreate(count);
        }

        private void SetPointCreate(float count)
        {
                for (int i = 0; i < count; i++)
                {
                        var pos = GetRandomPos();
                        var obj = Instantiate(_prefabAsteroid);
                        obj.transform.position = pos;
                        RotateAsteroid(obj);
                }
        }

        private Vector2 GetRandomPos()
        {
                var side = Random.Range(0,4);
                return side switch
                {
                        0 => new Vector2(_halfWidth, Random.Range(-_halfHeight,_halfHeight)),
                        1 => new Vector2(-_halfWidth, Random.Range(-_halfHeight,_halfHeight)),
                        2 => new Vector2(Random.Range(-_halfWidth,_halfWidth),_halfHeight),
                        3 => new Vector2(Random.Range(-_halfWidth,_halfWidth),-_halfHeight),
                        _ => Vector2.zero
                };
        }

        private void RotateAsteroid(GameObject obj)
        {
                var direction = _lookTarget - (Vector2)obj.transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, angle + Random.Range(-_rotateOffset,_rotateOffset));
        }
}
