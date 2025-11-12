using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Velocity { get; set; }
    public bool IsAlive { get; set; }
    public int Lives { get; set; }
    public int Score { get; set; }
    public bool IsInvulnerable { get; set; }
    public bool IsControllable { get; set; }

    // Stats for game logic
    public float InvulnerabilityDuration { get; set; }
    public float UncontrollableDuration { get; set; }
}
