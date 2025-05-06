using R3;

public class Tip : MonoBehaviour
{
    [SerializeField] private Button _tipButton;
    [SerializeField] private bool _disableButtonOnClick = false;

    public Subject<Unit> NextTipRequest = new();

    private void OnEnable()
    {
        _tipButton.onClick.AddListener(OnTipButtonClicked)
    }

    private void OnDisable()
    {
        _tipButton.onClick.RemoveAllListeners();
    }

    private void OnTipButtonClicked()
    {
        NextTipRequest.OnNext(Unit.Default);
    }
}