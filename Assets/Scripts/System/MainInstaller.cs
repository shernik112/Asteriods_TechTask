using System;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab = default;
    [SerializeField] private GameObject asteroidPrefab = default;
    [SerializeField] private GameObject ufoPrefab = default;
    [SerializeField] private GameObject bulletPrefab = default;
    [SerializeField] private Transform rootHandlers = default;
    [SerializeField] private RestartInvoke restartButton = default;
    [SerializeField] private HandlerScore handlerScore = default;
    [SerializeField] private CountLaserShots countLaserShots = default;
    [SerializeField] private FinalScore finalScore = default;

    private readonly Type[] _singleBehaviours =
    {
        typeof(EnemiesSpawner),
        typeof(BlockCursor),
        typeof(BulletPool),
        typeof(UfoPool),
        typeof(AsteroidPool),
        typeof(HandlerGameCondition),
        typeof(HandlerInput)
    };
    
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();

        Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletPool>();
        Container.Bind<GameObject>().FromInstance(asteroidPrefab).WhenInjectedInto<AsteroidPool>();
        Container.Bind<GameObject>().FromInstance(ufoPrefab).WhenInjectedInto<UfoPool>();
        
        for (var i = 0; i < _singleBehaviours.Length; i++)
        {
            var behaviour = _singleBehaviours[i];
            Container.Bind(behaviour).FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
                .AsSingle().NonLazy();
        }
        
        Container.Bind<RestartInvoke>().FromInstance(restartButton).AsSingle();
        Container.Bind<CountLaserShots>().FromInstance(countLaserShots).AsSingle();
        Container.Bind<HandlerScore>().FromInstance(handlerScore).AsSingle();
        Container.Bind<FinalScore>().FromInstance(finalScore).AsSingle();
    }

    public void InjectGo(GameObject obj)
    {
        Container.InjectGameObject(obj);
    }
}