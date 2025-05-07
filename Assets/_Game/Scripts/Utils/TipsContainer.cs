using R3;
using System.Collections.Generic;
using UnityEngine;

public class TipsContainer : MonoBehaviour
{
    [SerializeField] private List<Tip> _tips;
    private int _tipIndex = 0;
    private readonly CompositeDisposable _subs = new();

    private readonly Subject<Unit> _tipsEnded = new();

    public void Init()
    {
        var rectTransform = GetComponent<RectTransform>();

        foreach (var tip in _tips)
        {
            tip.gameObject.SetActive(false);
            tip.transform.SetParent(rectTransform, true);
        }
    }
    public Subject<Unit> StartTips()
    {
        NextTip();
        return _tipsEnded;
    }

    private void NextTip()
    {
        if (_tipIndex >= _tips.Count)
        {
            _tipIndex = 0;
            _subs.Dispose();
            _tipsEnded.OnNext(Unit.Default);
            return;
        }

        var currentTip = _tips[_tipIndex];
        currentTip.gameObject.SetActive(true);
        currentTip.NextTipRequest.Subscribe(_ => TipSub(currentTip)).AddTo(_subs);
    }

    private void TipSub(Tip tip)
    {
        _tipIndex++;
        tip.gameObject.SetActive(false);
        NextTip();
    }
}