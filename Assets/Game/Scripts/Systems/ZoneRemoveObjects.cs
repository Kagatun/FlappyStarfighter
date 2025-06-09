using UnityEngine;

namespace Scripts.Systems
{
    public class ZoneRemoveObjects : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDestroyable gameObject))
                gameObject.OnDestroy();
        }
    }
}
