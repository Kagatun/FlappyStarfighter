using System;
using UnityEngine;
using YG;

namespace Scripts.Systems
{
    public class FocusObserver : MonoBehaviour
    {
        private bool _isPause;

        public static event Action<bool> ApplicationFocus;
        public static event Action<bool> ApplicationPause;

        public static bool HasFocus { get; private set; } = true;

        private void Start()
        {
            HasFocus = YG2.isFocusWindowGame;
        }

        private void OnEnable()
        {
            YG2.onShowWindowGame += OnYandexWindowShown;
            YG2.onHideWindowGame += OnYandexWindowHidden;
        }

        private void OnDisable()
        {
            YG2.onShowWindowGame -= OnYandexWindowShown;
            YG2.onHideWindowGame -= OnYandexWindowHidden;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            UpdateFocusState(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            UpdatePauseState(pauseStatus);
        }

        public static void UpdateFocusState(bool hasFocus)
        {
            if (HasFocus == hasFocus)
                return;

            HasFocus = hasFocus;
            ApplicationFocus?.Invoke(hasFocus);
        }

        private void UpdatePauseState(bool isPaused)
        {
            if (_isPause == isPaused)
                return;

            _isPause = isPaused;
            ApplicationPause?.Invoke(isPaused);
        }

        private void OnYandexWindowShown() =>
            UpdateFocusState(true);

        private void OnYandexWindowHidden() =>
            UpdateFocusState(false);
    }
}