using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGameplayView : WindowView<ScreenGameplayViewModel>
{

    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _customSkinButton;
    [SerializeField] private Button _skipLevelButton;

    [SerializeField] private ColoringView _coloringView;

    private readonly string _completePercentString = "Соответствие: ";
    [SerializeField] private TMP_Text _completePercentText;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private ScrollRect _scrollBar;

    private readonly CompositeDisposable _subs = new();

    private void UpdatePercents(EditableModel model1, EditableModel model2)
    {
        var model1Pixels = model1.GetAllPixels();
        var model2Pixels = model2.GetAllPixels();

        int matchCount = 0;
        for (int i = 0; i < model1Pixels.Count; i++)
        {
            if (model1Pixels[i] == model2Pixels[i])
                matchCount++;
        }

        ViewModel.CompletePercent.OnNext((float)matchCount / model1Pixels.Count);
    }

    #region Callbacks
    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _customSkinButton.onClick.AddListener(OnCustomSkinButtonClicked);
        _skipLevelButton.onClick.AddListener(OnSkipLevelButtonClicked);
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _customSkinButton.onClick.RemoveAllListeners();
        _skipLevelButton.onClick.RemoveAllListeners();
    }

    protected override void OnBind(ScreenGameplayViewModel viewModel)
    {
        ViewModel.CompletePercent.Subscribe(p => _completePercentText.text = $"{_completePercentString}{p * 100:00.0}%");
        ViewModel.Coins.Subscribe(c => _coinsText.text = $"{c}").AddTo(_subs);
        ViewModel.Score.Subscribe(s => _scoreText.text = $"Очки: {s}").AddTo(_subs);

        _coloringView.Bind(viewModel, viewModel.Colors, UpdatePercents);

        _scrollBar.horizontalNormalizedPosition = 0f;
    }

    private void OnDestroy()
    {
        _subs.Dispose();
    }

    private void OnSettingsButtonClicked()
    {
        ViewModel.OpenSettings();
    }

    private void OnCustomSkinButtonClicked()
    {
        ViewModel.OpenCustomSkin();
    }

    private void OnSkipLevelButtonClicked()
    {
        ViewModel.SkipLevel();
    }
    #endregion
}