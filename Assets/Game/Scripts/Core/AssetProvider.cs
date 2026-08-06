using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Project.System
{
    [UsedImplicitly]
    public sealed class AssetProvider
    {
        private readonly Dictionary<AssetId, GameObject> _prefabs = new();

        public void  AddPrefab(LoadedAsset loadedAsset)
        {
            if (loadedAsset.Prefab == null)
                return;
            
            if (!_prefabs.TryAdd(loadedAsset.Id, loadedAsset.Prefab))
                throw new InvalidOperationException($"Asset {loadedAsset.Id} already saved.");
        }

        public GameObject GetPrefab(AssetId assetId)
        {
           if(_prefabs.TryGetValue(assetId, out GameObject prefab))
               return prefab;
           
           throw new InvalidOperationException(
               $"Asset {assetId} has not been loaded yet.");
        }
    }
}
