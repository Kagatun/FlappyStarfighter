using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Scripts.Systems
{
    public class AudioClipInstaller : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private List<AudioClip> _audioClips;

        private void Start()
        {
            int clipIndex = (YG2.saves.LevelNumber - 1) / 5;
            _music.clip = _audioClips[clipIndex];
        }
    }
}