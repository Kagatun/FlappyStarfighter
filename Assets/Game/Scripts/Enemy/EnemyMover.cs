using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _zigZagAmplitude;
        [SerializeField] private float _zigZagFrequency = 1.5f;

        private Transform _transform;
        private float _initialY;
        private float _time;
        private Vector3 _movementDelta;
        private float _deltaRotation;

        private void Awake()
        {
            _transform = transform;
            _movementDelta = Vector3.zero;
        }

        private void Update()
        {
            _movementDelta.x = -_moveSpeed * Time.deltaTime;
            _movementDelta.y = 0f;
            _movementDelta.z = 0f;

            if (_zigZagAmplitude > 0)
            {
                _time += Time.deltaTime;
                _movementDelta.y = (Mathf.Sin(_time * _zigZagFrequency) * _zigZagAmplitude) - (_transform.position.y - _initialY);
            }

            _deltaRotation = _rotationSpeed * Time.deltaTime;
            _transform.position += _movementDelta;

            if (_zigZagAmplitude > 0)
                _transform.Rotate(0, 0, _deltaRotation);
        }

        public void Reset(Vector2 spawnPosition)
        {
            _initialY = spawnPosition.y;
            _time = 0;
            _transform.position = spawnPosition;
        }
    }
}