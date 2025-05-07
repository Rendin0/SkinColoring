using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGameplayView : WindowView<ScreenGameplayViewModel>
{

    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _customSkinButton;
    [SerializeField] private Button _skipLevelButton;

    [SerializeField] private ColoringView _coloringView;

    [SerializeField] private TMP_Text _completePercentText;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _skinNickText;

    [SerializeField] private ScrollRect _scrollBar;

    [SerializeField] private TipsContainer _generalTips;
    [SerializeField] private TipsContainer _customSkinTips;

    [SerializeField] private Image _confettiGif;

    private readonly CompositeDisposable _subs = new();

    private void UpdatePercents(EditableModel model1, EditableModel model2)
    {
        var model1Pixels = model1.GetAllPixels();
        var model2Pixels = model2.GetAllPixels();

        int matchCount = 0;
        for (int i = 0; i < model1Pixels.Count; i++)
        {
            if (model1Pixels[i] == model2Pixels[i])
                matchCount++;
        }

        ViewModel.CompletePercent.OnNext((float)matchCount / model1Pixels.Count);
    }

    #region Callbacks
    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _customSkinButton.onClick.AddListener(OnCustomSkinButtonClicked);
        _skipLevelButton.onClick.AddListener(OnSkipLevelButtonClicked);

        _generalTips.Init();
        _customSkinTips.Init();
        _generalTips.gameObject.SetActive(false);
        _customSkinTips.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _customSkinButton.onClick.RemoveAllListeners();
        _skipLevelButton.onClick.RemoveAllListeners();
    }

    protected override void OnBind(ScreenGameplayViewModel viewModel)
    {
        ViewModel.CompletePercent.Subscribe(p => _completePercentText.text = $"{p * 100:00.0}%");
        ViewModel.Coins.Subscribe(c => _coinsText.text = $"{c}").AddTo(_subs);
        ViewModel.Score.Subscribe(s => _scoreText.text = $"{s}").AddTo(_subs);
        _skinNickText.text = ViewModel.SkinNick;

        ViewModel.CompletePercent.Skip(5).Subscribe(p =>
        {
            if (p >= 1f)
                StartCoroutine(EndLevel());
        });

        _coloringView.Bind(viewModel, viewModel.Colors, UpdatePercents);

        _scrollBar.horizontalNormalizedPosition = 0f;

        if (!ViewModel.HasSeenGeneralTip.CurrentValue)
        {
            _generalTips.gameObject.SetActive(true);
            _generalTips.StartTips().Subscribe(_ => OnGeneralTipsEnded());
        }

        if (ViewModel.LevelId >= 2 && !ViewModel.HasSeenCustomSkinTip.CurrentValue)
        {
            _customSkinTips.gameObject.SetActive(true);
            _customSkinTips.StartTips();
        }
    }

    private void OnGeneralTipsEnded()
    {
        _generalTips.gameObject.SetActive(false);
        ViewModel.EndGeneralTips();
    }
    private void OnDestroy()
    {
        _subs.Dispose();
    }

    private void OnSettingsButtonClicked()
    {
        ViewModel.OpenSettings();
    }

    private void OnCustomSkinButtonClicked()
    {
        ViewModel.OpenCustomSkin();
    }

    private void OnSkipLevelButtonClicked()
    {
        ViewModel.SkipLevel();
    }

    private IEnumerator EndLevel()
    {
        var models = _coloringView.EndLevel();
        var startRotation = models[0].transform.rotation;
        var endRotation = Quaternion.Euler(new Vector3(0, 270, 0));

        _confettiGif.gameObject.SetActive(true);

        float time = 2.5f;
        float waitTimer = 0f;

        while (waitTimer <= time)
        {
            models[0].transform.rotation = Quaternion.Lerp(startRotation, endRotation, waitTimer);
            models[1].transform.rotation = Quaternion.Lerp(startRotation, endRotation, waitTimer);

            yield return new WaitForSeconds(Time.deltaTime);
            waitTimer += Time.deltaTime;
        }

        _confettiGif.gameObject.SetActive(false);

        ViewModel.ContinueLevel();
    }
    #endregion
}