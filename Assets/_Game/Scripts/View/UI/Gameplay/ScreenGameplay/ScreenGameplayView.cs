using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGameplayView : WindowView<ScreenGameplayViewModel>
{

    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _customSkinButton;

    [SerializeField] private ColoringView _coloringView;

    private readonly string _completePercentString = "Соответствие: ";
    [SerializeField] private TMP_Text _completePercentText;



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
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _customSkinButton.onClick.RemoveAllListeners();
    }

    protected override void OnBind(ScreenGameplayViewModel viewModel)
    {
        ViewModel.CompletePercent.Subscribe(p => _completePercentText.text = $"{_completePercentString}{p * 100:00}%");

        _coloringView.Bind(viewModel, UpdatePercents);
    }
    private void OnSettingsButtonClicked()
    {
        ViewModel.OpenSettings();
    }

    private void OnCustomSkinButtonClicked()
    {
        ViewModel.OpenCustomSkin();
    }
    #endregion
}