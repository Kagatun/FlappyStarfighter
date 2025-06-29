using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Systems
{
    [Serializable]
    public class SoundSlider
    {
        [SerializeField] private SoundSaveField _saveField;
        [SerializeField] private Slider _slider;

        public SoundSaveField SaveField => _saveField;
        public Slider Slider => _slider;
    }
}