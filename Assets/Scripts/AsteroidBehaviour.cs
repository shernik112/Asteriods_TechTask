using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(MoveForward))]
public class AsteroidBehaviour : BaseEnemy<AsteroidPool>
{
    [SerializeField] private float multiplierBoost = default;
    [SerializeField] private int countStage = default;
    [field: SerializeField] 
    public override int CountScoreByDefeat { get; set; } = default;
    private readonly float _createRotate = 50f;
    private readonly float _lowerRotate = 20f;
    private MoveForward _moveForward;
    private int _sizeLevel = 1;
    private Vector3 _initialScale;
    
    private void Awake()
    {
        _moveForward = GetComponent<MoveForward>();
        _initialScale = transform.localScale;
    }
    
    public void InitParams(int size)
    {
        _sizeLevel = size;
        _moveForward.currentSpeed = _moveForward.defaultSpeed + multiplierBoost * size;
        transform.localScale =  _initialScale / size;
    }

    protected override void HitBullet()
    {
        if (_sizeLevel >= countStage)
        {
            Pool.ReturnToPool(gameObject); 
            return;
        }
       InitAsteroid(1);
       InitAsteroid(2);
       Pool.ReturnToPool(gameObject);
    }

    private void InitAsteroid(int side)
    {
        var mag = Random.Range(_lowerRotate, _createRotate);
        var randomRotate = side == 1 ? mag : -mag;
        var obj = Pool.Get();
        obj.GetComponent<AsteroidBehaviour>().InitParams(_sizeLevel + 1);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, randomRotate);
    }

    public void SetDefaultParameters()
    {
        if (_moveForward != null)
            _moveForward.currentSpeed = _moveForward.defaultSpeed;

        transform.localScale = _initialScale;
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
