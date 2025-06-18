using UnityEngine;
using System;
using Scripts.Enemies;
using YG;

namespace Scripts.PlayerUFO
{
    public class PlayerLaser : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _cooldown = 15f;
        [SerializeField] private float _searchInterval = 0.1f;
        [SerializeField] private float _searchRadius = 14f;
        [SerializeField] private float _laserDuration = 0.2f;
        [SerializeField] private int _damage = 1;

        [Header("References")] [SerializeField]
        private AudioSource _laserSound;

        [SerializeField] private LineRenderer _laserLine;

        private readonly Collider2D[] _enemyBuffer = new Collider2D[15];

        private Transform _transform;
        private float _lastSearchTime;
        private float _laserTimer;
        private bool _isLaserActive;
        private bool _isReady = true;

        public event Action<int> Hited;

        private void Awake()
        {
            _transform = transform;
            InitializeLaser();
        }

        private void Start()
        {
            _cooldown -= YG2.saves.CooldownLaser;
        }

        private void Update()
        {
            if (_isLaserActive)
            {
                UpdateLaser();

                return;
            }

            if (_isReady)
                SearchForEnemies();
        }
        
        public void ResetLaser()
        {
            CancelInvoke(nameof(SetReady));
            _isReady = true;
            DeactivateLaser();
        }

        private void InitializeLaser()
        {
            _laserLine.positionCount = 2;
            _laserLine.gameObject.SetActive(false);
        }

        private void UpdateLaser()
        {
            _laserTimer += Time.deltaTime;

            if (!(_laserTimer >= _laserDuration))
                return;

            DeactivateLaser();
            StartCooldown();
        }

        private void SearchForEnemies()
        {
            if (Time.time - _lastSearchTime < _searchInterval)
                return;

            _lastSearchTime = Time.time;

            if (TryFindNearestEnemy(out Enemy enemy))
                AttackEnemy(enemy);
        }

        private bool TryFindNearestEnemy(out Enemy enemy)
        {
            enemy = null;
            int count = Physics2D.OverlapCircleNonAlloc(_transform.position, _searchRadius, _enemyBuffer, _enemyLayer);

            if (count == 0)
                return false;

            float closestDistance = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                if (!_enemyBuffer[i].TryGetComponent(out Enemy currentEnemy))
                    continue;

                float distance = (_enemyBuffer[i].transform.position - _transform.position).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    enemy = currentEnemy;
                }
            }

            return enemy != null;
        }

        private void AttackEnemy(Enemy enemy)
        {
            _laserSound?.Play();
            _isLaserActive = true;
            _isReady = false;
            _laserTimer = 0f;

            _laserLine.SetPosition(0, _transform.position);
            _laserLine.SetPosition(1, enemy.TransformEnemy.position);
            _laserLine.gameObject.SetActive(true);

            enemy.TakeDamage(_damage);
            Hited?.Invoke(enemy.ScoreValue);
        }

        private void DeactivateLaser()
        {
            _isLaserActive = false;
            _laserLine.gameObject.SetActive(false);
        }

        private void StartCooldown()
        {
            _isReady = false;
            Invoke(nameof(SetReady), _cooldown);
        }

        private void SetReady() =>
            _isReady = true;
    }
}