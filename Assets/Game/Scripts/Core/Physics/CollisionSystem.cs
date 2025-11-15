using Scripts.Core.Physics.Interfaces;
using Scripts.Core.Signals.CollisionSignals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.Physics
{
    public class CollisionSystem : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        public CollisionSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CollisionOccurredSignal>(OnCollisionOccurred);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CollisionOccurredSignal>(OnCollisionOccurred);
        }

        private void OnCollisionOccurred(CollisionOccurredSignal signal)
        {
            if (signal.Collider1 is IMovable movable1 && signal.Collider2 is IMovable movable2)
            {
                HandleRicochet(movable1, movable2, signal.CollisionNormal);
            }
        }

        private void HandleRicochet(IMovable movable1, IMovable movable2, Vector2 collisionNormal)
        {
            var relativeVelocity = movable1.Velocity - movable2.Velocity;
            var velocityAlongNormal = Vector2.Dot(relativeVelocity, collisionNormal);

            if (velocityAlongNormal > 0) return;

            float restitution = 0.8f; 
            float impulseScalar = -(1 + restitution) * velocityAlongNormal;
            impulseScalar /= movable1.Mass + movable2.Mass;

            var impulse = impulseScalar * collisionNormal;
            movable1.Velocity += impulse * movable2.Mass;
            movable2.Velocity -= impulse * movable1.Mass;
        }
    }
}
