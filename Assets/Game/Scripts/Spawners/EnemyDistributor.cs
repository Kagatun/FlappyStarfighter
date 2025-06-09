using System.Collections.Generic;
using System.Linq;
using Scripts.Systems;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Spawner
{
    public class EnemyDistributor : MonoBehaviour
    {
        [SerializeField] private List<SpawnerEnemy> _spawnerEnemies;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private EnemyTrackingZone _trackingZone;
        [SerializeField] private List<int> _countEnemies;
        [SerializeField] private float _minDelayBetweenSpawns = 1f;
        [SerializeField] private float _maxDelayBetweenSpawns = 3f;

        private float _spawnTimer;
        private bool _isSpawnAllowed;
        private int _allEnemies;
        private bool _spawningFinished;

        private void Start()
        {
            _spawnTimer = Random.Range(_minDelayBetweenSpawns, _maxDelayBetweenSpawns);
        }

        private void Update()
        {
            if (!_isSpawnAllowed || _spawningFinished)
                return;

            _spawnTimer -= Time.deltaTime;

            if (!(_spawnTimer <= 0f))
                return;

            SpawnEnemy();
            _spawnTimer = Random.Range(_minDelayBetweenSpawns, _maxDelayBetweenSpawns);
        }

        public void SetCountsEnemies(List<int> countEnemies)
        {
            _countEnemies = new List<int>(countEnemies);
            ShowAllEnemies();
        }

        public void StartSpawning()
        {
            _isSpawnAllowed = true;
            _spawningFinished = false;
        }

        public void StopSpawning() =>
            _isSpawnAllowed = false;

        private void SpawnEnemy()
        {
            List<int> availableSpawnerIndices = new List<int>();

            for (int i = 0; i < _countEnemies.Count; i++)
            {
                if (_countEnemies[i] > 0)
                    availableSpawnerIndices.Add(i);
            }

            if (availableSpawnerIndices.Count == 0)
            {
                FinishSpawning();

                return;
            }

            int randomSpawnerIndex = availableSpawnerIndices[Random.Range(0, availableSpawnerIndices.Count)];
            _spawnerEnemies[randomSpawnerIndex].Spawn();
            _allEnemies--;
            _countEnemies[randomSpawnerIndex]--;
            _scoreView.ShowScore(_allEnemies);
        }

        private void FinishSpawning()
        {
            _spawningFinished = true;
            _isSpawnAllowed = false;
            _trackingZone.StartSearching();
        }

        private void ShowAllEnemies()
        {
            _allEnemies = _countEnemies.Sum();
            _scoreView.ShowScore(_allEnemies);
        }
    }
}