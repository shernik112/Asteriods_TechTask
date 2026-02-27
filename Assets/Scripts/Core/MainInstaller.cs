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
        [SerializeField] private GameObject ufoPrefab = default;
        [SerializeField] private GameObject bulletPrefab = default;
        [SerializeField] private GameObject mainCanvas = default;
        [SerializeField] private GameObject mainCamera = default;
        [SerializeField] private GameObject eventBus = default;
        [SerializeField] private GameObject transition = default;
        [SerializeField] private Transform rootHandlers = default;
        [SerializeField] private MainAudio mainAudio = default;
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
            Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletShoot>();
            Container.Bind<GameObject>().WithId("Asteroid").FromInstance(asteroidPrefab).WhenInjectedInto<EnemiesSpawner>();
            Container.Bind<GameObject>().WithId("Ufo").FromInstance(ufoPrefab).WhenInjectedInto<EnemiesSpawner>();
        
            for (var i = 0; i < _singleBehaviours.Length; i++)
            {
                var behaviour = _singleBehaviours[i];
                Container.Bind(behaviour).FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
                    .AsSingle().NonLazy();
            }
            
            Container.Bind<MainAudio>().FromInstance(mainAudio).AsSingle();
            Container.Bind<MainInstaller>().FromInstance(this).AsSingle();
            Container.InstantiatePrefab(mainCanvas);
                Container.InstantiatePrefab(transition);
        }

        public void InjectGo(GameObject obj)
        {
            Container.InjectGameObject(obj);
        }
    }
}