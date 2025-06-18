using System;
using UnityEngine;
using YG;

namespace Scripts.PlayerUFO
{
    public class PlayerShield : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _shieldMaterial;
        [SerializeField] private AudioSource _soundShieldActivated;
        [SerializeField] private int _cooldown = 13;
        
        private float _currentTime;

        public bool HasRestored { get; private set; } = true;

        private void Start()
        {
            _cooldown -= YG2.saves.CooldownShield;
        }

        private void Update()
        {
            if (HasRestored)
                return;

            _currentTime += Time.deltaTime;

            if (!(_currentTime >= _cooldown))
                return;

            EnableProtection();
        }

        public void DisableProtection()
        {
            HasRestored = false;
            _renderer.material = _defaultMaterial;
            _currentTime = 0;
        }

        public void EnableProtection()
        {
            HasRestored = true;
            _renderer.material = _shieldMaterial;
            _soundShieldActivated.Play();
            _currentTime = 0;
        }
    }
}
