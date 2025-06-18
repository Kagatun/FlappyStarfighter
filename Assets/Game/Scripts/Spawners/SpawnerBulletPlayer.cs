using Scripts.Shooting;
using Scripts.Systems;
using UnityEngine;

namespace Scripts.Spawner
{
    public class SpawnerBulletPlayer : SpawnerObjects<Bullet>
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private AudioSource _sound;

        public void Spawn(Transform direction, float speed, int damage)
        {
            Bullet bullet = Get();
            bullet.TransformBullet.position = direction.position;
            bullet.SetParameters(direction, speed, damage);
            _scoreCounter.Subscribe(bullet);
            _sound.Play();
        }

        protected override void OnGet(Bullet enemy)
        {
            base.OnGet(enemy);
            enemy.Removed += OnReturnToPool;
        }

        protected override void OnRelease(Bullet enemy)
        {
            enemy.Removed -= OnReturnToPool;
            base.OnRelease(enemy);
        }

        private void OnReturnToPool(Bullet bullet) =>
            AddToPool(bullet);
    }
}