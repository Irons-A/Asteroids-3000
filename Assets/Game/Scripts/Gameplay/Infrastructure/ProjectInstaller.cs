using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ConfigService>().AsSingle();

        Container.Bind<InputDetector>().AsSingle();
        Container.Bind<IInputStrategy>().To<KeyboardMouseInputStrategy>().AsSingle();
        Container.Bind<IInputStrategy>().To<GamepadInputStrategy>().AsSingle();
        Container.Bind<IInputStrategy>().To<MobileInputStrategy>().AsSingle();

        Container.BindInterfacesAndSelfTo<InputSystem>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle();
    }
}
