using UnityEngine;

public class PlayerShoot : ManagedBehaviour
{
    [SerializeField] private float cooldown = 0.2f;
    [SerializeField] private float instantiateOffset = 0.2f;
    [SerializeField] private GameObject bulletPrefab;
    private float _lastTime;
    private bool _mayShoot;
    public override void ManagedUpdate()
    {
        _lastTime += Time.deltaTime;
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && _lastTime >= cooldown)
        {
            Shoot();
            _lastTime = 0f;
        }
    }

    private void Shoot()
    {
        Vector2 spawnPos = (Vector2)transform.position + (Vector2)transform.up * instantiateOffset;
        var obj = Instantiate(bulletPrefab,spawnPos,transform.localRotation);
    }
}
