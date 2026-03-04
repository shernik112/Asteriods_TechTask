using UnityEngine;

namespace Project.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public float moveSpeed;
        public float moveAcceleration;
        public float rotateSpeed;
        public float rotateAcceleration;
        public AudioClip destructionClip;
        public AudioClip dashClip;
    }
}
