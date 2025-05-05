using UnityEngine;
using UnityEngine.UI;
using YG;

public class PopupSettingsView : PopupView<PopupSettingsViewModel>
{
    [SerializeField] private Button _ruLanguageButton;
    [SerializeField] private Button _enLanguageButton;


    private void OnEnable()
    {
        _ruLanguageButton.onClick.AddListener(() => OnLanguageButtonClicked("ru"));
        _enLanguageButton.onClick.AddListener(() => OnLanguageButtonClicked("en"));
    }

    private void OnDisable()
    {
        _ruLanguageButton.onClick.RemoveAllListeners();
        _enLanguageButton.onClick.RemoveAllListeners();
    }

    private void OnLanguageButtonClicked(string language)
    {
        ViewModel.SwitchLanguage(language);
    }
}