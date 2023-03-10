using UnityEngine;
using Zenject;

public class SnakeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //////////////////////////////////
        // Common Bindings
        //////////////////////////////////
        Container.Bind<IGridService>()
            .To<GridService>()
            .AsSingle();

        Container.Bind<ILongTermStorage>()
            .To<PlayerPrefsStorage>()
            .AsSingle(); 

        Container.Bind<IScoreService>()
            .To<ScoreService>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<IHighScoreService>()
            .To<HighScoreService>()
            .AsSingle();

        Container.Bind<IGameManager>()
            .To<GameManager>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<IMenuManager>()
            .To<MenuManager>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container.BindFactory<UnityEngine.Object, SnakeBody, SnakeBody.Factory>()
            .FromFactory<PrefabFactory<SnakeBody>>();


        //////////////////////////////////
        // Platform Specific Bindings
        //////////////////////////////////
        Container.Bind<IVibratorService>()
                .To<NiceVibratorService>()
                .AsSingle();
    }
}