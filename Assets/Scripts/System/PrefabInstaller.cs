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

    private Type[] _singleBehaviours = new Type[]
    {
        typeof(EnemiesSpawner),
        typeof(BlockCursor),
        typeof(BulletPool),
        typeof(UFOPool),
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
        Container.Bind<GameObject>().FromInstance(ufoPrefab).WhenInjectedInto<UFOPool>();

    }

    public void InjectGo(GameObject obj)
    {
        Container.InjectGameObject(obj);
    }
}