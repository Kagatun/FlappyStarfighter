using UnityEngine;

namespace Scripts.Utils
{
    public class Tracker : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _offsetX;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            Vector3 position = _transform.position;
            position.x = _player.position.x + _offsetX;
            _transform.position = position;
        }
    }
}