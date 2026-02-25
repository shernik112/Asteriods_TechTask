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
        [SerializeField] private Camera mainCamera = default;
        // [SerializeField] private Transform mainCanvas = default;
        [SerializeField] private Transform rootHandlers = default;
        [SerializeField] private RestartButton restartButton = default;
        [SerializeField] private CountLaserShots countLaserShots = default;
        [SerializeField] private MainAudio mainAudio = default;
        [SerializeField] private GameObject[] uiPrefabs;
        
        private readonly Type[] _singleBehaviours =
        {
            typeof(EnemiesSpawner),
            typeof(HandlerInput),
            typeof(PauseHandler),
            typeof(HandlerScore)
        };

    
        public override void InstallBindings()
        {
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

            // foreach (var uiPrefab in uiPrefabs)
            // {
            //     var behaviour = uiPrefab.GetType();
            //     Container.Bind(behaviour).FromComponentInNewPrefab(uiPrefab).UnderTransform(rootHandlers)
            //         .AsSingle().NonLazy();
            // }

            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.Bind<RestartButton>().FromInstance(restartButton).AsSingle();
            Container.Bind<CountLaserShots>().FromInstance(countLaserShots).AsSingle();
            Container.Bind<MainAudio>().FromInstance(mainAudio).AsSingle();

            Container.Bind<MainInstaller>().FromInstance(this).AsSingle();
        }

        public void InjectGo(GameObject obj)
        {
            Container.InjectGameObject(obj);
        }
    }
}