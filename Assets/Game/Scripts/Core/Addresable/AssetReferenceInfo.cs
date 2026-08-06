using UnityEngine.AddressableAssets;

namespace Project.System
{
    public readonly struct AssetReferenceInfo
    {
        public readonly AssetId Id;
        public readonly AssetReferenceGameObject Reference;

        public AssetReferenceInfo(
            AssetId id,
            AssetReferenceGameObject reference)
        {
            Id = id;
            Reference = reference;
        }
    }
}