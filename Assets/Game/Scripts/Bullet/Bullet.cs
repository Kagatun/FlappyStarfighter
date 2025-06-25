using System;
using Scripts.Enemies;
using Scripts.PlayerUFO;
using UnityEngine;

namespace Scripts.Shooting
{
    public class Bullet : MonoBehaviour, IRemovable
    {
        [SerializeField] private LayerMask _targetMask;

        private Vector2 _direction;
        private Vector2 _velocity;
        private float _speed;
        private int _damage;

        public event Action<int> Fitted;
        public event Action<Bullet> Removed;

        public Transform TransformBullet { get; private set; }

        private void Awake()
        {
            TransformBullet = transform;
        }

        private void Update()
        {
            TransformBullet.Translate(_velocity * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if ((_targetMask.value & (1 << collider.gameObject.layer)) == 0)
                return;

            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
                Fitted?.Invoke(enemy.ScoreValue);
            }
            else if (collider.TryGetComponent(out Player player))
            {
                player.TakeDamage(_damage);
            }

            Remove();
        }

        public void SetParameters(Transform firePoint, float speed, int damage)
        {
            _damage = damage;
            _speed = speed;
            _direction = firePoint.right;
            _velocity = _direction * _speed;
            TransformBullet.rotation = firePoint.rotation;
        }

        public void Remove() =>
            Removed?.Invoke(this);
    }
}