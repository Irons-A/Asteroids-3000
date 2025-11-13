using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerViewModel
{
    // Basic stats
    public readonly ReactiveProperty<int> Lives = new ReactiveProperty<int>();
    public readonly ReactiveProperty<int> Score = new ReactiveProperty<int>();
    public readonly ReactiveProperty<bool> IsInvulnerable = new ReactiveProperty<bool>();

    // Movement UI
    public readonly ReactiveProperty<float> RotationAngle = new ReactiveProperty<float>(); // 0-359 degrees
    public readonly ReactiveProperty<float> Speed = new ReactiveProperty<float>();
    public readonly ReactiveProperty<Vector2> Position = new ReactiveProperty<Vector2>();

    // Laser UI
    public readonly ReactiveProperty<int> LaserCharges = new ReactiveProperty<int>();
    public readonly ReactiveProperty<float> LaserChargeProgress = new ReactiveProperty<float>();

    // For the radial rotation indicator (0-1 range)
    public readonly ReactiveProperty<float> RotationProgress = new ReactiveProperty<float>();
}
