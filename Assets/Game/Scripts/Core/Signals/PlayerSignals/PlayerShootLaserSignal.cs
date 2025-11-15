using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Signals.PlayerSignals
{
    public class PlayerShootLaserSignal
    {
        public Vector2 Position { get; }
        public float Rotation { get; }
        public float Duration { get; }

        public PlayerShootLaserSignal(Vector2 position, float rotation, float duration)
        {
            Position = position;
            Rotation = rotation;
            Duration = duration;
        }
    }
}