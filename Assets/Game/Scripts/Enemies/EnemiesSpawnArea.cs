using Random = UnityEngine.Random;
using UnityEngine;

namespace Project.System
{
    public class EnemiesSpawnArea
    {
        private readonly Vector2 _lookTarget = Vector2.zero;

        private readonly EnemiesControllerData _data;
        private readonly float _halfHeight;
        private readonly float _halfWidth;

        public EnemiesSpawnArea(EnemiesControllerData data, Camera mainCamera)
        {
            _data = data;
            
            _halfHeight = mainCamera.orthographicSize + _data.DefaultPosOffset;
            _halfWidth = _halfHeight * mainCamera.aspect + _data.DefaultPosOffset;
        }

        public Vector2 GetRandomEdgePosition()
        {
            var side = Random.Range(0, 4);

            return side switch
            {
                0 => new Vector2(_halfWidth, Random.Range(-_halfHeight, _halfHeight)),
                1 => new Vector2(-_halfWidth, Random.Range(-_halfHeight, _halfHeight)),
                2 => new Vector2(Random.Range(-_halfWidth, _halfWidth), _halfHeight),
                3 => new Vector2(Random.Range(-_halfWidth, _halfWidth), -_halfHeight),
                _ => Vector2.zero
            };
        }

        public Quaternion GetAsteroidRotation(Vector2 position)
        {
            var direction = _lookTarget - position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(
                0f,
                0f,
                angle + Random.Range(-_data.RotateOffset, _data.RotateOffset));
        }
    }
}