using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Signals.PhysicsSignals
{
    public class EntityDestroyedSignal
    {
        public object Entity { get; }

        public EntityDestroyedSignal(object entity)
        {
            Entity = entity;
        }
    }
}
