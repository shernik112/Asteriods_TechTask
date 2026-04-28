using Project.Player;
using Project.UI;
using UnityEngine;
using Zenject;
using System;

namespace Project.System
{
    public class PlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerPrefab = default;
        [SerializeField] private GameObject bulletPrefab = default;
        [SerializeField] private GameObject mainAudio = default;
        [SerializeField] private GameObject transitionPrefab = default;
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
            var rootPools = new GameObject("RootPools").transform;
            Container.Bind<Transform>().FromInstance(rootPools).AsSingle();
            Container.BindInstance(new ObjectPool(bulletPrefab, Container, rootPools));
  
            
            Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletShoot>();

            Container.Bind<GameObject>().WithId("Border").FromInstance(borderPrefab).WhenInjectedInto<PlacementBorder>();
            
            foreach (var singleBehaviour in _singleBehaviours)
            {
                Container.BindInterfacesAndSelfTo(singleBehaviour).AsSingle();
            }
            
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