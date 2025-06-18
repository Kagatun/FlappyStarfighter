using System;
using UnityEngine;

namespace Scripts.Input
{
    public class DesktopInputHandler : IInputHandler
    {
        public event Action Jumped;
        public event Action Fired;

        private readonly int _jumpButton;
        private readonly int _fireButton;

        public DesktopInputHandler(bool isLeftControl)
        {
            _jumpButton = isLeftControl ? 0 : 1;
            _fireButton = isLeftControl ? 1 : 0;
        }

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(_jumpButton))
                Jumped?.Invoke();

            if (UnityEngine.Input.GetMouseButtonDown(_fireButton))
                Fired?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                Jumped?.Invoke();
        }
    }
}