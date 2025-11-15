using Scripts.Core.Signals.EnvironmentSignals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.Environment
{
    public class EnvironmentSystem : IInitializable, ITickable
    {
        private readonly EnvironmentConfig _config;
        private readonly SignalBus _signalBus;
        private float _spawnTimer;

        public Vector2 WorldBounds => _config.SceneSize;
        public Vector2 ScreenWrapBounds => _config.SceneSize + new Vector2(_config.ScreenWrapMargin * 2, _config.ScreenWrapMargin * 2);

        public EnvironmentSystem(EnvironmentConfig config, SignalBus signalBus)
        {
            _config = config;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _spawnTimer = 0f;
        }

        public void Tick()
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _config.EnemySpawnRate)
            {
                _spawnTimer = 0f;
                TrySpawnEnemy();
            }
        }

        private void TrySpawnEnemy()
        {
            _signalBus.Fire(new TrySpawnEnemySignal());
        }

        public Vector2 GetRandomSpawnPosition()
        {
            // Spawn at edges of the screen
            var side = Random.Range(0, 4);
            var bounds = ScreenWrapBounds * 0.5f;

            return side switch
            {
                0 => new Vector2(-bounds.x, Random.Range(-bounds.y, bounds.y)), // Left
                1 => new Vector2(bounds.x, Random.Range(-bounds.y, bounds.y)),  // Right
                2 => new Vector2(Random.Range(-bounds.x, bounds.x), -bounds.y), // Bottom
                _ => new Vector2(Random.Range(-bounds.x, bounds.x), bounds.y)   // Top
            };
        }

        public bool ShouldSpawnUFO()
        {
            return Random.Range(0f, 1f) <= _config.UFOSpawnProbability;
        }
    }
}
