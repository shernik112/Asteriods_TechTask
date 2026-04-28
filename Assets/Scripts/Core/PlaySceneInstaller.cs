using Project.Player;
using Project.UI;
using UnityEngine;
using Zenject;
using System;

namespace Project.System
{
    public class PlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController playerPrefab = default;
        [SerializeField] private MainAudio mainAudio = default;
        [SerializeField] private GameObject transitionPrefab = default;
        [SerializeField] private GameObject bulletPrefab = default;
        [SerializeField] private GameObject borderPrefab = default;
        [SerializeField] private Camera mainCamera = default;
        [SerializeField] private EnemiesSpawnerData enemiesSpawnerData = default;
        [SerializeField] private GameObject[] prefabs;
        
        private readonly Type[] _singleBehaviours =
        {    
            typeof(PauseHandler),
            typeof(EnemiesSpawner),
            typeof(HandlerInput),
            typeof(HandlerScore),
        };

    
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerPrefab).AsSingle();
            Container.Bind<RestartButton>().FromComponentInNewPrefab(transitionPrefab).AsSingle();
            Container.Bind<MainAudio>().FromComponentInNewPrefab(mainAudio).AsSingle();

            Container.Bind<EnemiesSpawnerData>().FromInstance(enemiesSpawnerData);
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