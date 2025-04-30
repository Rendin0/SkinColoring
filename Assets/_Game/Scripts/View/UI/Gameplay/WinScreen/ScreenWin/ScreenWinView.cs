using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWinView : WindowView<ScreenWinViewModel>
{

    [SerializeField] private Button _buttonContinue;

    private void OnEnable()
    {
        _buttonContinue.onClick.AddListener(OnContinueButtonClicked);

    }

    private void OnContinueButtonClicked()
    {
        ViewModel.ContinueLevel();
    }
}