using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "LaserData", menuName = "Scriptable Objects/LaserData")]
    public class LaserData : ScriptableObject
    {
        public AudioClip newChargeClip;
        public AudioClip shootLaser;
        public int defaultCountShotLaser;
        public float durationLaserShot;
        public float cooldownDuration;
    }
}
