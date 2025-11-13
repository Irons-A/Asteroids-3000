using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    public float AccelerationRate;
    public float DecelerationRate;
    public float MaxSpeed;
    public float RotationSpeed;
    public int InitialLives;
    public float InvulnerabilityDuration;
    public float UncontrollableDuration;
    public LaserSettingsData LaserSettings;
}
