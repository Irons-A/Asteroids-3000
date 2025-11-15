using Scripts.Core.Physics.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Data
{
    public class PlayerModel : IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        public float Mass => 1f;
        public bool UseGravity => false;
        public bool IsKinematic => false;

        public bool IsAlive { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }
        public bool IsInvulnerable { get; set; }
        public bool IsControllable { get; set; }

        public float InvulnerabilityDuration { get; set; }
        public float UncontrollableDuration { get; set; }
    }
}
