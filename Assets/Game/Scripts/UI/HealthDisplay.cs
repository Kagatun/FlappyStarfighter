using System;
using System.Collections.Generic;
using Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _healthImages;
        [SerializeField] private List<Image> _healthHealthyImages;

        private Health _playerHealth;

        private void OnEnable()
        {
            if (_playerHealth != null)
            {
                _playerHealth.Hited += OnTurnOffImageHeart;
                _playerHealth.Deceased += OnTurnOffAllImageHeart;
                _playerHealth.Healed += OnTurnOnHearts;
            }
        }

        private void OnDisable()
        {
            if (_playerHealth.MaxValue > 0 && _playerHealth != null)
            {
                _playerHealth.Hited -= OnTurnOffImageHeart;
                _playerHealth.Deceased -= OnTurnOffAllImageHeart;
                _playerHealth.Healed -= OnTurnOnHearts;
            }
        }

        public void SetHealth(Health health)
        {
            _playerHealth = health;
            _playerHealth.Hited += OnTurnOffImageHeart;
            _playerHealth.Deceased += OnTurnOffAllImageHeart;
            _playerHealth.Healed += OnTurnOnHearts;

            EnabledStartHearts();
        }

        private void OnTurnOnHearts(int healthPoint)
        {
            for (int i = 0; i < _healthHealthyImages.Count && healthPoint > 0; i++)
            {
                if (_healthHealthyImages[i].enabled || !_healthImages[i].activeSelf) 
                    continue;
                
                _healthHealthyImages[i].enabled = true;
                healthPoint--;
            }
        }

        private void EnabledStartHearts()
        {
            for (int i = 0; i < _healthImages.Count; i++)
            {
                _healthImages[i].gameObject.SetActive(i < _playerHealth.MaxValue);

                if (i < _healthHealthyImages.Count)
                    _healthHealthyImages[i].enabled = i < _playerHealth.MaxValue;
            }
        }

        private void OnTurnOffImageHeart(int damage)
        {
            for (int i = _healthHealthyImages.Count - 1; i >= 0 && damage > 0; i--)
            {
                if (_healthHealthyImages!= null &&!_healthHealthyImages[i].enabled)
                    continue;
                
                _healthHealthyImages[i].enabled = false;
                damage--;
            }
        }

        private void OnTurnOffAllImageHeart()
        {
            foreach (var image in _healthHealthyImages)
                image.enabled = false;
        }
    }
}