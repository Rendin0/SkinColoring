using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;
using UnityEditor.Rendering;

public class ScreenGameplayView : WindowView<ScreenGameplayViewModel>
{
    [SerializeField] private RawImage _coloringArea;

    [SerializeField] private Button _settingsButton;

    [SerializeField] private Button _toolRotateButton;
    [SerializeField] private Button _toolPencilButton;
    [SerializeField] private Button _toolEraserButton;
    [SerializeField] private Button _toolClearButton;
    private Button _selectedTool;

    [SerializeField] private List<Button> _colorButtons;
    [SerializeField] private Image _selectedColorImage;

    private Camera _camera;
    private bool _isHolding = false;

    private void Update()
    {
        HandleColoring();
    }
    private void HandleColoring()
    {
        if (!_isHolding)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _coloringArea.rectTransform,
            Input.mousePosition,
            null,
            out var rectRelativeMousePosition
        );

        // ScreenPointToLocalPointInRectangle возвращает точку относительно центра пр€моугольника
        // »з-за этого необходима трансл€ци€ на половину размера
        rectRelativeMousePosition += _coloringArea.rectTransform.rect.size / 2;

        var ray = _camera.ScreenPointToRay(rectRelativeMousePosition);

        if (Physics.Raycast(ray, out var hit)
            && hit.collider.TryGetComponent<EditableTexture>(out var editableTexture))
        {
            editableTexture.ChangePixelColor(hit);
        }
    }

    #region Callbacks

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
        ViewModel.IsHolding.Subscribe(b => _isHolding = b);
        _camera = ViewModel.SkinCamera;
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
    #endregion
}