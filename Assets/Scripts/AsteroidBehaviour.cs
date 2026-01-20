using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AsteroidBehaviour : BaseEnemy
{
    private static bool _firstAstroidHit;
    [Inject] private AsteroidPool _pool;
    [SerializeField] private float createRotate;
    public float multiplierBoost;
    private MoveForward _moveForward;
    public int _sizeLevel = 1;
    private Vector3 _intialScale;
    public override void ManagedInintialize()
    {
        _moveForward = GetComponent<MoveForward>();
        _intialScale = transform.localScale;
    }

    public void InitParams(int size)
    {
        _sizeLevel = size;
        _moveForward._currentSpeed = _moveForward.defaultSpeed * multiplierBoost * size;
        transform.localScale =  _intialScale / size;
    }

    protected override void HitBullet()
    {
        if (_sizeLevel >= 3)
        {
            _pool.Return(gameObject); 
            return;
        }
       IniteAsteroid();
       IniteAsteroid();
       _pool.Return(gameObject);
    }

    private void IniteAsteroid()
    {   
        var randomRotate = Random.Range(-createRotate, createRotate);
        var obj = _pool.Get();
        obj.GetComponent<AsteroidBehaviour>().InitParams(_sizeLevel + 1);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, randomRotate);
    }
    
    protected override void HitLaser()
    {
    }

    public void SetDefaultParametrs()
    {
        _moveForward._currentSpeed = _moveForward.defaultSpeed;
        transform.localScale = Vector3.one;
        _sizeLevel = 1;
    }
}
