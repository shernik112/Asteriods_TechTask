using UnityEngine;
using Zenject;

namespace Project.System
{
    public sealed class LoadBootstrap : MonoBehaviour
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
        }
    }
}
