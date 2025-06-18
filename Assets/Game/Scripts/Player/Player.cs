using System;
using System.Collections;
using Scripts.UI;
using Scripts.Utils;
using UnityEngine;
using YG;

namespace Scripts.PlayerUFO
{
    public class Player : MonoBehaviour, IDestroyable, IDamageable
    {
        [SerializeField] private PlayerAttacker _attacker;
        [SerializeField] private PlayerShield _shield;
        [SerializeField] private PlayerLaser _laser;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private HealthDisplay _healthDisplay;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private int _startMaxHealth;
        [SerializeField] private AudioSource _hitSound;
        [SerializeField] private LayerMask _layerMaskDefault;

        private Transform _transform;
        private Health _health;
        private int _timeInvulnerability = 3;
        private WaitForSeconds _wait;
        private LayerMask _layerMaskPlayer;

        public event Action Death;

        private void Awake()
        {
            _layerMaskPlayer = gameObject.layer;
            _transform = transform;
            _wait = new WaitForSeconds(_timeInvulnerability);
        }

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

        public void StartAttack()
        {
            _attacker.enabled = true;
            _attacker.OnAttackModeChanged();
        }

        public void TakeDamage(int damage)
        {
            _hitSound.Play();

            if (_shield.HasRestored)
                _shield.DisableProtection();
            else
                _health.TakeDamage(damage);
        }

        public void OnDestroy()
        {
            DisableMover();
            _laser.enabled = false;
            int fatalDamage = 10;
            _health.TakeDamage(fatalDamage);
        }

        public void Recover()
        {
            StartCoroutine(StartLayerChange());
            _laser.enabled = true;
            _laser.ResetLaser();
            _shield.EnableProtection();
            EnableMover();
            _transform.position = _startPosition.position;
            _transform.rotation = _startPosition.rotation;
            _health.Reset();
        }

        public void EnableMover()
        {
            _shield.EnableProtection();
            _collider.enabled = true;
            _playerMover.enabled = true;
            _playerMover.Rigidbody.gravityScale = 1;
        }

        public void TurnOnWinningScreensaver()
        {
            _attacker.StopAutoAttack();
            _shield.enabled = false;
            _collider.enabled = false;
            _transform.rotation = Quaternion.Euler(0, 0, 0);
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

        private IEnumerator StartLayerChange()
        {
            gameObject.layer = _layerMaskDefault;

            yield return _wait;

            gameObject.layer = _layerMaskPlayer;
        }
    }
}