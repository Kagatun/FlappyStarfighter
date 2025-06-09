using UnityEngine;

namespace Scripts.Enemies
{
    public class RotatingMover : IMovement
    {
        private float _rotationSpeed;
        private Transform _transform;

        public RotatingMover(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void UpdateMovement()
        {
            _transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
        }

        public void ResetParameters(Vector2 spawnPosition)
        {
        }
    }
}