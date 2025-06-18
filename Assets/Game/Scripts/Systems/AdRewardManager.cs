using System;
using UnityEngine;
using YG;

namespace Scripts.Systems
{
    public class AdRewardManager : MonoBehaviour
    {
        public event Action RewardCallback;

        private void OnEnable()
        {
            YG2.onCloseAnyAdv += OnUnPause;
        }

        private void OnDisable()
        {
            YG2.onCloseAnyAdv -= OnUnPause;
        }

        public void ShowReviveAd(Action onRewardCallback)
        {
            RewardCallback = onRewardCallback;
            YG2.RewardedAdvShow("revive", OnAdCompleted);
        }

        private void OnUnPause()
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            FocusObserver.UpdateFocusState(true);
        }

        private void OnAdCompleted()
        {
            RewardCallback?.Invoke();
            RewardCallback = null;
            FocusObserver.UpdateFocusState(true);
        }

        private void OnDestroy()
        {
            RewardCallback = null;
        }
    }
}