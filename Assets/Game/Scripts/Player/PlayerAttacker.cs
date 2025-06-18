using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Input;
using Scripts.Spawner;
using UnityEngine;
using YG;

namespace Scripts.PlayerUFO
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private SpawnerBulletPlayer _spawnerBulletPlayer;
        [SerializeField] private List<Transform> _firepoints;
        [SerializeField] private int _damage;
        [SerializeField] private int _criticalDamage;
        [SerializeField] private int _speed;
        [SerializeField] private int _chanceMultiShot;
        [SerializeField] private float _timeCooldownShot;

        private int _maxChance = 100;
        private int _minChance = 0;

        private Coroutine _coroutineShooting;
        private WaitForSeconds _waitShot;

        private void Awake()
        {
            _waitShot = new WaitForSeconds(_timeCooldownShot);
        }

        private void Start()
        {
            _chanceMultiShot += YG2.saves.PercentageMultiShooting;
        }

        private void OnEnable()
        {
            _inputDetector.Fired += OnShoot;
            _inputDetector.ChangedAttackMode += OnAttackModeChanged;
        }

        private void OnDisable()
        {
            _inputDetector.Fired -= OnShoot;
            _inputDetector.ChangedAttackMode -= OnAttackModeChanged;
            StopAutoAttack();
        }

        public void OnAttackModeChanged()
        {
            if (YG2.saves.IsAutoAttack)
                StartAutoAttack();
            else
                StopAutoAttack();
        }
        
        public void StopAutoAttack()
        {
            if (_coroutineShooting == null)
                return;

            StopCoroutine(_coroutineShooting);
        }

        private void OnShoot()
        {
            if (YG2.saves.IsAutoAttack == false)
                PerformShoot();
        }

        private void PerformShoot()
        {
            if (_chanceMultiShot >= UnityEngine.Random.Range(_minChance, _maxChance))
            {
                _spawnerBulletPlayer.Spawn(_firepoints[0], _speed, _criticalDamage);

                for (int i = 1; i < _firepoints.Count; i++)
                    _spawnerBulletPlayer.Spawn(_firepoints[i].transform, _speed, _damage);
            }
            else
            {
                _spawnerBulletPlayer.Spawn(_firepoints[0], _speed, _damage);
            }
        }

        private void StartAutoAttack()
        {
            StopAutoAttack();
            _coroutineShooting = StartCoroutine(AutoShootingCoroutine());
        }

        private IEnumerator AutoShootingCoroutine()
        {
            while (true)
            {
                PerformShoot();

                yield return _waitShot;
            }
        }
    }
}