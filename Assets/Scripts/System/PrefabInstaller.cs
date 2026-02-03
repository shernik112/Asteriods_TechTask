using System;
using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab = default;
    [SerializeField] private GameObject asteroidPrefab = default;
    [SerializeField] private GameObject ufoPrefab = default;
    [SerializeField] private GameObject bulletPrefab = default;
    [SerializeField] private Transform rootHandlers = default;
    [SerializeField] private RestartInvoke restartButton = default;
    [SerializeField] private HandlerScore handlerScore = default;

    private Type[] _singleBehaviours = new Type[]
    {
        typeof(EnemiesSpawner),
        typeof(BlockCursor),
        typeof(BulletPool),
        typeof(UFOPool),
        typeof(AsteroidPool),
        typeof(HandlerGameCondition),
        typeof(HandlerShootLaser)
    };
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy(); 
        
        Container.Bind<RestartInvoke>().FromInstance(restartButton).AsSingle().NonLazy();
        Container.Bind<HandlerScore>().FromInstance(handlerScore).AsSingle().NonLazy();
        
        Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletPool>();
        Container.Bind<GameObject>().FromInstance(asteroidPrefab).WhenInjectedInto<AsteroidPool>();
        Container.Bind<GameObject>().FromInstance(ufoPrefab).WhenInjectedInto<UFOPool>();
        for(var i = 0; i < _singleBehaviours.Length; i++)
        {
            var behaviour = _singleBehaviours[i];
            Container.Bind(behaviour).FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
                .AsSingle().NonLazy();
        }
        

    }

    public void InjectGo(GameObject obj)
    {
        Container.InjectGameObject(obj);
    }
}