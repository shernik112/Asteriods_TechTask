using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.System
{
    public class AssetProvider
    {
        public readonly IEnumerable<AssetReferenceGameObject> AssetPrefabs;
        
        public AssetProvider(IEnumerable<AssetReferenceGameObject> assetPrefabs)
        {
            AssetPrefabs = assetPrefabs;
        }
        
        

    }
}
