using UnityEngine;

namespace Project.Enemies
{
    [CreateAssetMenu(fileName = "EnemyDefinition", menuName = "Scriptable Objects/EnemyDefinition")]
    public class EnemyDefinition : ScriptableObject
    {
        public Sprite sprite;
        public Sprite hitSprite;
        public AudioClip hitClip;
    }
}
