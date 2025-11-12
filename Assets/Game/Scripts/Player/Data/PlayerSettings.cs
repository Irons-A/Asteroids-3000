using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    public float AccelerationRate = 5f;
    public float DecelerationRate = 0.95f;
    public float MaxSpeed = 10f;
    public float RotationSpeed = 180f;
    public int InitialLives = 3;
}
