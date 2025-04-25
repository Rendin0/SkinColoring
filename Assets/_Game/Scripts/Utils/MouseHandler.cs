using UnityEngine;
using R3;
using UnityEngine.InputSystem;
using System;

public class MouseHandler : MonoBehaviour
{
    private InputActions _inputActions;
    private InputHandler _inputHandler;

    private Camera _camera;

    private bool isHolding = false;

    private void Awake()
    {
        _camera = Camera.main;

        _inputActions = new InputActions();
        _inputActions.Gameplay.Enable();

        _inputHandler = new InputHandler();
        _inputHandler.MouseRequest.Subscribe(c => OnMouseAction(c));

        _inputActions.Gameplay.SetCallbacks(_inputHandler);
    }

    private void OnMouseAction(InputAction.CallbackContext context)
    {
        isHolding = context.performed;
    }

    private void Update()
    {
        HandleColoring();
    }

    private void HandleColoring()
    {
        if (!isHolding)
            return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit)
            && hit.collider.TryGetComponent<EditableTexture>(out var editableTexture))
        {
            editableTexture.ChangePixelColor(hit);
        }
    }
}
