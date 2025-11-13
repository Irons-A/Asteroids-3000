using Scripts.Core.Data;
using Scripts.Core.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.PlayerInput.Strategies
{
    public class MobileInputStrategy : IInputStrategy
    {
        private MobileSettings _settings;
        private bool _isEnabled = true;

        public void Initialize(InputSettings settings)
        {
            _settings = settings.Mobile;
        }

        public PlayerInputData GetInput()
        {
            if (!_isEnabled) return default;

            // Unfinished

            return new PlayerInputData
            {
                RotationDirection = GetRotationDirection(),
                Movement = GetMovementState(),
                ShootBullet = false,
                ShootLaser = false,
                ToggleMenu = false
            };
        }

        private Vector2 GetRotationDirection()
        {
            // Unfinished
            return Vector2.zero;
        }

        private MovementState GetMovementState()
        {
            // Unfinished
            return MovementState.None;
        }

        public bool IsDevicePresent() => Application.isMobilePlatform;
        public void Enable() => _isEnabled = true;
        public void Disable() => _isEnabled = false;
    }
}