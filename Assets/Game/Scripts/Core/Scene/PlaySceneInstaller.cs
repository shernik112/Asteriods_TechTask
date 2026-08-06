using Project.Player;
using Project.System.Analytics;
using Project.System.Analytics.Firebase;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class PlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private EnemiesControllerData enemiesControllerData;
        [SerializeField] private LaserData laserData;
        [SerializeField] private PlayerData playerData;

        private AssetProvider _assetProvider;
        
        [Inject]
        public void Construct(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public override void InstallBindings()
        {
            var bulletPrefab = _assetProvider.GetPrefab(AssetId.Bullet);
            
            Container.
                Bind(typeof(PlayerController), typeof(PlayerMover),
                    typeof(PlayerDeathHandler), typeof(BulletShoot), typeof(ShootLaser))
                .FromComponentInNewPrefab(_assetProvider.GetPrefab(AssetId.Player)).AsSingle();
            
            Container.Bind<RestartButton>().FromComponentInNewPrefab(_assetProvider.GetPrefab(AssetId.Transition)).AsSingle();
            Container.Bind<AudioHandler>().FromComponentInNewPrefab(_assetProvider.GetPrefab(AssetId.AudioHandler)).AsSingle();

            Container.Bind<EnemiesControllerData>().FromInstance(enemiesControllerData);
            Container.Bind<LaserData>().FromInstance(laserData);
            Container.Bind<PlayerData>().FromInstance(playerData);
            
            Container.Bind<Transform>().FromInstance(transform).WhenInjectedInto<EnemiesController>();
            Container.BindInterfacesAndSelfTo<ObjectPool>().AsSingle().WithArguments(bulletPrefab, transform).WhenInjectedInto<BulletShoot>();
  
            
            Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletShoot>();
            Container.Bind<GameObject>().FromInstance(_assetProvider.GetPrefab(AssetId.Border)).WhenInjectedInto<PlacementBorder>();

            Container.Bind<ISaveService>().To<SaveService>().AsTransient();
            Container.Bind<BlockCursor>().AsTransient();
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesController>().AsSingle();
            Container.BindInterfacesAndSelfTo<HandlerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<HandlerScore>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsHandler>().AsSingle();
            
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();

            Container.Bind<PlacementBorder>().FromComponentInNewPrefab(_assetProvider.GetPrefab(AssetId.PlacementBorder)).AsSingle().NonLazy();
        }
    }
}