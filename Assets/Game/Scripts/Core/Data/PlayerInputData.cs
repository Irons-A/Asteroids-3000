using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Data
{
    public struct PlayerInputData
    {
        public Vector2 RotationDirection;
        public MovementState Movement;
        public bool ShootBullet;
        public bool ShootLaser;
        public bool ToggleMenu;
    }
}
