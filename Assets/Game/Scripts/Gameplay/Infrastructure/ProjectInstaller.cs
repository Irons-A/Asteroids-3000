using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Signal Bus
        SignalBusInstaller.Install(Container);

        // Config Service
        Container.BindInterfacesTo<ConfigService>().AsSingle();

        // Input System
        Container.Bind<InputDetector>().AsSingle();
        Container.Bind<IInputStrategy>().To<KeyboardMouseInputStrategy>().AsSingle();
        Container.Bind<IInputStrategy>().To<GamepadInputStrategy>().AsSingle();
        Container.Bind<IInputStrategy>().To<MobileInputStrategy>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputSystem>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

        // Player
        Container.Bind<PlayerModel>().AsSingle();
        Container.Bind<PlayerViewModel>().AsSingle();
        Container.Bind<LaserModel>().AsSingle();
        Container.Bind<LaserController>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();

        // Load settings from JSON
        BindSettings<InputSettings>("InputSettings");
        BindSettings<PlayerSettings>("PlayerSettings");
    }

    private void BindSettings<T>(string configName) where T : class
    {
        Container.Bind<T>()
            .FromMethod(context =>
                context.Container.Resolve<IConfigService>().LoadConfig<T>(configName))
            .AsSingle();
    }
}
