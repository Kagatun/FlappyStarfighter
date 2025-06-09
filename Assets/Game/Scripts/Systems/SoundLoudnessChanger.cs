using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using YG;

namespace Scripts.Systems
{
    public class SoundLoudnessChanger : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixerMaster;
        [SerializeField] private List<SoundSlider> _soundSliders;

        private void Start()
        {
            if (YG2.isSDKEnabled)
                OnLoadVolumes();
        }

        private void OnEnable()
        {
            foreach (var slider in _soundSliders)
                slider.Slider.onValueChanged.AddListener(value => OnVolumeChanged(slider.SaveField, value));

            YG2.onGetSDKData += OnLoadVolumes;
        }

        private void OnDisable()
        {
            foreach (var slider in _soundSliders)
                slider.Slider.onValueChanged.RemoveAllListeners();

            YG2.onGetSDKData -= OnLoadVolumes;
        }

        private void OnVolumeChanged(SoundSaveField saveField, float volume)
        {
            string mixerParam = saveField.ToString();
            float dBValue = Mathf.Log10(Mathf.Clamp(volume, 0.00001f, 1f)) * 20;
            
            _mixerMaster.audioMixer.SetFloat(mixerParam, dBValue);
            var field = typeof(SavesYG).GetField(mixerParam);

            if (field == null || field.FieldType != typeof(float)) 
                return;
            
            field.SetValue(YG2.saves, volume);
            YG2.SaveProgress();
        }

        private void OnLoadVolumes()
        {
            foreach (var slider in _soundSliders)
            {
                if (slider.Slider == null) 
                    continue;

                string fieldName = slider.SaveField.ToString();
                var field = typeof(SavesYG).GetField(fieldName);

                if (field == null || field.FieldType != typeof(float))
                    continue;
                
                float savedValue = (float)field.GetValue(YG2.saves);
                slider.Slider.value = savedValue;
                OnVolumeChanged(slider.SaveField, savedValue);
            }
        }
    }
}