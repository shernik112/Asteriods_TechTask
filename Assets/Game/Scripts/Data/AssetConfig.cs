using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.System
{
    [CreateAssetMenu(fileName = "AssetConfig", menuName = "Scriptable Objects/AssetConfig")]
    public class AssetConfig : ScriptableObject
    {
        [field: SerializeField]  public PlayerAssets Player {get; private set;}
        [field: SerializeField]  public EnemyAssets Enemies {get; private set;}
        [field: SerializeField]  public SceneAssets Scene {get; private set;}
    }

    [Serializable]
    public class PlayerAssets
    {
        [field: SerializeField]  public AssetReferenceGameObject Player {get; private set;}
        [field: SerializeField]  public AssetReferenceGameObject PlayerBullet {get; private set;}
        [field: SerializeField]  public AssetReferenceGameObject PlayerLaser {get; private set;}
    }

    [Serializable]
    public class EnemyAssets
    {
        [field: SerializeField] public AssetReferenceGameObject AsteroidPrefab {get; private set;}
        [field: SerializeField] public AssetReferenceGameObject FragmentAsteroidPrefab {get; private set;}
        [field: SerializeField] public AssetReferenceGameObject UfoPrefab {get; private set;}
    }

    [Serializable]
    public class SceneAssets
    {
        [field: SerializeField]  public AssetReferenceGameObject AudioHandler {get; private set;}
        [field: SerializeField]  public AssetReferenceGameObject Border {get; private set;}
        [field: SerializeField]  public AssetReferenceGameObject PlacementBorder {get; private set;}
        [field: SerializeField]  public AssetReferenceGameObject Transition {get; private set;}
    }
}
