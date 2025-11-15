using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Signals.EvironmentSignals
{
    public class EnemySpawnedSignal
    {
        public object Enemy { get; }

        public EnemySpawnedSignal(object enemy)
        {
            Enemy = enemy;
        }
    }
}
