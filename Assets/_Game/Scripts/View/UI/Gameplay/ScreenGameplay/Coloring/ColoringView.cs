using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoringView : MonoBehaviour
{
    [SerializeField] private RawImage _coloringArea;


    [SerializeField] private Button _toolRotateButton;
    [SerializeField] private Button _toolPencilButton;
    [SerializeField] private Button _toolEraserButton;
    [SerializeField] private Button _toolClearButton;
    private Button _selectedTool;
    private Button _prevSelectedTool;

    [SerializeField] private List<Button> _colorButtons;
    [SerializeField] private Image _selectedColorImage;
    private Color _selectedColor;

    private Camera _camera;

    private bool _isHolding = false;
    private Vector2 _rotateAxis = Vector2.zero;

    private EditableModel[] _models;
    [SerializeField] private float _rotateSpeed;

    private Action<EditableModel, EditableModel> _drawCallback;

    private void Update()
    {
        HandleTools();
    }

    #region Tools
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
            editableModel.transform.Rotate(Vector3.down, _rotateAxis.x * _rotateSpeed, Space.World);
            editableModel.transform.Rotate(_camera.transform.right, _rotateAxis.y * _rotateSpeed, Space.World);
        }
    }
    private void ClearTexutre()
    {
        if (TryHit<EditableTexture>(out var editableTexture, out _))
        {
            // Стандартный цвет текстуры
            editableTexture.ColorAllPixels(new Color32(205, 205, 205, 205));
            _drawCallback?.Invoke(_models[0], _models[1]);
        }
    }
    private void ErasePixel()
    {
        if (TryHit<EditableTexture>(out var editableTexture, out var hit))
        {
            // Стандартный цвет текстуры
            editableTexture.ChangePixelColor(hit, new Color(0.804f, 0.804f, 0.804f, 0.804f));
            _drawCallback?.Invoke(_models[0], _models[1]);
        }
    }
    private void ColorPixel()
    {
        if (TryHit<EditableTexture>(out var editableTexture, out var hit))
        {
            editableTexture.ChangePixelColor(hit, _selectedColor);
            _drawCallback?.Invoke(_models[0], _models[1]);
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
    #endregion

    #region Bindings
    public void Bind(IColoringViewModel viewModel, Action<EditableModel, EditableModel> updatePercents = null)
    {
        _drawCallback = updatePercents;
        _camera = viewModel.SkinCamera;

        SetSelectedTool(_toolPencilButton);

        _selectedColor = _colorButtons[0].targetGraphic.color;

        viewModel.IsHolding.Subscribe(b => _isHolding = b);
        viewModel.RotateAxis.Subscribe(a => _rotateAxis = a);
        viewModel.RMB.Subscribe(b => OnRMB(b));

        _models = FindObjectsByType<EditableModel>(FindObjectsSortMode.InstanceID);

        _drawCallback?.Invoke(_models[0], _models[1]);
    }

    private void OnEnable()
    {
        _toolRotateButton.onClick.AddListener(() => OnToolButtonClicked(_toolRotateButton));
        _toolPencilButton.onClick.AddListener(() => OnToolButtonClicked(_toolPencilButton));
        _toolEraserButton.onClick.AddListener(() => OnToolButtonClicked(_toolEraserButton));
        _toolClearButton.onClick.AddListener(() => OnToolButtonClicked(_toolClearButton));

        _colorButtons.ForEach(color => color.onClick.AddListener(() => SetSelectedColor(color)));
    }

    private void OnDisable()
    {
        _toolRotateButton.onClick.RemoveAllListeners();
        _toolPencilButton.onClick.RemoveAllListeners();
        _toolEraserButton.onClick.RemoveAllListeners();
        _toolClearButton.onClick.RemoveAllListeners();

        _colorButtons.ForEach(color => color.onClick.RemoveAllListeners());
    }

    private void OnToolButtonClicked(Button tool)
    {
        SetSelectedTool(tool);
    }

    private void OnRMB(bool isHolding)
    {
        _isHolding = isHolding;
        if (_isHolding)
        {
            _prevSelectedTool = _selectedTool;
            SetSelectedTool(_toolRotateButton);
        }
        else
            SetSelectedTool(_prevSelectedTool);
    }

    private void SetSelectedTool(Button tool)
    {
        if (tool == null)
            return;

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