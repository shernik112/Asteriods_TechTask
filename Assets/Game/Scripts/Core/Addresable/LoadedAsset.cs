using UnityEngine;

namespace Project.System
{
    public class LoadedAsset
    {
        public readonly AssetId Id;
        public readonly GameObject Prefab;

        public LoadedAsset(
            AssetId id,
            GameObject prefab)
        {
            Id = id;
            Prefab = prefab;
        }
    }
}