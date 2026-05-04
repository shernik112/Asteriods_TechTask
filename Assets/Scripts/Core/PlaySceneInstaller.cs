using Project.Player;
using Project.UI;
using UnityEngine;
using Zenject;
using System;

namespace Project.System
{
    public class PlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController playerPrefab = null;
        [SerializeField] private MainAudio mainAudio = null;
        [SerializeField] private GameObject transitionPrefab = null;
        [SerializeField] private GameObject bulletPrefab = null;
        [SerializeField] private GameObject borderPrefab = null;
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] private EnemiesControllerData enemiesControllerData = null;
        [SerializeField] private LaserData laserData = null;
        [SerializeField] private PlayerData playerData = null;
        
        [SerializeField] private GameObject[] prefabs;
        
        private readonly Type[] _singleBehaviours =
        {    
            typeof(PauseHandler),
            typeof(EnemiesController),
            typeof(HandlerInput),
            typeof(HandlerScore),
        };

    
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerPrefab).AsSingle();
            Container.Bind<RestartButton>().FromComponentInNewPrefab(transitionPrefab).AsSingle();
            Container.Bind<MainAudio>().FromComponentInNewPrefab(mainAudio).AsSingle();

            Container.Bind<EnemiesControllerData>().FromInstance(enemiesControllerData);
            Container.Bind<LaserData>().FromInstance(laserData);
            Container.Bind<PlayerData>().FromInstance(playerData);
            
            Container.Bind<Transform>().FromInstance(transform).AsSingle();
            Container.BindInstance(new ObjectPool(bulletPrefab, Container, transform));
            Container.BindInterfacesAndSelfTo<ObjectPool>().WithArguments(bulletPrefab, transform).WhenInjectedInto<BulletShoot>();
  
            
            Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletShoot>();
            Container.Bind<GameObject>().FromInstance(borderPrefab).WhenInjectedInto<PlacementBorder>();
            
            foreach (var singleBehaviour in _singleBehaviours)
                Container.BindInterfacesAndSelfTo(singleBehaviour).AsSingle();
            
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