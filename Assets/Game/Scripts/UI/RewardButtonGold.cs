using Scripts.Shop;
using Scripts.Systems;
using TMPro;
using UnityEngine;
using YG;

namespace Scripts.UI
{
    public class RewardButtonGold : ButtonHandler
    {
        [SerializeField] private TextMeshProUGUI _textGold;
        [SerializeField] private AdRewardManager _adRewardManager;
        [SerializeField] private GoldView  _goldView;

        private readonly int _rewardGold = 100;

        protected override void OnButtonClick()
        {
            _adRewardManager.ShowReviveAd(GetReward);
        }

        private void GetReward()
        {
            YG2.saves.Gold += _rewardGold;
            _textGold.text = $"{YG2.saves.Gold}";
            ActionButton.gameObject.SetActive(false);
            _goldView.ShowGold();
            YG2.SaveProgress();
        }
    }
}