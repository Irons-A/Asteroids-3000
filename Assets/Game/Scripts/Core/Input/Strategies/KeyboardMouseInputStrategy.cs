using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseInputStrategy : IInputStrategy
{
    private KeyboardMouseSettings _settings;
    private bool _isEnabled = true;

    private const float ScreenCenterXRatio = 0.5f;
    private const float ScreenCenterYRatio = 0.5f;

    public void Initialize(InputSettings settings)
    {
        _settings = settings.KeyboardMouse;
    }

    public PlayerInputData GetInput()
    {
        if (!_isEnabled) return default;

        return new PlayerInputData
        {
            RotationDirection = GetRotationDirection(),
            Movement = GetMovementState(),
            ShootBullet = Input.GetKeyDown(_settings.ShootBulletKey),
            ShootLaser = Input.GetKeyDown(_settings.ShootLaserKey),
            ToggleMenu = Input.GetKeyDown(_settings.MenuKey)
        };
    }

    private Vector2 GetRotationDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 screenCenter = new Vector2(Screen.width * ScreenCenterXRatio, 
            Screen.height * ScreenCenterYRatio);
        Vector2 direction = (new Vector2(mousePosition.x, mousePosition.y) - screenCenter).normalized;

        return direction * _settings.MouseSensitivity;
    }

    private MovementState GetMovementState()
    {
        bool acceleratePressed = Input.GetKey(_settings.AccelerateKey);
        bool deceleratePressed = Input.GetKey(_settings.DecelerateKey);

        return InputHelper.CalculateMovementState(acceleratePressed, deceleratePressed);
    }

    public bool IsDevicePresent() => true;
    public void Enable() => _isEnabled = true;
    public void Disable() => _isEnabled = false;
}
