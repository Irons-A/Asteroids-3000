using Scripts.Core.Data;
using Scripts.Core.Physics.Interfaces;
using Scripts.Core.Signals.PhysicsSignals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.Physics
{
    public class MovementSystem : IInitializable, ITickable, IDisposable
    {
        private readonly List<IMovable> _movables = new List<IMovable>();
        private readonly PhysicsConfig _physicsConfig;
        private readonly SignalBus _signalBus;

        public MovementSystem(PhysicsConfig physicsConfig, SignalBus signalBus)
        {
            _physicsConfig = physicsConfig;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EntitySpawnedSignal>(OnEntitySpawned);
            _signalBus.Subscribe<EntityDestroyedSignal>(OnEntityDestroyed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EntitySpawnedSignal>(OnEntitySpawned);
            _signalBus.Unsubscribe<EntityDestroyedSignal>(OnEntityDestroyed);
            _movables.Clear();
        }

        public void RegisterMovable(IMovable movable)
        {
            if (!_movables.Contains(movable))
            {
                _movables.Add(movable);
            }
        }

        public void UnregisterMovable(IMovable movable)
        {
            _movables.Remove(movable);
        }

        public void Tick()
        {
            var deltaTime = Time.deltaTime;

            foreach (var movable in _movables)
            {
                if (!movable.IsKinematic)
                {
                    UpdatePhysics(movable, deltaTime);
                }

                UpdatePosition(movable, deltaTime);
            }
        }

        private void UpdatePhysics(IMovable movable, float deltaTime)
        {
            if (movable.UseGravity)
            {
                movable.Velocity += Vector2.down * _physicsConfig.Gravity * deltaTime;
            }

            movable.Velocity *= (1f - _physicsConfig.Drag * deltaTime);

            // Clamp velocity to prevent extreme values
            var maxSpeed = 50f;

            if (movable.Velocity.magnitude > maxSpeed)
            {
                movable.Velocity = movable.Velocity.normalized * maxSpeed;
            }
        }

        private void UpdatePosition(IMovable movable, float deltaTime)
        {
            movable.Position += movable.Velocity * deltaTime;
        }

        private void OnEntitySpawned(EntitySpawnedSignal signal)
        {
            if (signal.Entity is IMovable movable)
            {
                RegisterMovable(movable);
            }
        }

        private void OnEntityDestroyed(EntityDestroyedSignal signal)
        {
            if (signal.Entity is IMovable movable)
            {
                UnregisterMovable(movable);
            }
        }
    }
}
