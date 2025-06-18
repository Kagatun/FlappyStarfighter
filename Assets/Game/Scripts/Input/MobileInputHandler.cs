using System;
using UnityEngine;

namespace Scripts.Input
{
    public class MobileInputHandler : IInputHandler
    {
        private readonly bool _leftSideIsJump;
        
        public event Action Jumped;
        public event Action Fired;
        
        public MobileInputHandler(bool isLeftControl)
        {
            _leftSideIsJump = isLeftControl;
        }

        public void Update()
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                if (touch.phase != TouchPhase.Began)
                    continue;
            
                bool isLeftSide = touch.position.x < Screen.width / 2f;
            
                if (isLeftSide == _leftSideIsJump)
                    Jumped?.Invoke();
                else
                    Fired?.Invoke();
            }
        }
    }
}