using Cysharp.Threading.Tasks;
using Scripts.Core.Interfaces;
using Scripts.Core.Signals;
using Scripts.Player.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Player.Logic
{
    public class PlayerController : IPlayerControllable, IInitializable, IDisposable, ITickable
    {
        private readonly SignalBus _signalBus;
        private readonly PlayerModel _playerModel;
        private readonly PlayerSettings _playerSettings;
        private readonly PlayerViewModel _playerViewModel;
        private readonly LaserController _laserController;
        private readonly LaserModel _laserModel;

        private bool _isInvulnerabilityActive = false;
        private bool _isUncontrollableActive = false;

        public PlayerController(SignalBus signalBus, PlayerModel playerModel,
                              PlayerSettings playerSettings, PlayerViewModel playerViewModel,
                              LaserController laserController, LaserModel laserModel)
        {
            _signalBus = signalBus;
            _playerModel = playerModel;
            _playerSettings = playerSettings;
            _playerViewModel = playerViewModel;
            _laserController = laserController;
            _laserModel = laserModel;
        }

        public void Initialize()
        {
            ResetPlayerState();
            _signalBus.Subscribe<PlayerHitSignal>(OnPlayerHit);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerHitSignal>(OnPlayerHit);
        }

        public void Tick()
        {
            UpdateUIValues();
        }

        private void UpdateUIValues()
        {
            _playerViewModel.LaserCharges.Value = _laserModel.CurrentCharges;

            if (_laserModel.CurrentCharges >= _laserModel.MaxCharges)
            {
                _playerViewModel.LaserChargeProgress.Value = 1f;
            }
            else
            {
                _playerViewModel.LaserChargeProgress.Value = _laserModel.ChargeProgress;
            }

            var normalizedAngle = NormalizeAngle(_playerModel.Rotation);

            _playerViewModel.RotationAngle.Value = normalizedAngle;
            _playerViewModel.RotationProgress.Value = normalizedAngle / 360f;
            _playerViewModel.Speed.Value = _playerModel.Velocity.magnitude;
            _playerViewModel.Position.Value = _playerModel.Position;
        }

        private float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle < 0) angle += 360f;
            return angle;
        }

        private void ResetPlayerState()
        {
            _playerModel.Position = Vector2.zero;
            _playerModel.Rotation = 0f;
            _playerModel.Velocity = Vector2.zero;
            _playerModel.IsAlive = true;
            _playerModel.IsControllable = true;
            _playerModel.IsInvulnerable = false;
            _playerModel.Lives = _playerSettings.InitialLives;
            _playerModel.Score = 0;

            _playerViewModel.Lives.Value = _playerModel.Lives;
            _playerViewModel.Score.Value = _playerModel.Score;
            _playerViewModel.IsInvulnerable.Value = false;
        }

        public void Accelerate()
        {
            if (!CanControl()) return;

            var accelerationDirection = new Vector2(
                Mathf.Sin(_playerModel.Rotation * Mathf.Deg2Rad),
                Mathf.Cos(_playerModel.Rotation * Mathf.Deg2Rad)
            );

            _playerModel.Velocity += accelerationDirection * _playerSettings.AccelerationRate;

            if (_playerModel.Velocity.magnitude > _playerSettings.MaxSpeed)
            {
                _playerModel.Velocity = _playerModel.Velocity.normalized * _playerSettings.MaxSpeed;
            }

            _signalBus.Fire(new PlayerAcceleratingSignal());
        }

        public void Decelerate()
        {
            if (!CanControl()) return;

            var decelerationDirection = -_playerModel.Velocity.normalized;
            _playerModel.Velocity += decelerationDirection * _playerSettings.AccelerationRate;

            if (Vector2.Dot(_playerModel.Velocity, decelerationDirection) > 0)
            {
                _playerModel.Velocity = Vector2.zero;
            }

            _signalBus.Fire(new PlayerDeceleratingSignal());
        }

        public void StopMovement()
        {

        }

        public void Rotate(Vector2 rotationDirection)
        {
            if (!CanControl()) return;

            if (rotationDirection != Vector2.zero)
            {
                var targetAngle = Mathf.Atan2(rotationDirection.x, rotationDirection.y) * Mathf.Rad2Deg;

                _playerModel.Rotation = targetAngle;

                _signalBus.Fire(new PlayerRotatingSignal(targetAngle));
            }
        }

        public void ShootBullet()
        {
            if (!CanControl()) return;

            _signalBus.Fire(new PlayerShootBulletSignal(_playerModel.Position, _playerModel.Rotation));
        }

        public void ShootLaser()
        {
            if (!CanControl()) return;

            if (_laserController.TryShoot())
            {
                _signalBus.Fire(new PlayerShootLaserSignal(_playerModel.Position, _playerModel.Rotation, _laserModel.LaserDuration));
            }
        }

        public void ToggleMenu()
        {
            _signalBus.Fire(new ToggleMenuSignal());
        }

        private bool CanControl()
        {
            return _playerModel.IsAlive && _playerModel.IsControllable;
        }

        private async void OnPlayerHit()
        {
            if (_playerModel.IsInvulnerable) return;

            _playerModel.Lives--;
            _playerViewModel.Lives.Value = _playerModel.Lives;

            if (_playerModel.Lives <= 0)
            {
                _playerModel.IsAlive = false;
                _signalBus.Fire(new PlayerDiedSignal());
                return;
            }

            await StartInvulnerabilityPeriod();
            await StartUncontrollablePeriod();

            _signalBus.Fire(new PlayerRespawnedSignal());
        }

        private async UniTask StartInvulnerabilityPeriod()
        {
            _playerModel.IsInvulnerable = true;
            _playerViewModel.IsInvulnerable.Value = true;
            _isInvulnerabilityActive = true;

            await UniTask.Delay(TimeSpan.FromSeconds(_playerSettings.InvulnerabilityDuration));

            if (_isInvulnerabilityActive)
            {
                _playerModel.IsInvulnerable = false;
                _playerViewModel.IsInvulnerable.Value = false;
            }
        }

        private async UniTask StartUncontrollablePeriod()
        {
            _playerModel.IsControllable = false;
            _isUncontrollableActive = true;

            await UniTask.Delay(TimeSpan.FromSeconds(_playerSettings.UncontrollableDuration));

            if (_isUncontrollableActive)
            {
                _playerModel.IsControllable = true;
            }
        }

        public void UpdatePosition(float deltaTime)
        {
            if (!_playerModel.IsAlive) return;

            _playerModel.Position += _playerModel.Velocity * deltaTime;
        }

        public void ResetToCenter()
        {
            _playerModel.Position = Vector2.zero;
            _playerModel.Velocity = Vector2.zero;
            _isInvulnerabilityActive = false;
            _isUncontrollableActive = false;
            _playerModel.IsInvulnerable = false;
            _playerModel.IsControllable = true;
            _playerViewModel.IsInvulnerable.Value = false;
        }
    }
}
