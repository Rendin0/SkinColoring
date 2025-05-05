

using System;
using YG;

public class PopupSettingsViewModel : WindowViewModel
{
    public override string Id => "PopupSettings";

    public void SwitchLanguage(string language)
    {
        YandexGame.SwitchLanguage(language);
    }
}
