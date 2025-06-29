using Scripts.Utils;
using UnityEngine;

namespace Scripts.Systems
{
    public class ZoneRemoveObjects : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IRemovable gameObject))
                gameObject.Remove();
        }
    }
}