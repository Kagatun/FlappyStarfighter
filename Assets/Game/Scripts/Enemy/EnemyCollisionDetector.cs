using Scripts.PlayerUFO;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemyCollisionDetector : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
                player.TakeDamage(_damage);
        }
    }
}