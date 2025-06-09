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
        [SerializeField] private AudioSource _soundVictory;
        [SerializeField] private AudioSource _soundDefeat;
        [SerializeField] private AudioSource _musicLevel;

        Coroutine _coroutineCountdown;
        Coroutine _coroutineDead;
        private bool _isRevived;
        private int _countdownTime = 3;
        private int _currentLevelIndex;
        private WaitForSecondsRealtime _wait;

        private void Awake()
        {
            _wait = new WaitForSecondsRealtime(_countdownTime);
        }

        public void SetIndexLevel(int indexLevel) =>
            _currentLevelIndex = indexLevel;

        protected override void OnButtonClick()
        {
            _isRevived = true;
            StopCountdown();
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
            _player.Recover();
           // _musicLevel.UnPause();
            Time.timeScale = 1;
            _inputDetector.enabled = true;
            _panelRecovery.gameObject.SetActive(false);
        }

        private void OnEnableButtonRecovery()
        {
            if (_isRevived == false)
            {
               // _panelRecovery.gameObject.SetActive(true);
               OpenPanel(_panelRecovery);
               //_coroutineCountdown = StartCoroutine(CountdownRoutine());
            }
            else
            {
                OpenPanelDefeat();
            }
        }

        private void StopCountdown()
        {
            if (_coroutineCountdown != null)
                StopCoroutine(_coroutineCountdown);
        }

        /*private IEnumerator CountdownRoutine()
        {
            _inputDetector.enabled = false;
            Time.timeScale = 0;
            _musicLevel.Pause();
            float timer = _countdownTime;

            while (timer > 0)
            {
                timer -= Time.unscaledDeltaTime;
                _countdownImage.fillAmount = timer / _countdownTime;

                yield return null;
            }

            OpenPanelDefeat();
        }*/

        private IEnumerator EnabledPanelDefeat()
        {
            _inputDetector.enabled = false;
            _enemyDistributor.StopSpawning();
            _trackingZone.gameObject.SetActive(false);
            _soundDefeat.Play();
            _musicLevel.Stop();
            _player.gameObject.SetActive(false);
            _spawnerEffect.Spawn(_player.transform.position);

            yield return _wait;

            OpenPanel(_panelDefeat);
            YG2.saves.Score += _scoreCounter.CurrentScore;
            Time.timeScale = 0;
        }

        private void OpenPanelVictory() =>
            StartCoroutine(EnabledPanelVictory());

        private IEnumerator EnabledPanelVictory()
        {
            _inputDetector.enabled = false;
            _trackingZone.gameObject.SetActive(false);
            _player.TurnOnWinningScreensaver();
            _soundVictory.Play();
            _musicLevel.Stop();

            yield return _wait;

            OpenPanel(_panelVictory);
            YG2.saves.Score += _scoreCounter.CurrentScore;
            Time.timeScale = 0;

            if (_currentLevelIndex == YG2.saves.LevelIndex)
                YG2.saves.LevelIndex++;
        }

        private void OpenPanelDefeat() =>
            _coroutineDead = StartCoroutine(EnabledPanelDefeat());

        private void StopGame()
        {
            OpenPanelDefeat();
            StopCountdown();
            _panelRecovery.gameObject.SetActive(false);
        }

        private void OpenPanel(Image panel)
        {
            panel.gameObject.SetActive(true);
            _inputDetector.enabled = false;
            Time.timeScale = 0;
        }
    }
}

