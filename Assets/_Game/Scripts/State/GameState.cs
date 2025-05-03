using R3;
using YG;

public class GameState
{
    public ReactiveProperty<int> LevelId = new(0);
    public ReactiveProperty<int> Score = new(0);
    public ReactiveProperty<int> Coins = new(0);


    public static explicit operator GameState(SavesYG savesYG)
    {
        GameState state = new()
        {
            LevelId = new(savesYG.LevelId),
            Score = new(savesYG.Score),
            Coins = new(savesYG.Coins),
        };

        return state;
    }
}