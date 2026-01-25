using System;
using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform rootHandlers;

    private Type[] _singleBehaviours = new Type[]
    {
        typeof(AsteroidsSpawner),
        typeof(BlockCursor),
        typeof(BulletPool),
        typeof(AsteroidPool),
        typeof(HandlerGameCondition)
    };
    public override void InstallBindings()
    {
        for(var i = 0; i < _singleBehaviours.Length; i++)
        {
            var behaviour = _singleBehaviours[i];
            Container.Bind(behaviour).FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
                .AsSingle().NonLazy();
        }
        
        Container.Bind<CharacterController>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();
        
        Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletPool>();
        Container.Bind<GameObject>().FromInstance(asteroidPrefab).WhenInjectedInto<AsteroidPool>();
        
        // Container.Bind<AsteroidsSpawner>().FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
        //     .AsSingle().NonLazy();
        // Container.Bind<BlockCursor>().FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
        //     .AsSingle().NonLazy();
        //
        // Container.Bind<BulletPool>().FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
        //     .AsSingle().NonLazy();
        //
        // Container.Bind<AsteroidPool>().FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
        //     .AsSingle().NonLazy();
    }

    public void InjectGo(GameObject obj)
    {
        Container.InjectGameObject(obj);
    }
}