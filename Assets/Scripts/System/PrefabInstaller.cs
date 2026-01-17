using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private GameObject asteroidPref;
    [SerializeField] private Transform rootHandlers;
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromComponentInNewPrefab(playerPref).AsSingle().NonLazy();

        Container.Bind<GameObject>().FromInstance(asteroidPref).WhenInjectedInto<AsteroidsSpawner>();
        Container.Bind<AsteroidsSpawner>().FromNewComponentOnNewGameObject().UnderTransform(rootHandlers)
            .AsSingle().NonLazy();
    }
}