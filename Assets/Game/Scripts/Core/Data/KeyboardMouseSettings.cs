using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Data
{
    [Serializable]
    public class KeyboardMouseSettings
    {
        public KeyCode AccelerateKey;
        public KeyCode DecelerateKey;
        public KeyCode ShootBulletKey;
        public KeyCode ShootLaserKey;
        public KeyCode MenuKey;
        public float MouseSensitivity;
    }
}
