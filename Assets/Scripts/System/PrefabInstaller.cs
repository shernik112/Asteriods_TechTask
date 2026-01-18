using System.ComponentModel;
using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform rootHandlers;
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();
        Container.Bind<GameObject>().FromInstance(asteroidPrefab).WhenInjectedInto<AsteroidsSpawner>();
        Container.Bind<AsteroidsSpawner>().FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
            .AsSingle().NonLazy();
        
        Container.Bind<GameObject>().FromInstance(bulletPrefab).WhenInjectedInto<BulletPool>();
        Container.Bind<BulletPool>().FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
            .AsSingle().NonLazy();
    }

    public void InjectGo(GameObject obj)
    {
        Container.InjectGameObject(obj);
    }
}