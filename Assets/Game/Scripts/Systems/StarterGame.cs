using Scripts.Input;
using Scripts.PlayerUFO;
using Scripts.Spawner;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Systems
{
    public class StarterGame : ButtonHandler
    {
        [SerializeField] private LevelData _levelData;
        [SerializeField] private Button _buttonNextLevel;
        [SerializeField] private Material _backgroundMaterial;
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private Player _player;
        [SerializeField] private AudioSource _music;
        [SerializeField] private EnemyDistributor _enemyDistributor;
        [SerializeField] private Image _startPanel;

        private int _maxLevelGame = 49;
        
        private void Start()
        {
            _levelData.SetLevelData();
            _backgroundMaterial.color = _levelData.LevelSettings.SpaceColor;
            _enemyDistributor.SetEnemies(_levelData.LevelSettings.TypesEnemies, _levelData.LevelSettings.EnemiesCount);

            if (_maxLevelGame == YG2.saves.LevelNumber)
                _buttonNextLevel.gameObject.SetActive(false);
        }

        protected override void OnButtonClick()
        {
            _startPanel.gameObject.SetActive(false);
            _enemyDistributor.StartSpawning();
            _inputDetector.enabled = true;
            _music.Play();
            _player.EnableMover();
            _player.StartAttack();
        }
    }
}