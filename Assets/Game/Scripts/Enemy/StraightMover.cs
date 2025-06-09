using UnityEngine;

namespace Scripts.Enemies
{
    public class StraightMover : IMovement
    {
        private float _speed;
        private Transform _transform;

        public StraightMover(float speed)
        {
            _speed = speed;
        }

        public void Initialize(Transform transform) =>
            _transform = transform;

        public void UpdateMovement() =>
            _transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
        
        public void ResetParameters(Vector2 spawnPosition)
        {
        }
    }
}