using Scripts.Enemies;
using UnityEngine;

namespace Scripts.Spawner
{
    public class SpawnerEffect : SpawnerObjects<EffectExplosion>
    {
        [SerializeField] private AudioSource _sound;
        
        public void Spawn(Vector2 spawnPoint)
        {
            EffectExplosion effectExplosion = Get();
            effectExplosion.Activate();
            effectExplosion.transform.position = spawnPoint;
            _sound.Play();
        }

        protected override void OnGet(EffectExplosion enemy)
        {
            base.OnGet(enemy);
            enemy.Removed += OnReturnToPool;
        }

        protected override void OnRelease(EffectExplosion enemy)
        {
            enemy.Removed -= OnReturnToPool;
            base.OnRelease(enemy);
        }

        private void OnReturnToPool(EffectExplosion enemy) =>
            AddToPool(enemy);
    }
}
