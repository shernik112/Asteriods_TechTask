using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "LaserData", menuName = "Scriptable Objects/LaserData")]
    public class LaserData : ScriptableObject
    {
        public AudioClip newChargeClip;
        public AudioClip shootLaser;
    }
}
