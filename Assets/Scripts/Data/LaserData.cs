using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "LaserData", menuName = "Scriptable Objects/LaserData")]
    public class LaserData : ScriptableObject
    {
        public Sprite laserSprite;
        public AudioClip newChargeClip;
        public AudioClip shootLaser;
        public readonly int DefaultCountShotLaser = 3;
        public readonly float RechargeDuration = 12f;
        public readonly float DurationLaserShot = 0.4f;
        public readonly float CooldownDuration = 0.5f;
    }
}
