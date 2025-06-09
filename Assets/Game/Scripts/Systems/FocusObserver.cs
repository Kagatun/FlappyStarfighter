using System;
using UnityEngine;
using YG;

namespace Scripts.Systems
{
    public class FocusObserver : MonoBehaviour
    {
        public static event Action<bool> ApplicationFocus;
        public static event Action<bool> ApplicationPause;
        public event Action ApplicationQuit;

        public static bool HasFocus { get; private set; } = true;
        public static bool IsPause { get; private set; }
        public static bool IsTransitioning { get; set; }

        private void OnEnable()
        {
            YG2.onPauseGame += OnYandexVisibilityChanged;
            YG2.onShowWindowGame += OnYandexWindowShown;
            YG2.onHideWindowGame += OnYandexWindowHidden;
        }

        private void OnDisable()
        {
            YG2.onPauseGame -= OnYandexVisibilityChanged;
            YG2.onShowWindowGame -= OnYandexWindowShown;
            YG2.onHideWindowGame -= OnYandexWindowHidden;
        }

        private void OnYandexVisibilityChanged(bool visible)
        {
            UpdateFocusState(visible);
        }

        private void OnYandexWindowShown()
        {
            UpdateFocusState(true);
        }

        private void OnYandexWindowHidden()
        {
            UpdateFocusState(false);
        }

        public static void UpdateFocusState(bool hasFocus)
        {
            if (HasFocus == hasFocus) return;

            HasFocus = hasFocus;
            ApplicationFocus?.Invoke(hasFocus);
        }

        public static void UpdatePauseState(bool isPaused)
        {
            if (IsPause == isPaused) return;

            IsPause = isPaused;
            ApplicationPause?.Invoke(isPaused);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            UpdateFocusState(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            UpdatePauseState(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            ApplicationQuit?.Invoke();
        }
    }
}