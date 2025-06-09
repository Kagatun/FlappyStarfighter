using System.Collections.Generic;
using Scripts.Spawner;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private List<Transform> _firepoints;
        [SerializeField] private float _minTimeBetween;
        [SerializeField] private float _maxTimeBetween;
        [SerializeField] private int _damage;
        [SerializeField] private int _speedBullet;
        
        private SpawnerBulletEnemy _spawnerBullet;
        private float _attackTimer;
        private float _currentAttackInterval;
        
        private void Update()
        {
            _attackTimer += Time.deltaTime;

            if (!(_attackTimer >= _currentAttackInterval))
                return;
            
            Shoot();
            Reset();
        }

        private void OnEnable()
        {
            Reset();
        }

        private void Reset()
        {
            _attackTimer = 0;
            _currentAttackInterval = Random.Range(_minTimeBetween, _maxTimeBetween);
        }

        public void SetSpawnerBullet(SpawnerBulletEnemy bulletEnemy) =>
            _spawnerBullet = bulletEnemy;

        private void Shoot()
        {
            for (int i = 0; i < _firepoints.Count; i++)
                _spawnerBullet.Spawn(_firepoints[i].transform, _speedBullet, _damage);
        }
    }
}