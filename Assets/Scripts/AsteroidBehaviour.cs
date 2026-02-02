using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AsteroidBehaviour : BaseEnemy<AsteroidPool>
{
    [SerializeField] private float multiplierBoost = default;
    [SerializeField] private int countStage = default;
    private float _createRotate = 50f;
    private readonly float _lowerRotate = 20f;
    private MoveForward _moveForward;
    private int _sizeLevel = 1;
    private Vector3 _intialScale;
    public override void ManagedInintialize()
    {
        _moveForward = GetComponent<MoveForward>();
        _intialScale = transform.localScale;
    }

    public void InitParams(int size)
    {
        _sizeLevel = size;
        _moveForward._currentSpeed = _moveForward.defaultSpeed + multiplierBoost * size;
        transform.localScale =  _intialScale / size;
    }

    protected override void HitBullet()
    {
        if (_sizeLevel >= countStage)
        {
            Pool.Return(gameObject); 
            return;
        }
       IniteAsteroid(1);
       IniteAsteroid(2);
       Pool.Return(gameObject);
    }

    private void IniteAsteroid(int side)
    {
        var mag = Random.Range(_lowerRotate, _createRotate);
        var randomRotate = side == 1 ? mag : -mag;
        var obj = Pool.Get();
        obj.GetComponent<AsteroidBehaviour>().InitParams(_sizeLevel + 1);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, randomRotate);
    }
    
    protected override void HitLaser()
    {
    }

    public void SetDefaultParametrs()
    {
        if (_moveForward != null)
            _moveForward._currentSpeed = _moveForward.defaultSpeed;

        transform.localScale = _intialScale;
        transform.rotation = Quaternion.identity;
        _sizeLevel = 1;
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
