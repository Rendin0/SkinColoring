
using R3;
using System;
using UnityEngine;

public class ScreenCustomSkinViewModel : WindowViewModel, IColoringViewModel
{
    public override string Id => "ScreenCustomSkin";

    public Subject<bool> IsHolding { get; } = new();
    public Subject<Vector2> RotateAxis { get; } = new();
    public Subject<bool> RMB { get; } = new();
    public Camera SkinCamera { get; }
    public GameState GameState { get; }
    public Observable<int> Coins { get; }
    public Observable<int> Score { get; }
    public ReactiveProperty<bool> HasSeenCustomSkinTips { get; }

    private readonly GameplayUIManager _uiManager;
    private readonly CompositeDisposable _subs = new();

    public ScreenCustomSkinViewModel(GameplayUIManager uiManager, InputHandler inputHandler, Camera skinCamera, GameState gameState)
    {
        _uiManager = uiManager;

        inputHandler.MouseRequest.Subscribe(c => IsHolding.OnNext(c.performed)).AddTo(_subs);
        inputHandler.AxisRequest.Subscribe(c => RotateAxis.OnNext(c.ReadValue<Vector2>())).AddTo(_subs);
        inputHandler.RMBRequest.Subscribe(c =>
        {
            if (!c.started)
                RMB.OnNext(c.performed);
        }).AddTo(_subs);

        SkinCamera = skinCamera;
        
        Score = gameState.Score;
        Coins = gameState.Coins;
        HasSeenCustomSkinTips = gameState.HasSeenCustomSkinTip;

        GameState = gameState;
    }

    public void OpenSettings()
    {
        _uiManager.OpenPopupSettings();
    }

    public void OpenLevels()
    {
        _uiManager.OpenScreenGameplay();
    }

    public override void Dispose()
    {
        base.Dispose();

        _subs.Dispose();
    }

    public void EndCustomSkinTips()
    {
        HasSeenCustomSkinTips.OnNext(true);
    }
}