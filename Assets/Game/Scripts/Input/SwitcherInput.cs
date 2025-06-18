using System.Collections.Generic;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Input
{
    public class SwitcherInput : ButtonHandler
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private List<Image> _leftDesktopPanels;
        [SerializeField] private List<Image> _rightDesktopPanels;
        [SerializeField] private List<Image> _leftMobilePanels;
        [SerializeField] private List<Image> _rightMobilePanels;
        [SerializeField] private List<Image> _imagesClickShooting;
        [SerializeField] private Toggle _autoAttackToggle;

        private void Start()
        {
            UpdateUI();
            SetupAutoAttackToggle();
            UpdateShootingImagesVisibility();
        }

        protected override void OnEnableAction() =>
            _autoAttackToggle.onValueChanged.AddListener(OnAutoAttackToggleChanged);

        protected override void OnDisableAction() =>
            _autoAttackToggle.onValueChanged.RemoveListener(OnAutoAttackToggleChanged);

        protected override void OnButtonClick()
        {
            YG2.saves.IsLeft = !YG2.saves.IsLeft;

            if (_inputDetector != null)
                _inputDetector.UpdateInputHandler();

            UpdateUI();
            YG2.SaveProgress();
        }

        private void SetupAutoAttackToggle()
        {
            if (_autoAttackToggle == null)
                return;

            _autoAttackToggle.isOn = YG2.saves.IsAutoAttack;
        }

        private void OnAutoAttackToggleChanged(bool isOn)
        {
            YG2.saves.IsAutoAttack = isOn;

            if (_inputDetector != null)
                _inputDetector.SetAutoAttack(isOn);

            UpdateShootingImagesVisibility();
            YG2.SaveProgress();
        }

        private void UpdateShootingImagesVisibility()
        {
            if (_imagesClickShooting == null) return;

            bool shouldShow = YG2.saves.IsAutoAttack == false;

            foreach (var image in _imagesClickShooting)
            {
                if (image != null)
                    image.gameObject.SetActive(shouldShow);
            }
        }

        private void UpdateUI()
        {
            bool isDesktop = YG2.saves.IsDesktop;
            bool isLeft = YG2.saves.IsLeft;

            SetPanelsActive(_leftDesktopPanels, isDesktop && isLeft);
            SetPanelsActive(_rightDesktopPanels, isDesktop && !isLeft);

            SetPanelsActive(_leftMobilePanels, !isDesktop && isLeft);
            SetPanelsActive(_rightMobilePanels, !isDesktop && !isLeft);
        }

        private void SetPanelsActive(List<Image> panels, bool state)
        {
            if (panels == null)
                return;

            foreach (var panel in panels)
            {
                if (panel != null)
                    panel.gameObject.SetActive(state);
            }
        }
    }
}