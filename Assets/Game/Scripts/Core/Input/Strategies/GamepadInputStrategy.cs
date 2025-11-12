using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamepadInputStrategy : IInputStrategy
{
    private GamepadSettings _settings;
    private bool _isEnabled = true;

    private const string RightStickHorizontalAxis = "RightStickHorizontal";
    private const string RightStickVerticalAxis = "RightStickVertical";

    private const float TriggerPressThreshold = 0.1f;

    public void Initialize(InputSettings settings)
    {
        _settings = settings.Gamepad;
    }

    public PlayerInputData GetInput()
    {
        if (!_isEnabled) return default;

        return new PlayerInputData
        {
            RotationDirection = GetRotationDirection(),
            Movement = GetMovementState(),
            ShootBullet = Input.GetButtonDown(_settings.ShootBulletButton),
            ShootLaser = Input.GetButtonDown(_settings.ShootLaserButton),
            ToggleMenu = Input.GetButtonDown(_settings.MenuButton)
        };
    }

    private Vector2 GetRotationDirection()
    {
        Vector2 rightStick = new Vector2(Input.GetAxis(RightStickHorizontalAxis),
            Input.GetAxis(RightStickVerticalAxis));

        if (rightStick.magnitude < _settings.StickDeadZone)
        {
            return Vector2.zero;
        }

        return rightStick.normalized * _settings.StickSensitivity;
    }

    private MovementState GetMovementState()
    {
        float accelerateAxis = Input.GetAxis(_settings.AccelerateButton);
        float decelerateAxis = Input.GetAxis(_settings.DecelerateButton);

        bool acceleratePressed = accelerateAxis > TriggerPressThreshold;
        bool deceleratePressed = decelerateAxis > TriggerPressThreshold;

        return InputHelper.CalculateMovementState(acceleratePressed, deceleratePressed);
    }

    public bool IsDevicePresent() => Input.GetJoystickNames().Any(name => !string.IsNullOrEmpty(name));
    public void Enable() => _isEnabled = true;
    public void Disable() => _isEnabled = false;
}