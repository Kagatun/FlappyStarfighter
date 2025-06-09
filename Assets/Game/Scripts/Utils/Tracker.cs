using UnityEngine;

namespace Scripts.Utils
{
    public class Tracker : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _offsetX;

        private void Update()
        {
            Vector3 position = transform.position;
            position.x = _player.transform.position.x + _offsetX;
            transform.position = position;
        }
    }
}