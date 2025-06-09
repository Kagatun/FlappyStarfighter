using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private MovementType _movementType;
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _rotationSpeed = 50f;
        [SerializeField] private float _zigZagAmplitude = 1.5f;
        [SerializeField] private float _zigZagFrequency = 1.5f;

        private List<IMovement> _activeMovements = new List<IMovement>();

        private void Awake()
        {
            InitializeMovements();
        }

        private void Update()
        {
            foreach (var movement in _activeMovements)
                movement.UpdateMovement();
        }
        
        private void InitializeMovements()
        {
            if (_movementType.HasFlag(MovementType.Straight))
                _activeMovements.Add(new StraightMover(_moveSpeed));

            if (_movementType.HasFlag(MovementType.Rotating))
                _activeMovements.Add(new RotatingMover(_rotationSpeed));

            if (_movementType.HasFlag(MovementType.ZigZag))
                _activeMovements.Add(new ZigZagMover(_zigZagAmplitude, _zigZagFrequency));

            foreach (var movement in _activeMovements)
                movement.Initialize(transform);
        }
        
        public void ResetParameters(Vector2 spawnPosition)
        {
            foreach (var movement in _activeMovements)
                movement.ResetParameters(spawnPosition);
        }
    }
}
