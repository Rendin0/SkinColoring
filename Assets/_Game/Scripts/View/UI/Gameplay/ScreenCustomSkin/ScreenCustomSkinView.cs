
using UnityEngine;
using UnityEngine.UI;

public class ScreenCustomSkinView : WindowView<ScreenCustomSkinViewModel>
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _levelsButton;
    [SerializeField] private ColoringView _coloringView;


    #region Callbacks
    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _levelsButton.onClick.AddListener(OnLevelsButtonClicked);
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _levelsButton.onClick.RemoveAllListeners();
    }

    protected override void OnBind(ScreenCustomSkinViewModel viewModel)
    {
        _coloringView.Bind(viewModel);
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