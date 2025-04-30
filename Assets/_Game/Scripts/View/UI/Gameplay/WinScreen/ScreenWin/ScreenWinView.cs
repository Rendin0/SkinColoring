using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWinView : WindowView<ScreenWinViewModel>
{

    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonSkip;

    private void OnEnable()
    {
        _buttonContinue.onClick.AddListener(OnContinueButtonClicked);
        _buttonSkip.onClick.AddListener(OnSkipButtonClicked);
    }

    private void OnDisable()
    {
        _buttonContinue.onClick.RemoveAllListeners();
        _buttonSkip.onClick.RemoveAllListeners();
    }

    private void OnContinueButtonClicked()
    {
        ViewModel.ContinueLevel();
    }

    private void OnSkipButtonClicked()
    {
        ViewModel.ContinueLevel();
    }
}