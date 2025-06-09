using UnityEngine;

namespace Scripts.Enemies
{
    public class ZigZagMover : IMovement
    {
        private Transform _transform;
        private float _amplitude;
        private float _frequency;
        private float _startY;
        private float _currentTime;

        public ZigZagMover(float amplitude, float frequency)
        {
            _amplitude = amplitude;
            _frequency = frequency;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
            _startY = transform.position.y;
            _currentTime = 0;
        }

        public void UpdateMovement()
        {
            _currentTime += Time.deltaTime;
            float waveOffset = Mathf.Sin(_currentTime * _frequency) * _amplitude;
            _transform.position = new Vector2(_transform.position.x, _startY + waveOffset);
        }

        public void ResetParameters(Vector2 spawnPosition)
        {
            _transform.position = spawnPosition;
            _startY = spawnPosition.y;
            _currentTime = 0;
        }
    }
}