using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.System
{
    [CreateAssetMenu(fileName = "AssetConfig", menuName = "Scriptable Objects/AssetConfig")]
    public class AssetConfig : ScriptableObject
    {
        [Header("Player")]
        [field: SerializeField] private AssetReferenceGameObject player;
        [field: SerializeField] private AssetReferenceGameObject playerBullet;
        [field: SerializeField] private AssetReferenceGameObject playerLaser;

        [Header("Enemies")]
        [field: SerializeField] private AssetReferenceGameObject asteroidPrefab;
        [field: SerializeField] private AssetReferenceGameObject fragmentAsteroidPrefab;
        [field: SerializeField] private AssetReferenceGameObject ufoPrefab;

        [Header("Scene")]
        [field: SerializeField] private AssetReferenceGameObject audioHandler;
        [field: SerializeField] private AssetReferenceGameObject border;
        [field: SerializeField] private AssetReferenceGameObject placementBorder;
        [field: SerializeField] private AssetReferenceGameObject transition;

        public IEnumerable<AssetReferenceGameObject> GetAllAssetReferences()
        {
            return new[]
            {
                player,
                playerBullet,
                playerLaser,
                asteroidPrefab,
                fragmentAsteroidPrefab,
                ufoPrefab,
                audioHandler,
                border,
                placementBorder,
                transition
            };
        }
    }
}
