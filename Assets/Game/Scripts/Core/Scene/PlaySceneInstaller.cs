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
        [SerializeField] private PlayerController playerPrefab;
        [SerializeField] private AudioHandler audioHandler;
        [SerializeField] private GameObject transitionPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject borderPrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private EnemiesControllerData enemiesControllerData;
        [SerializeField] private LaserData laserData;
        [SerializeField] private PlayerData playerData;
        
        [SerializeField] private GameObject[] prefabs;
    
        public override void InstallBindings()
        {
            Container.
                Bind(typeof(PlayerController), typeof(PlayerMover),
                    typeof(PlayerDeathHandler), typeof(BulletShoot), typeof(ShootLaser))
                .FromComponentInNewPrefab(playerPrefab).AsSingle();
            
            Container.Bind<RestartButton>().FromComponentInNewPrefab(transitionPrefab).AsSingle();
            Container.Bind<AudioHandler>().FromComponentInNewPrefab(audioHandler).AsSingle();

            Container.Bind<EnemiesControllerData>().FromInstance(enemiesControllerData);
            Container.Bind<LaserData>().FromInstance(laserData);
            Container.Bind<PlayerData>().FromInstance(playerData);
            
            Container.Bind<Transform>().FromInstance(transform).AsSingle();
            Container.BindInterfacesAndSelfTo<ObjectPool>().WithArguments(bulletPrefab, transform).WhenInjectedInto<BulletShoot>();
  
            
            Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletShoot>();
            Container.Bind<GameObject>().FromInstance(borderPrefab).WhenInjectedInto<PlacementBorder>();
            
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesController>().AsSingle();
            Container.BindInterfacesAndSelfTo<HandlerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<HandlerScore>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsHandler>().AsSingle();
            
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        }

        public override void Start()
        {
            foreach (var prefab in prefabs)
            {
                Container.InstantiatePrefab(prefab);
            }
        }
    }
}