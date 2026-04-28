using UnityEngine;

namespace Project.Enemies
{
    [CreateAssetMenu(fileName = "EnemyDefinition", menuName = "Scriptable Objects/EnemyDefinition")]
    public class EnemyDefinition : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int ScoreByHit { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Sprite HitSprite { get; private set; }
        [field: SerializeField] public AudioClip HitClip { get; private set; }
    }
}