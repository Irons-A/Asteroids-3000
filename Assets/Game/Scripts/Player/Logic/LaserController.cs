using Scripts.Player.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Player.Logic
{
    public class LaserController : ITickable
    {
        private readonly LaserModel _laserModel;
        private readonly SignalBus _signalBus;

        public LaserController(LaserModel laserModel, SignalBus signalBus)
        {
            _laserModel = laserModel;
            _signalBus = signalBus;
            _laserModel.CurrentCharges = _laserModel.MaxCharges;
        }

        public void Tick()
        {
            UpdateCooldown();
            UpdateCharging();
        }

        private void UpdateCooldown()
        {
            if (_laserModel.CurrentCooldown > 0)
            {
                _laserModel.CurrentCooldown -= Time.deltaTime;
            }
        }

        private void UpdateCharging()
        {
            if (!_laserModel.IsCharging) return;

            _laserModel.CurrentChargeTimer += Time.deltaTime;

            if (_laserModel.CurrentChargeTimer >= _laserModel.ChargeCooldown)
            {
                _laserModel.CurrentCharges++;
                _laserModel.CurrentChargeTimer = 0f;

                if (_laserModel.CurrentCharges < _laserModel.MaxCharges)
                {
                    _laserModel.CurrentChargeTimer = 0f;
                }
            }
        }

        public bool TryShoot()
        {
            if (!_laserModel.IsReadyToShoot) return false;

            _laserModel.CurrentCharges--;
            _laserModel.CurrentCooldown = _laserModel.ShotCooldown;

            if (_laserModel.CurrentCharges < _laserModel.MaxCharges)
            {
                _laserModel.CurrentChargeTimer = 0f;
            }

            return true;
        }
    }
}
