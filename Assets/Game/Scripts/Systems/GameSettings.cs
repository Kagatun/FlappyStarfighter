using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private Color _spaceColor;
        [SerializeField] private List<bool> _typesEnemies;
        [SerializeField] private int _enemiesCount;

        public Color SpaceColor => _spaceColor;
        public IReadOnlyList<bool> TypesEnemies => _typesEnemies;
        public int EnemiesCount => _enemiesCount;
    }
}