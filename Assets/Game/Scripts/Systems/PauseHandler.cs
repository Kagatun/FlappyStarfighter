using Scripts.Input;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Systems
{
    public class PauseHandler : ButtonHandler
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private Image _panelPause;
        [SerializeField] private Image _panelInterface;
        [SerializeField] private Button _buttonPlay;

        protected override void OnEnableAction()
        {
            _inputDetector.TabPressed += OnButtonClick;
            _buttonPlay.onClick.AddListener(OnButtonClick);
        }

        protected override void OnDisableAction()
        {
            _inputDetector.TabPressed -= OnButtonClick;
            _buttonPlay.onClick.RemoveListener(OnButtonClick);
        }

        protected override void OnButtonClick()
        {
            if (_panelPause.gameObject.activeSelf == false)
                PauseGame();
            else
                UnPauseGame();
        }

        private void UnPauseGame()
        {
            AudioListener.pause = false;
            Time.timeScale = 1.0f;
            _inputDetector.SetStatusNotPressed();
            _panelPause.gameObject.SetActive(false);
            _panelInterface.gameObject.SetActive(true);
        }

        private void PauseGame()
        {
            _panelPause.gameObject.SetActive(true);
            _panelInterface.gameObject.SetActive(false);
            _inputDetector.SetStatusPressed();
            Time.timeScale = 0;
            AudioListener.pause = true;
            YG2.InterstitialAdvShow();
        }
    }
}