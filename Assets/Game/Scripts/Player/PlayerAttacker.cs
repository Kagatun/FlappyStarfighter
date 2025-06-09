using Scripts.Input;
using Scripts.Spawner;
using UnityEngine;

namespace Scripts.PlayerUFO
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private SpawnerBulletPlayer spawnerBulletPlayer;
        [SerializeField] private Transform _firepoint;
        [SerializeField] private int _damage;
        [SerializeField] private int _speed;

        private void OnEnable()
        {
            _inputDetector.Fired += OnShoot;
        }

        private void OnDisable()
        {
            _inputDetector.Fired -= OnShoot;
        }

        private void OnShoot() =>
            spawnerBulletPlayer.Spawn(_firepoint, _speed , _damage);
    }
}