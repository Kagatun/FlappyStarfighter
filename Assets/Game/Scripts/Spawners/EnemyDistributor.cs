using System.Collections.Generic;
using Scripts.Systems;
using Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Spawner
{
    public class EnemyDistributor : MonoBehaviour
    {
        [SerializeField] private List<SpawnerEnemy> _spawnerEnemies;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private EnemyTrackingZone _trackingZone;
        [SerializeField] private float _minDelay = 1f;
        [SerializeField] private float _maxDelay = 3f;

        private List<int> _activeSpawnerIndices = new List<int>();
        private List<int> _spawnSequence = new List<int>();
        private int _totalEnemies;
        private int _spawnedCount;
        private float _spawnTimer;
        private bool _isSpawning;
        private bool _bossSpawned;

        private void Update()
        {
            if (!_isSpawning) return;

            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0f)
            {
                SpawnNextEnemy();
                _spawnTimer = Random.Range(_minDelay, _maxDelay);
            }
        }

        public void SetEnemies(List<bool> enabledTypes, int totalEnemies)
        {
            _activeSpawnerIndices.Clear();
            for (int i = 0; i < enabledTypes.Count && i < _spawnerEnemies.Count - 1; i++)
            {
                if (enabledTypes[i])
                    _activeSpawnerIndices.Add(i);
            }

            _spawnSequence.Clear();
            
            // Добавляем по одному каждого типа в случайном порядке
            if (_activeSpawnerIndices.Count > 0)
            {
                var shuffledTypes = ShuffleList(new List<int>(_activeSpawnerIndices));
                _spawnSequence.AddRange(shuffledTypes);
            }

            // Добавляем оставшихся врагов
            int remainingNormalEnemies = totalEnemies - _spawnSequence.Count;
            for (int i = 0; i < remainingNormalEnemies; i++)
            {
                _spawnSequence.Add(_activeSpawnerIndices[Random.Range(0, _activeSpawnerIndices.Count)]);
            }

            // Настройка босса
            bool hasBoss = enabledTypes.Count > 0 && 
                         enabledTypes[enabledTypes.Count - 1] && 
                         _spawnerEnemies.Count > enabledTypes.Count - 1;

            _totalEnemies = totalEnemies; // Всегда общее количество врагов
            _spawnedCount = 0;
            _bossSpawned = !hasBoss;

            UpdateScoreDisplay();
        }

        private void SpawnNextEnemy()
        {
            // Сначала спавним обычных врагов
            if (_spawnedCount < _spawnSequence.Count)
            {
                SpawnEnemy(_spawnSequence[_spawnedCount]);
                return;
            }

            // Затем босса (если есть)
            if (!_bossSpawned && _spawnerEnemies.Count > 0)
            {
                SpawnEnemy(_spawnerEnemies.Count - 1);
                _bossSpawned = true;
                return;
            }

            FinishSpawning();
        }

        private void SpawnEnemy(int spawnerIndex)
        {
            if (spawnerIndex < 0 || spawnerIndex >= _spawnerEnemies.Count)
                return;

            _spawnerEnemies[spawnerIndex].Spawn();
            _spawnedCount++;
            UpdateScoreDisplay();
        }

        private List<T> ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
            return list;
        }

        private void UpdateScoreDisplay()
        {
            int remainingEnemies = (_totalEnemies - _spawnedCount) + (_bossSpawned ? 0 : 1);
            _scoreView.ShowScore(Mathf.Max(0, remainingEnemies));
        }

        public void StartSpawning()
        {
            _isSpawning = true;
            _spawnTimer = Random.Range(_minDelay, _maxDelay);
        }

        public void StopSpawning() => _isSpawning = false;

        private void FinishSpawning()
        {
            _isSpawning = false;
            _trackingZone.StartSearching();
        }
    }
}