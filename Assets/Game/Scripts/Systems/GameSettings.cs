using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Systems
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [FormerlySerializedAs("levelNumber")] public int LevelNumber;
        public Color SpaceColor;
        public AudioClip LevelMusic;
        public List<int> EnemiesCount;
    }
}