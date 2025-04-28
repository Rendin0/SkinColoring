using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;
using UnityEditor.Rendering;
using System.Runtime.InteropServices.WindowsRuntime;
using System;
using TMPro.EditorUtilities;

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
    private Color _selectedColor;

    private Camera _camera;
    private bool _isHolding = false;

    private EditableModel[] _models;

    private void Update()
    {
        HandleTools();
    }

    private void HandleTools()
    {
        if (!_isHolding)
            return;

        if (_selectedTool == _toolPencilButton)
            ColorPixel();
        else if (_selectedTool == _toolRotateButton)
            RotateModel();
        else if (_selectedTool == _toolEraserButton)
            ErasePixel();
        else
            ClearTexutre();
    }

    private void RotateModel()
    {
        foreach (var editableModel in _models)
        {
            float xAxis = Input.GetAxis("Mouse X");
            float yAxis = Input.GetAxis("Mouse Y");

            editableModel.transform.Rotate(Vector3.down, xAxis);
            editableModel.transform.Rotate(Vector3.forward, yAxis);
        }
    }

    private void ClearTexutre()
    {
        if (TryHit<EditableTexture>(out var editableTexture, out _))
        {
            // Стандартный цвет текстуры
            editableTexture.ColorAllPixels(new Color32(205, 205, 205, 205));
        }
    }

    private void ErasePixel()
    {
        if (TryHit<EditableTexture>(out var editableTexture, out var hit))
        {
            // Стандартный цвет текстуры
            editableTexture.ChangePixelColor(hit, new Color(0.804f, 0.804f, 0.804f, 0.804f));
        }
    }

    private void ColorPixel()
    {
        if(TryHit<EditableTexture>(out var editableTexture, out var hit))
        {
            editableTexture.ChangePixelColor(hit, _selectedColor);
        }
    }

    private bool TryHit<T>(out T hitObject, out RaycastHit hit) where T : MonoBehaviour
    {
        hitObject = null;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _coloringArea.rectTransform,
            Input.mousePosition,
            null,
            out var rectRelativeMousePosition
        );

        // ScreenPointToLocalPointInRectangle возвращает точку относительно центра прямоугольника
        // Из-за этого необходима трансляция на половину размера
        rectRelativeMousePosition += _coloringArea.rectTransform.rect.size / 2;

        var ray = _camera.ScreenPointToRay(rectRelativeMousePosition);

        if (Physics.Raycast(ray, out hit)
            && hit.collider.TryGetComponent(out hitObject))
        {
            return true;
        }
        return false;
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

        _selectedColor = _colorButtons[0].targetGraphic.color;

        ViewModel.IsHolding.Subscribe(b => _isHolding = b);
        _camera = ViewModel.SkinCamera;

        _models = FindObjectsByType<EditableModel>(FindObjectsSortMode.InstanceID);
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
        _selectedColor = color.targetGraphic.color;
        SetSelectedTool(_toolPencilButton);
    }
    #endregion
}