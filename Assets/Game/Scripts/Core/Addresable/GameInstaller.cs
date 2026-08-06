
using Project.System;
using UnityEngine;
using Zenject;

public sealed class GameInstaller : MonoInstaller
{
    [SerializeField] private AssetConfig _assetConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<AssetConfig>().FromInstance(_assetConfig).AsSingle();
        Container.Bind<AssetProvider>().AsSingle();
        Container.Bind<IAssetLoader>().To<AssetLoader>().AsSingle();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }
}