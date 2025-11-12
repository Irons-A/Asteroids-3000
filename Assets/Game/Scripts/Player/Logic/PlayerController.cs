using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : IPlayerControllable, IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly PlayerModel _playerModel;
    private readonly PlayerSettings _playerSettings;

    public PlayerController(SignalBus signalBus, PlayerModel playerModel, PlayerSettings playerSettings)
    {
        _signalBus = signalBus;
        _playerModel = playerModel;
        _playerSettings = playerSettings;
    }

    public void Initialize()
    {
        // Initialize player state
        _playerModel.Position = Vector2.zero;
        _playerModel.Rotation = 0f;
        _playerModel.Velocity = Vector2.zero;
        _playerModel.IsAlive = true;
    }

    public void Dispose()
    {
        // Cleanup if needed
    }

    public void Accelerate()
    {
        if (!_playerModel.IsAlive) return;

        // Calculate acceleration direction based on current rotation
        var accelerationDirection = new Vector2(
            Mathf.Sin(_playerModel.Rotation * Mathf.Deg2Rad),
            Mathf.Cos(_playerModel.Rotation * Mathf.Deg2Rad)
        );

        _playerModel.Velocity += accelerationDirection * _playerSettings.AccelerationRate;

        // Clamp maximum speed
        if (_playerModel.Velocity.magnitude > _playerSettings.MaxSpeed)
        {
            _playerModel.Velocity = _playerModel.Velocity.normalized * _playerSettings.MaxSpeed;
        }

        _signalBus.Fire(new PlayerAcceleratingSignal());
    }

    public void Decelerate()
    {
        if (!_playerModel.IsAlive) return;

        // Apply deceleration (friction/reverse thrust)
        _playerModel.Velocity *= _playerSettings.DecelerationRate;

        // If velocity is very small, stop completely
        if (_playerModel.Velocity.magnitude < 0.1f)
        {
            _playerModel.Velocity = Vector2.zero;
        }

        _signalBus.Fire(new PlayerDeceleratingSignal());
    }

    public void StopMovement()
    {
        // Let velocity naturally decay or maintain current velocity
        // In Asteroids, momentum is usually preserved when no input
    }

    public void Rotate(Vector2 rotationDirection)
    {
        if (!_playerModel.IsAlive) return;

        if (rotationDirection != Vector2.zero)
        {
            // Convert direction vector to rotation angle
            var targetAngle = Mathf.Atan2(rotationDirection.x, rotationDirection.y) * Mathf.Rad2Deg;
            _playerModel.Rotation = targetAngle;

            _signalBus.Fire(new PlayerRotatingSignal(targetAngle));
        }
    }

    public void ShootBullet()
    {
        if (!_playerModel.IsAlive) return;

        _signalBus.Fire(new PlayerShootBulletSignal(_playerModel.Position, _playerModel.Rotation));
    }

    public void ShootLaser()
    {
        if (!_playerModel.IsAlive) return;

        _signalBus.Fire(new PlayerShootLaserSignal(_playerModel.Position, _playerModel.Rotation));
    }

    public void ToggleMenu()
    {
        _signalBus.Fire(new ToggleMenuSignal());
    }

    // Called by game loop to update player position based on velocity
    public void UpdatePosition(float deltaTime)
    {
        if (!_playerModel.IsAlive) return;

        _playerModel.Position += _playerModel.Velocity * deltaTime;

        // Handle screen wrapping
        WrapAroundScreenEdges();
    }

    private void WrapAroundScreenEdges()
    {
        // This will be implemented once we have screen bounds
        // For now, it's a placeholder
    }
}
