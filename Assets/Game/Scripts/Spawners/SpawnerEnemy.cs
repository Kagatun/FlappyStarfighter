using Scripts.Enemies;
using UnityEngine;

namespace Scripts.Spawner
{
    public class SpawnerEnemy : SpawnerObjects<Enemy>
    {
        [SerializeField] private SpawnerEffect _effect;
        [SerializeField] private SpawnerBulletEnemy  _spawnerBullet;
        [SerializeField] private float _minStartPositionY = 3.33f;
        [SerializeField] private float _maxStartPositionY = -3.85f;
        [SerializeField] private float _startPositionX;

        public void Spawn()
        {
            Enemy enemy = Get();
            Vector2 spawnPos = transform.position + GetStartPoint();
    
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = transform.rotation;
            enemy.ResetParameters(spawnPos);
            enemy.SetSpawnerBullet(_spawnerBullet);
        }

        protected override void OnGet(Enemy enemy)
        {
            base.OnGet(enemy);
            enemy.Removed += OnReturnToPool;
            enemy.Exploded += OnSetPositionEnemy;
        }

        protected override void OnRelease(Enemy enemy)
        {
            enemy.Exploded -= OnSetPositionEnemy;
            enemy.Removed -= OnReturnToPool;
            base.OnRelease(enemy);
        }

        private void OnReturnToPool(Enemy enemy) =>
            AddToPool(enemy);

        private void OnSetPositionEnemy(Vector3 position) =>
            _effect.Spawn(position);

        private Vector3 GetStartPoint() =>
            new Vector2(_startPositionX, Random.Range(_minStartPositionY, _maxStartPositionY));
    }
}