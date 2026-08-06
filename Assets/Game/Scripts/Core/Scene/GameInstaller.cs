
using Project.System;
using Zenject;

public sealed class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AssetProvider>().AsSingle();
        Container.Bind<IAssetLoader>().To<AssetLoader>().AsSingle();
        // Container.Bind<AssetProvider>().WhenInjectedInto<>()
    }
}