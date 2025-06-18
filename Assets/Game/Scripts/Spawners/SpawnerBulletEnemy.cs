using Scripts.Shooting;
using UnityEngine;

namespace Scripts.Spawner
{
    public class SpawnerBulletEnemy : SpawnerObjects<Bullet>
    {
        [SerializeField] private AudioSource _sound;
        
        public void Spawn(Transform firePoint, float speed, int damage)
        {
            Bullet bullet = Get();
            bullet.TransformBullet.position = firePoint.position;
            bullet.SetParameters(firePoint, speed, damage);
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