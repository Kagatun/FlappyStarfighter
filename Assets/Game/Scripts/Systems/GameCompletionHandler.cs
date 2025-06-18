using System.Collections;
using Scripts.Input;
using Scripts.PlayerUFO;
using Scripts.Spawner;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Systems
{
    public class GameCompletionHandler : ButtonHandler
    {
        [SerializeField] private Player _player;
        [SerializeField] private SpawnerEffect _spawnerEffect;
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private EnemyDistributor _enemyDistributor;
        [SerializeField] private EnemyTrackingZone _trackingZone;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private AdRewardManager _adRewardManager;
        [SerializeField] private Image _panelVictory;
        [SerializeField] private Image _panelDefeat;
        [SerializeField] private Image _panelRecovery;
        [SerializeField] private Image _countdownImage;
        [SerializeField] private Button _buttonStopGame;
        [SerializeField] private Button _buttonMenu;
        [SerializeField] private AudioSource _soundVictory;
        [SerializeField] private AudioSource _soundDefeat;
        [SerializeField] private AudioSource _musicLevel;

        Coroutine _coroutineCountdown;
        Coroutine _coroutineDead;
        private bool _isRevived;
        private bool _isDefeated = true;
        private int _countdownTime = 3;
        private int _waitTime = 1;
        private string _leaderboard = "Score";
        private WaitForSeconds _wait;
        private WaitForSecondsRealtime _waitRecovery;

        private void Awake()
        {
            _wait = new WaitForSeconds(_countdownTime);
            _waitRecovery = new WaitForSecondsRealtime(_waitTime);
        }

        protected override void OnButtonClick()
        {
            _isRevived = true;
            _adRewardManager.ShowReviveAd(ContinuePlayingGame);
        }

        protected override void OnEnableAction()
        {
            _trackingZone.NoEnemiesDetected += OpenPanelVictory;
            _player.Death += OnEnableButtonRecovery;
            _buttonStopGame.onClick.AddListener(StopGame);
        }

        protected override void OnDisableAction()
        {
            _trackingZone.NoEnemiesDetected -= OpenPanelVictory;
            _player.Death -= OnEnableButtonRecovery;
            _buttonStopGame.onClick.RemoveListener(StopGame);
        }

        private void ContinuePlayingGame()
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
            _player.Recover();
            _inputDetector.enabled = true;
            _panelRecovery.gameObject.SetActive(false);
        }

        private void OnEnableButtonRecovery()
        {
            if (_isRevived == false)
                StartCoroutine(StartEnableButtonRecovery());
            else
                _coroutineDead = StartCoroutine(HandleGameOver(_panelDefeat, _soundDefeat));
        }

        private IEnumerator HandleGameOver(Image panel, AudioSource sound)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            _buttonMenu.interactable = false;
            _inputDetector.enabled = false;
            _enemyDistributor.StopSpawning();
            _trackingZone.gameObject.SetActive(false);
            sound.Play();
            _musicLevel.Stop();

            YG2.saves.Gold += _scoreCounter.CurrentScore;

            if (_isDefeated)
            {
                _player.gameObject.SetActive(false);
                _spawnerEffect.Spawn(_player.transform.position);
            }
            else
            {
                _player.TurnOnWinningScreensaver();
                YG2.saves.Score += _scoreCounter.CurrentScore;
                YG2.SetLeaderboard(_leaderboard, YG2.saves.Score);

                if (YG2.saves.LevelNumber + 1 == YG2.saves.LevelIndex)
                    YG2.saves.LevelIndex++;
            }

            YG2.SaveProgress();

            yield return _wait;

            OpenPanel(panel);
        }

        private void OpenPanelVictory()
        {
            _isDefeated = false;
            StartCoroutine(HandleGameOver(_panelVictory, _soundVictory));
        }

        private void StopGame()
        {
            _coroutineDead = StartCoroutine(HandleGameOver(_panelDefeat, _soundDefeat));
            _panelRecovery.gameObject.SetActive(false);
        }

        private void OpenPanel(Image panel)
        {
            if (_isDefeated)
                Time.timeScale = 0;

            panel.gameObject.SetActive(true);
            _inputDetector.enabled = false;
        }

        private IEnumerator StartEnableButtonRecovery()
        {
            _inputDetector.enabled = false;
            AudioListener.pause = true;
            Time.timeScale = 0;

            yield return _waitRecovery;

            _panelRecovery.gameObject.SetActive(true);
        }
    }
}