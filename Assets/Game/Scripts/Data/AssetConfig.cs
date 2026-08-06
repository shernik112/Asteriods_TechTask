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
        [field: SerializeField] private AssetReferenceGameObject bullet;
        [field: SerializeField] private AssetReferenceGameObject laser;

        [Header("Enemies")]
        [field: SerializeField] private AssetReferenceGameObject asteroidPrefab;
        [field: SerializeField] private AssetReferenceGameObject fragmentAsteroidPrefab;
        [field: SerializeField] private AssetReferenceGameObject ufoPrefab;

        [Header("Scene")]
        [field: SerializeField] private AssetReferenceGameObject audioHandler;
        [field: SerializeField] private AssetReferenceGameObject border;
        [field: SerializeField] private AssetReferenceGameObject placementBorder;
        [field: SerializeField] private AssetReferenceGameObject transition;
        
        [Header("PlayScene")]
        [field: SerializeField] public AssetReference PlayScene { get; private set;}

        public IEnumerable<AssetReferenceInfo> GetAllAssetReferences()
        {
            return new AssetReferenceInfo[]
            {
                new(AssetId.Player, player),
                new(AssetId.Bullet, bullet),
                new(AssetId.Laser, laser),

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
        Bullet,
        Laser,

        Asteroid,
        FragmentAsteroid,
        Ufo,

        AudioHandler,
        Border,
        PlacementBorder,
        Transition
    }
}
