using System;
using UnityEngine;
using YG;

namespace Scripts.Input
{
    public class InputDetector : MonoBehaviour
    {
        private IInputHandler _inputHandler;
        private bool _isTabPressed;
        private bool _isAutoAttackEnabled;

        public event Action Jumped;
        public event Action Fired;
        public event Action TabPressed;
        public event Action ChangedAttackMode;

        private void Start()
        {
            UpdateInputHandler();
            _isAutoAttackEnabled = YG2.saves.IsAutoAttack;
        }

        private void LateUpdate()
        {
            if (_isTabPressed == false)
                _inputHandler.Update();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
                TabPressed?.Invoke();
        }

        public void UpdateInputHandler()
        {
            _inputHandler = YG2.saves.IsDesktop
                ? new DesktopInputHandler(YG2.saves.IsLeft)
                : new MobileInputHandler(YG2.saves.IsLeft);

            _inputHandler.Jumped += () => Jumped?.Invoke();
            _inputHandler.Fired += () =>
            {
                if (_isAutoAttackEnabled == false)
                    Fired?.Invoke();
            };
        }

        public void SetAutoAttack(bool isEnabled)
        {
            _isAutoAttackEnabled = isEnabled;
            YG2.saves.IsAutoAttack = isEnabled;
            ChangedAttackMode?.Invoke();
            YG2.SaveProgress();
        }

        public void SetStatusPressed() =>
            _isTabPressed = true;

        public void SetStatusNotPressed() =>
            _isTabPressed = false;
    }
}