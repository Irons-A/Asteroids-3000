using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Signals.EnvironmentSignals
{
    public class EnemyDestroyedSignal
    {
        public object Enemy { get; }

        public EnemyDestroyedSignal(object enemy)
        {
            Enemy = enemy;
        }
    }
}
