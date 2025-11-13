using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Data
{
    [Serializable]
    public class LaserSettingsData
    {
        public int MaxCharges;
        public float ChargeCooldown;
        public float ShotCooldown;
        public float LaserDuration;
    }
}
