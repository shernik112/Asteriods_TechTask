using Project.Player;
using Project.UI;
using UnityEngine;
using Zenject;
using System;

namespace Project.System
{
    public class PlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera = default;
        [SerializeField] private GameObject playerPrefab = default;
        [SerializeField] private GameObject asteroidPrefab = default;
        [SerializeField] private GameObject fragmentAsteroidPrefab = default;
        [SerializeField] private GameObject ufoPrefab = default;
        [SerializeField] private GameObject bulletPrefab = default;
        [SerializeField] private GameObject eventBus = default;
        [SerializeField] private GameObject mainAudio = default;
        [SerializeField] private GameObject[] prefabs;
        
        private readonly Type[] _singleBehaviours =
        {
            typeof(EnemiesSpawner),
            typeof(HandlerInput),
            typeof(PauseHandler),
            typeof(HandlerScore)
        };

    
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().FromComponentInNewPrefab(eventBus).AsSingle().NonLazy();
            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();
            Container.Bind<MainAudio>().FromComponentInNewPrefab(mainAudio).AsSingle().NonLazy();
            
            Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletShoot>();
            
            Container.Bind<GameObject>().WithId("Asteroid").FromInstance(asteroidPrefab).WhenInjectedInto<EnemiesSpawner>();
            Container.Bind<GameObject>().WithId("FragmentAsteroid").FromInstance(fragmentAsteroidPrefab).WhenInjectedInto<EnemiesSpawner>();
            Container.Bind<GameObject>().WithId("Ufo").FromInstance(ufoPrefab).WhenInjectedInto<EnemiesSpawner>();
            
            foreach (var singleBehaviour in _singleBehaviours)
                Container.Bind(singleBehaviour).FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.Bind<PlaySceneInstaller>().FromInstance(this).AsSingle();
        }

        public override void Start()
        {
            foreach (var prefab in prefabs)
            {
                Container.InstantiatePrefab(prefab);
            }
        }

        public GameObject Instantiate(GameObject prefab, Transform parentTransform)
        {
            var go = Container.InstantiatePrefab(prefab, parentTransform);
            return go;
        }
    }
}