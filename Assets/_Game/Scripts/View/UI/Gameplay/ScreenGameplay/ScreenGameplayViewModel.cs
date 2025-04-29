using R3;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenGameplayViewModel : WindowViewModel
{
    public override string Id => "ScreenGameplay";

    private readonly GameplayUIManager _uiManager;

    public Subject<bool> IsHolding { get; } = new();
    public Subject<Vector2> RotateAxis { get; } = new();

    private readonly CompositeDisposable _subs = new();

    public Camera SkinCamera { get; }

    public ReactiveProperty<float> CompletePercent { get; } = new();

    public ScreenGameplayViewModel(GameplayUIManager uiManager, InputHandler inputHandler, Camera skinCamera)
    {
        _uiManager = uiManager;

        inputHandler.MouseRequest.Subscribe(c => IsHolding.OnNext(c.performed)).AddTo(_subs);
        inputHandler.AxisRequest.Subscribe(c => RotateAxis.OnNext(c.ReadValue<Vector2>())).AddTo(_subs);

        SkinCamera = skinCamera;
    }

    public void OpenSettings()
    {
        _uiManager.OpenPopupSettings();
    }

    public override void Dispose()
    {
        base.Dispose();

        _subs.Dispose();
    }
}