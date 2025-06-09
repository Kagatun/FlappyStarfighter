using System;

namespace Scripts.Input
{
    public class DesktopInputHandler : IInputHandler
    {
        private readonly int _leftButton;
        private readonly int _rightButton = 1;
        
        public event Action Jumped;
        public event Action Fired;

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(_leftButton))
                Jumped?.Invoke();

            if (UnityEngine.Input.GetMouseButtonDown(_rightButton))
                Fired?.Invoke();
        }
    }
}