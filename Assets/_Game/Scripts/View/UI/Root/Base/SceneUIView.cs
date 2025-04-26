
using ObservableCollections;
using R3;
using UnityEngine;

public class SceneUIView : MonoBehaviour
{
    [SerializeField] private WindowsContainer _windowsContainer;

    private readonly CompositeDisposable _subscriptions = new();

    public void Bind(SceneUIViewModel viewModel)
    {
        _subscriptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
        {
            _windowsContainer.OpenScreen(newScreenViewModel);
        }));

        foreach (var openedPopup in viewModel.OpenedPopups)
        {
            _windowsContainer.OpenPopup(openedPopup);
        }


        _subscriptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(e =>
        {
            _windowsContainer.OpenPopup(e.Value);
        }));

        _subscriptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(e =>
        {
            _windowsContainer.ClosePopup(e.Value);
        }));

        OnBind(viewModel);
    }

    protected virtual void OnBind(SceneUIViewModel rootViewModel) { }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }

}