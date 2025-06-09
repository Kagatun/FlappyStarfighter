using System;
using Scripts.UI;
using Scripts.Utils;
using UnityEngine;
using YG;

namespace Scripts.PlayerUFO
{
    public class Player : MonoBehaviour, IDestroyable, IDamageable
    {
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private PolygonCollider2D _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private HealthDisplay _healthDisplay;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private int _startMaxHealth;
        [SerializeField] private AudioSource _hitSound;

        private Health _health;

        public event Action Death;

        private void Start()
        {
            _health = new Health(_startMaxHealth + YG2.saves.MaxHitPoints);
            _healthDisplay.SetHealth(_health);
            _health.Deceased += OnDie;
        }

        private void OnEnable()
        {
            if (_health != null)
                _health.Deceased += OnDie;
        }

        private void OnDisable()
        {
            if (_healthDisplay != null)
                _healthDisplay.gameObject.SetActive(false);

            _health.Deceased -= OnDie;
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            _hitSound.Play();
        }

        public void OnDestroy()
        {
            DisableMover();
            int fatalDamage = 10;
            _health.TakeDamage(fatalDamage);
        }

        public void Recover()
        {
            EnableMover();
            transform.position = _startPosition.position;
            transform.rotation = _startPosition.rotation;
            _health.Reset();
        }

        public void EnableMover()
        {
            _collider.enabled = true;
            _playerMover.enabled = true;
            _playerMover.Rigidbody.gravityScale = 1;
        }

        public void TurnOnWinningScreensaver()
        {
            _collider.enabled = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _renderer.color = Color.green;
            DisableMover();
        }

        private void DisableMover()
        {
            _collider.enabled = false;
            _playerMover.Rigidbody.gravityScale = 0;
            _playerMover.Rigidbody.velocity = Vector2.zero;
            _playerMover.enabled = false;
        }

        private void OnDie()
        {
            DisableMover();
            Death?.Invoke();
        }
    }
}