using R3;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : InputActions.IGameplayActions
{
    public Subject<InputAction.CallbackContext> MouseRequest { get; } = new();

    public void OnMouse(InputAction.CallbackContext context)
    {
        MouseRequest.OnNext(context);
    }
}
