using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputSystem : MonoBehaviour, ITickable
{
    private const string InputSettingsPath = "InputSettings";

    private InputDetector _inputDetector;
    private IPlayerControllable _playerController;
    private bool _isInitialized;

    [Inject]
    public void Construct(InputDetector inputDetector, IConfigService configService)
    {
        _inputDetector = inputDetector;

        var inputSettings = configService.LoadConfig<InputSettings>(InputSettingsPath);
        _inputDetector.InitializeStrategies(inputSettings);

        _isInitialized = true;
    }

    public void SetPlayerController(IPlayerControllable playerController)
    {
        _playerController = playerController;
    }

    public void Tick()
    {
        if (!_isInitialized || _playerController == null) return;

        _inputDetector.DetectAndSwitchStrategy();

        PlayerInputData input = _inputDetector.GetInput();
        ApplyInputToPlayer(input);
    }

    private void ApplyInputToPlayer(PlayerInputData input)
    {
        if (input.RotationDirection != Vector2.zero)
        {
            _playerController.Rotate(input.RotationDirection);
        }

        switch (input.Movement)
        {
            case MovementState.Accelerating:
                {
                    _playerController.Accelerate();
                    break;
                }
            case MovementState.Decelerating:
                {
                    _playerController.Decelerate();
                    break;
                }
            case MovementState.None:
                {
                    _playerController.StopMovement();
                    break;
                }
        }

        if (input.ShootBullet)
        {
            _playerController.ShootBullet();
        }

        if (input.ShootLaser)
        {
            _playerController.ShootLaser();
        }

        if (input.ToggleMenu)
        {
            _playerController.ToggleMenu();
        }
    }
}
