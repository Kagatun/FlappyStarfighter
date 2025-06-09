using System;

public interface IInputHandler
{
    event Action Jumped;
    event Action Fired;
    
    void Update();
}
