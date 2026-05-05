using UnityEngine;

namespace Project.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float MoveAcceleration { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
        [field: SerializeField] public float RotateAcceleration { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public AudioClip DestructionClip { get; private set; }
        [field: SerializeField] public AudioClip DashClip { get; private set; }
    }
}