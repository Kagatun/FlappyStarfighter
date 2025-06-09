using Scripts.Input;
using UnityEngine;

namespace Scripts.PlayerUFO
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private float _tapForce;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxRotationZ;
        [SerializeField] private float _minRotationZ;

        public Rigidbody2D Rigidbody { get; private set; }
        private Quaternion _maxRotation;
        private Quaternion _minRotation;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();

            _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
            _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
        }

        private void FixedUpdate()
        {
            Fall();
        }

        private void OnEnable()
        {
            _inputDetector.Jumped += OnTakeOff;
        }

        private void OnDisable()
        {
            _inputDetector.Jumped -= OnTakeOff;
        }

        private void OnTakeOff()
        {
            Rigidbody.velocity = new Vector2(_speed, _tapForce);
            transform.rotation = _maxRotation;
        }

        private void Fall() =>
            transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}