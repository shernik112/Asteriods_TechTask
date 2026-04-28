using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set;}
        [field: SerializeField] public Sprite Sprite { get; private set;}
    }
}
