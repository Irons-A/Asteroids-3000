using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootLaserSignal
{
    public Vector2 Position { get; }
    public float Rotation { get; }
    public PlayerShootLaserSignal(Vector2 position, float rotation)
    {
        Position = position;
        Rotation = rotation;
    }
}
