using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputDetector
{
    private readonly Dictionary<Type, IInputStrategy> _strategyMap;
    private IInputStrategy _currentStrategy;

    public InputDetector(IEnumerable<IInputStrategy> strategies)
    {
        _strategyMap = strategies.ToDictionary(s => s.GetType(), s => s);
    }

    public void InitializeStrategies(InputSettings settings)
    {
        foreach (IInputStrategy strategy in _strategyMap.Values)
        {
            strategy.Initialize(settings);
        }
    }

    public void DetectAndSwitchStrategy()
    {
        IInputStrategy newStrategy = FindBestStrategy();

        if (_currentStrategy != newStrategy)
        {
            _currentStrategy?.Disable();
            _currentStrategy = newStrategy;
            _currentStrategy?.Enable();
        }
    }

    private IInputStrategy FindBestStrategy()
    {
        GamepadInputStrategy gamepadStrategy = _strategyMap.Values.OfType<GamepadInputStrategy>().FirstOrDefault();
        if (gamepadStrategy?.IsDevicePresent() == true)
            return gamepadStrategy;

        KeyboardMouseInputStrategy keyboardStrategy = _strategyMap.Values.OfType<KeyboardMouseInputStrategy>().FirstOrDefault();
        if (keyboardStrategy?.IsDevicePresent() == true)
            return keyboardStrategy;

        MobileInputStrategy mobileStrategy = _strategyMap.Values.OfType<MobileInputStrategy>().FirstOrDefault();
        return mobileStrategy;
    }

    public PlayerInputData GetInput() => _currentStrategy?.GetInput() ?? default;
}

