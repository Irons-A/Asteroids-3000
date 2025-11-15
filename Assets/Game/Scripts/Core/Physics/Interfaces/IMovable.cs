using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Physics.Interfaces
{
    public interface IMovable
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        float Rotation { get; set; }
        float Mass { get; }
        bool UseGravity { get; }
        bool IsKinematic { get; }
    }
}
