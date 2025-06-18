using Scripts.UI;
using TMPro;
using UnityEngine;
using YG;

namespace Scripts.Systems
{
    public class RewardDoublingButton : ButtonHandler
    {
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private AdRewardManager _adRewardManager;
        [SerializeField] private ScoreCounter _scoreCounter;

        protected override void OnEnableAction()
        {
            if (_scoreCounter.CurrentScore > 0)
                _goldText.text = $"{_scoreCounter.CurrentScore}";
            else
                ActionButton.gameObject.SetActive(false);
        }

        protected override void OnButtonClick()
        {
            ActionButton.gameObject.SetActive(false);
            _adRewardManager.ShowReviveAd(GetReward);
        }

        private void GetReward()
        {
            int currentGold = _scoreCounter.CurrentScore;
            YG2.saves.Gold += _scoreCounter.CurrentScore;
            currentGold += _scoreCounter.CurrentScore;
            _goldText.text = $"{currentGold}";
            YG2.SaveProgress();
        }
    }
}