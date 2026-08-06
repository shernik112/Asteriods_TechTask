using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.System
{
    public interface IAssetLoader
    {
        UniTask<GameObject> LoadAssetAsync(
            AssetReferenceGameObject assetReference,
            CancellationToken token);

        void ReleaseAll();
    }
}