using UnityEngine;
using UnityEngine.UI;
using R3;


public class CustomColorView : MonoBehaviour
{
    [SerializeField] private Button _buyColorButton;
    [SerializeField] private int _buyPrice;
    private CustomColorViewModel _viewModel;
    public Button Button { get; private set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    protected void OnEnable()
    {
        _buyColorButton.onClick.AddListener(OnBuyColorButtonClicked);
    }

    protected void OnDisable()
    {
        _buyColorButton.onClick.RemoveAllListeners();
    }

    public void Bind(CustomColorViewModel viewModel)
    {
        _viewModel = viewModel;
        Button.targetGraphic.color = new Color(viewModel.Color.r, viewModel.Color.g, viewModel.Color.b, 1f);

        _viewModel.IsObtained.Subscribe(b => _buyColorButton.gameObject.SetActive(!b));
    }

    private void OnBuyColorButtonClicked()
    {
        _viewModel.BuyColor(_buyPrice);
    }
}