using NUnit.Framework;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCustomSkinView : WindowView<ScreenCustomSkinViewModel>
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _levelsButton;
    [SerializeField] private Button _getCoinsButton;
    [SerializeField] private ColoringView _coloringView;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private CustomSkinColors _colors;

    private readonly CompositeDisposable _subs = new();


    #region Callbacks
    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _levelsButton.onClick.AddListener(OnLevelsButtonClicked);
        _getCoinsButton.onClick.AddListener(OnGetCoinsButtonClicked);
    }

    private void OnGetCoinsButtonClicked()
    {
        YGUtils.ShowRewarded(Rewards.Coins50);
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _levelsButton.onClick.RemoveAllListeners();
        _getCoinsButton.onClick.RemoveAllListeners();
    }

    protected override void OnBind(ScreenCustomSkinViewModel viewModel)
    {
        _coloringView.Bind(viewModel, _colors.Colors);
        viewModel.Coins.Subscribe(c => _coinsText.text = $"{c}").AddTo(_subs);
        viewModel.Score.Subscribe(s => _scoreText.text = $"Очки: {s}").AddTo(_subs);
    }
    private void OnDestroy()
    {
        _subs.Dispose();
    }

    private void OnSettingsButtonClicked()
    {
        ViewModel.OpenSettings();
    }

    private void OnLevelsButtonClicked()
    {
        ViewModel.OpenLevels();
    }
    #endregion

}