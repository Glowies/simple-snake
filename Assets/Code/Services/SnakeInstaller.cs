using UnityEngine;
using Zenject;

public class SnakeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGridService>()
            .To<GridService>()
            .AsSingle();
    }
}