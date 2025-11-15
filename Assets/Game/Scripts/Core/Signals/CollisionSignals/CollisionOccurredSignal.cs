using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Signals.CollisionSignals
{
    public class CollisionOccurredSignal
    {
        public object Collider1 { get; }
        public object Collider2 { get; }
        public Vector2 CollisionNormal { get; }

        public CollisionOccurredSignal(object collider1, object collider2, Vector2 collisionNormal)
        {
            Collider1 = collider1;
            Collider2 = collider2;
            CollisionNormal = collisionNormal;
        }
    }

}
