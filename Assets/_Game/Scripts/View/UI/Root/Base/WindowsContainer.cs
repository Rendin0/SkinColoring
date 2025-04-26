using System.Collections.Generic;
using UnityEngine;

public class WindowsContainer : MonoBehaviour
{
    [SerializeField] private Transform _screensContainer;
    [SerializeField] private Transform _popupsContainer;

    private readonly Dictionary<WindowViewModel, IWindowView> _openedPopupBinders = new();
    private IWindowView _openedScreenBinder;

    public void OpenPopup(WindowViewModel viewModel)
    {
        var prefabPath = GetPrefabPath(viewModel);
        var prefab = Resources.Load<GameObject>(prefabPath);
        var createdPopup = Instantiate(prefab, _popupsContainer);
        var binder = createdPopup.GetComponent<IWindowView>();

        binder.Bind(viewModel);
        _openedPopupBinders.Add(viewModel, binder);
    }

    public void ClosePopup(WindowViewModel popupViewModel)
    {
        var binder = _openedPopupBinders[popupViewModel];

        binder?.Close();
        _openedPopupBinders.Remove(popupViewModel);
    }

    public void OpenScreen(WindowViewModel viewModel)
    {
        if (viewModel == null)
        {
            return;
        }

        _openedScreenBinder?.Close();

        var prefabPath = GetPrefabPath(viewModel);
        var prefab = Resources.Load<GameObject>(prefabPath);
        var createdScreen = Instantiate(prefab, _screensContainer);
        var binder = createdScreen.GetComponent<IWindowView>();

        binder.Bind(viewModel);
        _openedScreenBinder = binder;
    }

    private static string GetPrefabPath(WindowViewModel viewModel)
    {
        return $"UI/{viewModel.Id}";
    }
}