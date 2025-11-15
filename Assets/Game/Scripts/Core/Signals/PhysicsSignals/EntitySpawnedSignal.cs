using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Signals.PhysicsSignals
{
    public class EntitySpawnedSignal
    {
        public object Entity { get; }

        public EntitySpawnedSignal(object entity)
        {
            Entity = entity;
        }
    }
}
