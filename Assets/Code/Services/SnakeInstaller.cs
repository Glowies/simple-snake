using UnityEngine;
using Zenject;

public class SnakeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGridService>()
            .To<GridService>()
            .AsSingle();

        Container.Bind<IScoreService>()
            .To<ScoreService>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<IGameManager>()
            .To<GameManager>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<IInputManager>()
            .To<InputManager>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<IMenuManager>()
            .To<MenuManager>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container.BindFactory<UnityEngine.Object, SnakeBody, SnakeBody.Factory>()
            .FromFactory<PrefabFactory<SnakeBody>>();
    }
}