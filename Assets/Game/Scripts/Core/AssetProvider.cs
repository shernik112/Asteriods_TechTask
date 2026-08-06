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

        public void  AddPrefab(AssetId assetId, GameObject prefab)
        {
            if (prefab == null)
                return;
            
            if (!_prefabs.TryAdd(assetId, prefab))
                throw new InvalidOperationException($"Asset {assetId} already saved.");
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
