using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public sealed class LoadingBootstrap : MonoBehaviour
    {
        private AssetConfig _assetConfig;
        private IAssetLoader _assetLoader;
        private AssetProvider _assetProvider;

        [Inject]
        public void Construct(
            AssetConfig assetConfig,
            IAssetLoader assetLoader,
            AssetProvider assetProvider)
        {
            _assetConfig = assetConfig;
            _assetLoader = assetLoader;
            _assetProvider = assetProvider;
        }
        
        private void Start()
        {
            LoadAllAssetsAsync(destroyCancellationToken).Forget();
        }

        private async UniTask LoadAllAssetsAsync(CancellationToken ct)
        {
            var tasks = new List<UniTask<LoadedAsset>>();
            
            foreach (var assetConfig in _assetConfig.GetAllAssetReferences())
            {
                tasks.Add(GetAssetAsync(assetConfig, ct));
            }
            
            var assets = await UniTask.WhenAll(tasks);
            
            foreach (var asset in assets)
                _assetProvider.AddPrefab(asset);
            
        }

        private async UniTask<LoadedAsset> GetAssetAsync(AssetReferenceInfo assetConfig, CancellationToken ct)
        {
            var prefab = await _assetLoader.LoadAssetAsync(assetConfig.Reference, ct);
            return new LoadedAsset(assetConfig.Id, prefab);
        }
    }
}
