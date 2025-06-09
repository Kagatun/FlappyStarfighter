using System;
using UnityEngine;
using YG;

namespace Scripts.Input
{
    public class InputDetector : MonoBehaviour
    {
        private IInputHandler _inputHandler;
        private bool _isTabPressed;

        public event Action Jumped;
        public event Action Fired;
        public event Action TabPressed;

        private void Start()
        {
            _inputHandler = YG2.saves.IsDesktop ? new DesktopInputHandler() : new MobileInputHandler();

            _inputHandler.Jumped += () =>
                Jumped?.Invoke();
            _inputHandler.Fired += () =>
                Fired?.Invoke();
        }

        private void Update()
        {
            if (_isTabPressed == false)
                _inputHandler.Update();
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
                    TabPressed?.Invoke();
        }

        public void SetStatusPressed() =>
            _isTabPressed = true;

        public void SetStatusNotPressed() =>
            _isTabPressed = false;
    }
}