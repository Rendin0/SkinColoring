using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Game.Scripts.Game.Root;
using ObservableCollections;
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
    public ReactiveProperty<bool> HasSeenGeneralTip;
    public ReactiveProperty<bool> HasSeenCustomSkinTip;
    public ObservableDictionary<CustomColorViewModel, bool> Colors;
    public int LevelId { get; }
    public string SkinNick { get; }

    public Camera SkinCamera { get; }

    private readonly CompositeDisposable _subs = new();


    public ReactiveProperty<float> CompletePercent { get; } = new();

    public ScreenGameplayViewModel(GameplayUIManager uiManager, InputHandler inputHandler, Camera skinCamera, GameState gameState, List<Color> colors, string skinNick)
    {
        _uiManager = uiManager;

        inputHandler.MouseRequest.Subscribe(c => IsHolding.OnNext(c.performed)).AddTo(_subs);
        inputHandler.AxisRequest.Subscribe(c => RotateAxis.OnNext(c.ReadValue<Vector2>())).AddTo(_subs);
        inputHandler.RMBRequest.Subscribe(c =>
        {
            if (!c.started)
                RMB.OnNext(c.performed);
        }).AddTo(_subs);

        

        Score = gameState.Score;
        Coins = gameState.Coins;
        LevelId = gameState.LevelId.Value;
        SkinNick = skinNick;
        HasSeenGeneralTip = gameState.HasSeenGeneralTip;
        HasSeenCustomSkinTip = gameState.HasSeenCustomSkinTip;


        Colors = new(colors.ToDictionary(item => new CustomColorViewModel(item, true), item => true));
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

    public void ContinueLevel()
    {
        _uiManager.ContinueLevel();
    }

    public void EndGeneralTips()
    {
        HasSeenGeneralTip.OnNext(true);
    }

    public void EndCustomSkinTips()
    {
        HasSeenCustomSkinTip.OnNext(true);
    }
}