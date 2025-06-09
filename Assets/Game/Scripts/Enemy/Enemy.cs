using System;
using Scripts.Spawner;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable, IDestroyable
    {
        [SerializeField] private EnemyAttacker _attacker;
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private int _hitPoint;

        [field: SerializeField] public int ScoreValue { get; private set; }

        private Health _health;

        public event Action<Enemy> Removed;
        public event Action <Vector3> Exploded;

        private void Awake()
        {
            _health = new Health(_hitPoint);
        }

        private void OnEnable()
        {
            _health.Deceased += OnDestroy;
        }

        private void OnDisable()
        {
            _health.Deceased -= OnDestroy;
        }
        
        public void ResetParameters(Vector2 spawnPosition)
        {
            _health.Reset();
            _mover.ResetParameters(spawnPosition);
        }

        public void TakeDamage(int damage) =>
            _health.TakeDamage(damage);

        public void OnDestroy()
        {
            if(_health.Value <= 0)
                Exploded?.Invoke(transform.position);
            
            Removed?.Invoke(this);
        }

        public void SetSpawnerBullet(SpawnerBulletEnemy spawnerBullet)
        {
            if (_attacker == null)
                return;

            _attacker.SetSpawnerBullet(spawnerBullet);
        }
    }
}