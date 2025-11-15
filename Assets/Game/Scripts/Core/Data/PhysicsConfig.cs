using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Data
{
    [Serializable]
    public class PhysicsConfig
    {
        public float Gravity = 0f;
        public float Drag = 0.1f;
        public float AngularDrag = 0.05f;
    }
}
