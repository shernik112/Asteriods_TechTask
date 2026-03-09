using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
    public class BulletData : ScriptableObject
    {
        public float moveSpeed;
        public Sprite sprite;   
    }
}
