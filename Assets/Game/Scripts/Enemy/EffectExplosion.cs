using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EffectExplosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private Coroutine _trackingCoroutine;
        private float _pausedTimeRemaining;
        private bool _isPaused;

        public event Action<EffectExplosion> Removed;
        
       public Transform TransformEffect {get; private set;}

        private void Awake()
        {
            TransformEffect =  transform;
        }

        private void OnEnable()
        {
            _particleSystem.Play();
            _trackingCoroutine = StartCoroutine(TrackParticleSystem());
        }

        private void OnDisable()
        {
            if (_trackingCoroutine != null)
            {
                StopCoroutine(_trackingCoroutine);
                _trackingCoroutine = null;
            }
        }

        public void Activate() =>
            _particleSystem.Play();

        private IEnumerator TrackParticleSystem()
        {
            float totalDuration = _particleSystem.main.duration;
            float elapsed = 0f;

            while (elapsed < totalDuration)
            {
                if (Time.timeScale > 0f)
                {
                    elapsed += Time.deltaTime;
                    _isPaused = false;
                }
                else
                {
                    if (_isPaused == false)
                    {
                        _particleSystem.Pause(true);
                        _isPaused = true;
                    }
                }

                yield return null;
            }

            if (Time.timeScale <= 0f)
                _particleSystem.Play(true);

            Removed?.Invoke(this);
        }
    }
}