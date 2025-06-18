using System.Collections.Generic;
using Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Shop
{
    public class Improvement : ButtonHandler
    {
        [SerializeField] private UpgradeSaveField _upgradeSaveField;
        [SerializeField] private List<Image> _starsLevels;
        [SerializeField] private List<TextMeshProUGUI> _specifications;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private GoldView _goldView;
        [SerializeField] private AudioSource _audioBuy;
        [SerializeField] private Animator _animator;

        private readonly List<int> _pricesTool = new List<int> { 150, 500, 1000, 2500, 5000 };
        private int _indexSave;
        private Vector3 _defaultScaleButton = Vector3.one;

        private bool IsMaxLevel => _indexSave >= _starsLevels.Count;
        private int LastVisibleSpecIndex => Mathf.Min(_indexSave, _specifications.Count - 1);

        private void Start()
        {
            _indexSave = GetCurrentUpgradeLevel();
            UpdateVisuals();
            OnUpdateAnimator();
        }

        protected override void OnButtonClick()
        {
            if (IsMaxLevel || YG2.saves.Gold < _pricesTool[_indexSave])
                return;

            YG2.saves.Gold -= _pricesTool[_indexSave];
            IncreaseUpgradeLevel();
            UpdateVisuals();
            _audioBuy.Play();
            _goldView.ShowGold();
            YG2.SaveProgress();
        }

        private void UpdateVisuals()
        {
            for (int i = 0; i < _starsLevels.Count; i++)
                _starsLevels[i].gameObject.SetActive(i < _indexSave);

            for (int i = 0; i < _specifications.Count; i++)
                _specifications[i].gameObject.SetActive(i == LastVisibleSpecIndex);

            _priceText.gameObject.SetActive(!IsMaxLevel);
            ActionButton.gameObject.SetActive(!IsMaxLevel);

            if (!IsMaxLevel)
                _priceText.text = _pricesTool[_indexSave].ToString();
        }

        protected override void OnEnableAction() =>
            _goldView.GoldChanged += OnUpdateAnimator;

        protected override void OnDisableAction() =>
            _goldView.GoldChanged -= OnUpdateAnimator;

        private void OnUpdateAnimator()
        {
            if (IsMaxLevel || YG2.saves.Gold < _pricesTool[_indexSave])
            {
                _animator.enabled = false;
                ActionButton.transform.localScale = _defaultScaleButton;

                return;
            }

            _animator.enabled = YG2.saves.Gold >= _pricesTool[_indexSave];
        }

        private int GetCurrentUpgradeLevel()
        {
            var fieldInfo = typeof(SavesYG).GetField(_upgradeSaveField.ToString());

            return fieldInfo != null ? (int)fieldInfo.GetValue(YG2.saves) : 0;
        }

        private void IncreaseUpgradeLevel()
        {
            var fieldInfo = typeof(SavesYG).GetField(_upgradeSaveField.ToString());

            if (fieldInfo == null || IsMaxLevel)
                return;

            int currentValue = (int)fieldInfo.GetValue(YG2.saves);
            fieldInfo.SetValue(YG2.saves, currentValue + 1);
            _indexSave++;
        }
    }
}