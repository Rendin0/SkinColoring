
public interface IGameStateProvider
{
    void Save(GameState gameState);
    GameState Load();
}
