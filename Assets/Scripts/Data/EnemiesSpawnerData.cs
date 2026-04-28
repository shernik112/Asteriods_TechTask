using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "EnemiesSpawnerData", menuName = "Scriptable Objects/EnemiesSpawnerData")]
    public class EnemiesSpawnerData : ScriptableObject
    {
        [field: SerializeField] public GameObject FragmentAsteroidPrefab { get; private set; }
        [field: SerializeField] public GameObject AsteroidPrefab { get; private set; }
        [field: SerializeField] public GameObject UfoPrefab { get; private set; }
    }
}