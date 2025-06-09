using Scripts.Input;
using Scripts.PlayerUFO;
using Scripts.Spawner;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Systems
{
    public class StarterGame : ButtonHandler
    {
        [SerializeField] private GameCompletionHandler _gameCompletionHandler;
        [SerializeField] private Material _backgroundMaterial;
        [SerializeField] private InputDetector  _inputDetector;
        [SerializeField] private Player _player;
        [SerializeField] private AudioSource _music;
        [SerializeField] private EnemyDistributor _enemyDistributor;

        private void Awake()
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            
            _music.clip = LevelManager.GameSettings.LevelMusic;
            _backgroundMaterial.color = LevelManager.GameSettings.SpaceColor;
            _gameCompletionHandler.SetIndexLevel(LevelManager.GameSettings.LevelNumber);
        }

        protected override void OnButtonClick()
        {
            _enemyDistributor.SetCountsEnemies(LevelManager.GameSettings.EnemiesCount);
            _enemyDistributor.StartSpawning();
            _inputDetector.enabled = true;
            _music.Play();
            _player.EnableMover();
        }
    }
}
