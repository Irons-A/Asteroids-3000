using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Interfaces
{
    public interface IPlayerControllable
    {
        void Accelerate();
        void Decelerate();
        void StopMovement();
        void Rotate(Vector2 rotationDirection);
        void ShootBullet();
        void ShootLaser();
        void ToggleMenu();
    }
}
