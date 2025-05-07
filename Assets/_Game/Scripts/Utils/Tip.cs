using R3;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    [SerializeField] private Button _tipButton;
    [SerializeField] private bool _disableButtonOnClick = false;

    public Subject<Unit> NextTipRequest = new();

    private void OnEnable()
    {
        _tipButton.onClick.AddListener(OnTipButtonClicked);
    }

    private void OnDisable()
    {
        _tipButton.onClick.RemoveAllListeners();
    }

    private void OnTipButtonClicked()
    {
        if (_disableButtonOnClick)
            _tipButton.gameObject.SetActive(false);

        NextTipRequest.OnNext(Unit.Default);
    }
}