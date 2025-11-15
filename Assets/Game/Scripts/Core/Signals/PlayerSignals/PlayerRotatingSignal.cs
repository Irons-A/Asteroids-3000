using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Signals.PlayerSignals
{
    public class PlayerRotatingSignal
    {
        public float TargetAngle { get; }
        public PlayerRotatingSignal(float targetAngle) => TargetAngle = targetAngle;
    }
}
