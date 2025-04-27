using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGameplayView : WindowView<ScreenGameplayViewModel>
{
    [SerializeField] private Button _settingsButton;

    [SerializeField] private Button _toolRotateButton;
    [SerializeField] private Button _toolPencilButton;
    [SerializeField] private Button _toolEraserButton;
    [SerializeField] private Button _toolClearButton;
    private Button _selectedTool;

    [SerializeField] private List<Button> _colorButtons;
    [SerializeField] private Image _selectedColorImage;


    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);

        _toolRotateButton.onClick.AddListener(() => OnToolButtonClicked(_toolRotateButton));
        _toolPencilButton.onClick.AddListener(() => OnToolButtonClicked(_toolPencilButton));
        _toolEraserButton.onClick.AddListener(() => OnToolButtonClicked(_toolEraserButton));
        _toolClearButton.onClick.AddListener(() => OnToolButtonClicked(_toolClearButton));

        _colorButtons.ForEach(color => color.onClick.AddListener(() => SetSelectedColor(color)));
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveAllListeners();

        _toolRotateButton.onClick.RemoveAllListeners();
        _toolPencilButton.onClick.RemoveAllListeners();
        _toolEraserButton.onClick.RemoveAllListeners();
        _toolClearButton.onClick.RemoveAllListeners();

        _colorButtons.ForEach(color => color.onClick.RemoveAllListeners());
    }

    protected override void OnBind(ScreenGameplayViewModel viewModel)
    {
        SetSelectedTool(_toolPencilButton);
        ViewModel.SetSelectedColor(_colorButtons[0].targetGraphic.color);
    }

    private void OnSettingsButtonClicked()
    {
        ViewModel.OpenSettings();
    }

    private void OnToolButtonClicked(Button tool)
    {
        SetSelectedTool(tool);
    }

    private void SetSelectedTool(Button tool)
    {
        if (_selectedTool != null)
            _selectedTool.targetGraphic.color = Color.clear;

        tool.targetGraphic.color = Color.white;
        _selectedTool = tool;
    }

    private void SetSelectedColor(Button color)
    {
        _selectedColorImage.rectTransform.position = color.transform.position;
        ViewModel.SetSelectedColor(color.targetGraphic.color);
    }
}