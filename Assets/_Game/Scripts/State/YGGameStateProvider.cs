
using YG;

public class YGGameStateProvider : IGameStateProvider
{
    public GameState Load()
    {
        return (GameState)YandexGame.savesData;
    }

    public void Save(GameState gameState)
    {
        YandexGame.savesData.SetData(gameState);
        YandexGame.SaveProgress();
    }
}