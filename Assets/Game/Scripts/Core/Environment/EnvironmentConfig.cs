using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Environment
{
    [Serializable]
    public class EnvironmentConfig
    {
        public Vector2 SceneSize = new Vector2(20f, 20f);
        public int MaxEnemies = 10;
        public float UFOSpawnProbability = 0.3f; // 0 = only asteroids, 1 = only UFOs
        public float EnemySpawnRate = 2f; // seconds between spawn attempts
        public float ScreenWrapMargin = 1f; // How far off-screen before wrapping
    }
}
