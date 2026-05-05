using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "LaserData", menuName = "Scriptable Objects/LaserData")]
    public class LaserData : ScriptableObject
    {
        [field: SerializeField] public AudioClip NewChargeClip { get; private set; }
        [field: SerializeField] public AudioClip ShootLaser { get; private set; }
        [field: SerializeField] public int DefaultCountShotLaser { get; private set; }
        [field: SerializeField] public float DurationLaserShot { get; private set; }
        [field: SerializeField] public float CooldownDuration { get; private set; }
    }
}