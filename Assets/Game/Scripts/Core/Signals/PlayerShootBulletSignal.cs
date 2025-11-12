using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootBulletSignal
{
    public Vector2 Position { get; }
    public float Rotation { get; }
    public PlayerShootBulletSignal(Vector2 position, float rotation)
    {
        Position = position;
        Rotation = rotation;
    }
}
