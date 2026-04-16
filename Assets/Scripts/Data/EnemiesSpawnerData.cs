using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "EnemiesSpawnerData", menuName = "Scriptable Objects/EnemiesSpawnerData")]
    public class EnemiesSpawnerData : ScriptableObject
    {
        public GameObject fragmentAsteroidPrefab;
        public GameObject asteroidPrefab;
        public GameObject ufoPrefab;
    }
}
