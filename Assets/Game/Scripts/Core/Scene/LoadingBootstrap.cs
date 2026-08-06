using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.System
{
    public sealed class LoadingBootstrap : MonoBehaviour
    {
        private AssetConfig _assetConfig;
        private IAssetLoader _assetLoader;
        private AssetProvider _assetProvider;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(
            AssetConfig assetConfig,
            IAssetLoader assetLoader,
            ISceneLoader sceneLoader,
            AssetProvider assetProvider)
        {
            _assetConfig = assetConfig;
            _assetLoader = assetLoader;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
        }
        
        private void Start()
        {
            BootstrapAsync(destroyCancellationToken).Forget();
        }

        private async UniTask BootstrapAsync(CancellationToken ct)
        {
            await LoadAllAssetsAsync(ct);

            await _sceneLoader.LoadSceneAsync(_assetConfig.PlayScene, LoadSceneMode.Single, ct);
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
