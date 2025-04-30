using R3;
using UnityEngine.InputSystem;

public class InputHandler : InputActions.IGameplayActions
{
    public Subject<InputAction.CallbackContext> MouseRequest { get; } = new();
    public Subject<InputAction.CallbackContext> AxisRequest { get; } = new();
    public Subject<InputAction.CallbackContext> RMBRequest { get; } = new();



    public void OnAxis(InputAction.CallbackContext context)
    {
        AxisRequest.OnNext(context);
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        MouseRequest.OnNext(context);
    }

    public void OnRMB(InputAction.CallbackContext context)
    {
        RMBRequest.OnNext(context);
    }
}
