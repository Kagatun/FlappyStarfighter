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

        private Transform _transformSpawner;
        
        public void Spawn()
        {
            _transformSpawner = transform;
            
            Enemy enemy = Get();
            Vector2 spawnPos = _transformSpawner.position + GetStartPoint();
    
            enemy.TransformEnemy.position = spawnPos;
            enemy.TransformEnemy.rotation = _transformSpawner.rotation;
            enemy.ResetParameters(spawnPos);
            enemy.SetSpawnerBullet(_spawnerBullet);
            enemy.ShootOnEnable();
        }

        protected override void OnGet(Enemy enemy)
        {
            base.OnGet(enemy);
            enemy.Removed += OnReturnToPool;
            enemy.Exploded += OnSetPositionEnemy;
        }

        protected override void OnRelease(Enemy enemy)
        {
            base.OnRelease(enemy);
            enemy.Exploded -= OnSetPositionEnemy;
            enemy.Removed -= OnReturnToPool;
            enemy.ShootOnDisable();
        }

        private void OnReturnToPool(Enemy enemy) =>
            AddToPool(enemy);

        private void OnSetPositionEnemy(Vector3 position) =>
            _effect.Spawn(position);

        private Vector3 GetStartPoint() =>
            new Vector2(_startPositionX, Random.Range(_minStartPositionY, _maxStartPositionY));
    }
}