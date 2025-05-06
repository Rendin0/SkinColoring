using R3;
using UnityEngine;

public class TipsContainer : MonoBehaviour
{
    [SerializeField] private List<Tip> _tips;
    private int _tipIndex = 0;
    private CompositeDisposable _subs = new();

    private void Awake()
    {
        _tips.ForEach(tip => tip.gameObject.SetActive(false));
    }


    public bool NextTip()
    {
        if (_tipIndex >= _tips.Length)
        {
            _subs.Dispose();
            return false;
        }

        var currentTip = _tips[_tipIndex];
        currentTip.gameObject.SetActive(true);
        currentTip.NextTipRequest.Subscribe(_ => TipSub(currentTip)).AddTo(_subs);

        return true;
    }

    private void TipSub(Tip tip)
    {
        _tipIndex++;
        tip.gameObject.SetActive(false);
        NextTip();
    }
}