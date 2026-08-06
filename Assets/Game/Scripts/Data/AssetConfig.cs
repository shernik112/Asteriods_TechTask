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

        public IEnumerable<AssetReferenceInfo> GetAllAssetReferences()
        {
            return new AssetReferenceInfo[]
            {
                new(AssetId.Player, player),
                new(AssetId.PlayerBullet, playerBullet),
                new(AssetId.PlayerLaser, playerLaser),

                new(AssetId.Asteroid, asteroidPrefab),
                new(AssetId.FragmentAsteroid, fragmentAsteroidPrefab),
                new(AssetId.Ufo, ufoPrefab),

                new(AssetId.AudioHandler, audioHandler),
                new(AssetId.Border, border),
                new(AssetId.PlacementBorder, placementBorder),
                new(AssetId.Transition, transition)
            };
            
        }
    }
    
    public enum AssetId
    {
        Player,
        PlayerBullet,
        PlayerLaser,

        Asteroid,
        FragmentAsteroid,
        Ufo,

        AudioHandler,
        Border,
        PlacementBorder,
        Transition
    }
}
