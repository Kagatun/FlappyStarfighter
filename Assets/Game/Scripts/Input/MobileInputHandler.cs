using System;
using UnityEngine;

namespace Scripts.Input
{
    public class MobileInputHandler : IInputHandler
    {
        public event Action Jumped;
        public event Action Fired;

        public void Update()
        {
            if (UnityEngine.Input.touchCount <= 0) 
                return;
            
            Touch touch = UnityEngine.Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began)
                return;
                
            if (touch.position.x < Screen.width / 2f)
                Jumped?.Invoke();
            else
                Fired?.Invoke();
        }
    }
}