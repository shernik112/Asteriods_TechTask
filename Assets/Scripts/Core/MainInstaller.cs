using Project.Player;
using Project.UI;
using UnityEngine;
using Zenject;
using System;

namespace Project.System
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerPrefab = default;
        [SerializeField] private GameObject asteroidPrefab = default;
        [SerializeField] private GameObject fragmentAsteroidPrefab = default;
        [SerializeField] private GameObject ufoPrefab = default;
        [SerializeField] private GameObject bulletPrefab = default;
        [SerializeField] private GameObject mainCamera = default;
        [SerializeField] private GameObject eventBus = default;
        [SerializeField] private GameObject mainAudio = default;
        [SerializeField] private Transform rootHandlers = default;
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
            Container.Bind<Camera>().FromComponentInNewPrefab(mainCamera).AsSingle().NonLazy();
            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();
            Container.Bind<MainAudio>().FromComponentInNewPrefab(mainAudio).AsSingle().NonLazy();
            
            Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletShoot>();
            
            Container.Bind<GameObject>().WithId("Asteroid").FromInstance(asteroidPrefab).WhenInjectedInto<EnemiesSpawner>();
            Container.Bind<GameObject>().WithId("FragmentAsteroid").FromInstance(fragmentAsteroidPrefab).WhenInjectedInto<EnemiesSpawner>();
            Container.Bind<GameObject>().WithId("Ufo").FromInstance(ufoPrefab).WhenInjectedInto<EnemiesSpawner>();
            
            foreach (var singleBehaviour in _singleBehaviours)
            {
                Container.Bind(singleBehaviour).FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
                    .AsSingle().NonLazy();
            }
            
            Container.Bind<MainInstaller>().FromInstance(this).AsSingle();
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
        public void InjectGo(GameObject obj)
        {
            Container.InjectGameObject(obj);
        }
    }
}