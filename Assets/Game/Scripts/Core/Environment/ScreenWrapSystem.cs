using Scripts.Core.Data;
using Scripts.Core.Environment;
using Scripts.Core.Physics.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.Environment
{
    public class ScreenWrapSystem : ITickable
    {
        private readonly List<IMovable> _wrapEntities = new List<IMovable>();
        private readonly EnvironmentSystem _environmentSystem;
        private readonly SignalBus _signalBus;

        public ScreenWrapSystem(EnvironmentSystem environmentSystem, SignalBus signalBus)
        {
            _environmentSystem = environmentSystem;
            _signalBus = signalBus;
        }

        public void RegisterForWrapping(IMovable movable)
        {
            if (!_wrapEntities.Contains(movable))
            {
                _wrapEntities.Add(movable);
            }
        }

        public void UnregisterFromWrapping(IMovable movable)
        {
            _wrapEntities.Remove(movable);
        }

        public void Tick()
        {
            foreach (var entity in _wrapEntities)
            {
                WrapEntity(entity);
            }
        }

        private void WrapEntity(IMovable entity)
        {
            var position = entity.Position;
            var bounds = _environmentSystem.ScreenWrapBounds * 0.5f;

            if (position.x > bounds.x)
            {
                position.x = -bounds.x;
            }
            else if (position.x < -bounds.x)
            {
                position.x = bounds.x;
            }

            if (position.y > bounds.y)
            {
                position.y = -bounds.y;
            }
            else if (position.y < -bounds.y)
            {
                position.y = bounds.y;
            }

            entity.Position = position;
        }
    }
}
