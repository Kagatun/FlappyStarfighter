using System;

namespace Scripts.Input
{
    public interface IInputHandler
    {
        event Action Jumped;
        event Action Fired;

        void Update();
    }
}
