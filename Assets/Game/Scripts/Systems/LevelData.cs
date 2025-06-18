using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Scripts.Systems
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private List<GameSettings> _settings;

        public GameSettings LevelSettings { get; private set; }

        public void SetLevelData() =>
            LevelSettings = _settings[YG2.saves.LevelNumber];
    }
}