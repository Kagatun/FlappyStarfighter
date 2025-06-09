using System;
using UnityEngine;
using YG;

namespace Scripts.Systems
{
    public class AdRewardManager : MonoBehaviour
    {
        private Action _onRewardCallback;

        public void ShowReviveAd(Action onRewardCallback)
        {
            _onRewardCallback = onRewardCallback;
            YG2.RewardedAdvShow("revive", OnAdCompleted);
        }

        private void OnAdCompleted()
        {
            _onRewardCallback?.Invoke();
            _onRewardCallback = null;
        }
        
        private void OnDestroy()
        {
            _onRewardCallback = null;
        }
    }
}