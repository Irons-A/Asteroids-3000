using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Data
{
    public class LaserModel
    {
        public int MaxCharges { get; set; } = 3;
        public int CurrentCharges { get; set; } = 3;
        public float ChargeCooldown { get; set; } = 5f;
        public float ShotCooldown { get; set; } = 0.5f;
        public float LaserDuration { get; set; } = 0.25f;

        public float CurrentCooldown { get; set; }
        public bool IsReadyToShoot => CurrentCharges > 0 && CurrentCooldown <= 0;

        public float CurrentChargeTimer { get; set; }
        public bool IsCharging => CurrentCharges < MaxCharges;
        public float ChargeProgress => CurrentChargeTimer / ChargeCooldown;
    }
}
