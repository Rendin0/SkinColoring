using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class ScreenGameplayViewModel : WindowViewModel, IColoringViewModel
{
    public override string Id => "ScreenGameplay";

    private readonly GameplayUIManager _uiManager;

    public Subject<bool> IsHolding { get; } = new();
    public Subject<Vector2> RotateAxis { get; } = new();
    public Subject<bool> RMB { get; } = new();
    public Observable<int> Coins;
    public Observable<int> Score;
    public List<Color> Colors;

    public Camera SkinCamera { get; }

    private readonly CompositeDisposable _subs = new();


    public ReactiveProperty<float> CompletePercent { get; } = new();

    public ScreenGameplayViewModel(GameplayUIManager uiManager, InputHandler inputHandler, Camera skinCamera, GameState gameState, List<Color> colors)
    {
        _uiManager = uiManager;

        inputHandler.MouseRequest.Subscribe(c => IsHolding.OnNext(c.performed)).AddTo(_subs);
        inputHandler.AxisRequest.Subscribe(c => RotateAxis.OnNext(c.ReadValue<Vector2>())).AddTo(_subs);
        inputHandler.RMBRequest.Subscribe(c =>
        {
            if (!c.started)
                RMB.OnNext(c.performed);
        }).AddTo(_subs);

        CompletePercent.Subscribe(p =>
        {
            if (p >= 1f)
                ContinueLevel();
        });

        Score = gameState.Score;
        Coins = gameState.Coins;

        Colors = colors;
        SkinCamera = skinCamera;
    }

    public void OpenSettings()
    {
        _uiManager.OpenPopupSettings();
    }

    public void OpenCustomSkin()
    {
        _uiManager.OpenScreenCustomSkin();
    }

    public override void Dispose()
    {
        base.Dispose();

        _subs.Dispose();
    }

    public void SkipLevel()
    {
        _uiManager.SkipLevel();
    }

    private void ContinueLevel()
    {
        _uiManager.ContinueLevel();
    }
}