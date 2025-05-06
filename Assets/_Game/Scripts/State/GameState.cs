using ObservableCollections;
using R3;
using System.Linq;
using UnityEngine;
using YG;

public class GameState
{
    public ReactiveProperty<int> LevelId = new(0);
    public ReactiveProperty<int> Score = new(0);
    public ReactiveProperty<int> Coins = new(0);
    public ObservableDictionary<CustomColorViewModel, bool> UnlockedColors = new();
    public ReactiveProperty<bool> HasSeenGeneralTip = new(false);
    public ReactiveProperty<bool> HasSeenCustomSkinTip = new(false);


    public static explicit operator GameState(SavesYG savesYG)
    {
        GameState state = new()
        {
            LevelId = new(savesYG.LevelId),
            Score = new(savesYG.Score),
            Coins = new(savesYG.Coins),
            HasSeenGeneralTip = new(savesYG.HasSeenGeneralTip),
            HasSeenCustomSkinTip = new(savesYG.HasSeenCustomSkinTip),
            UnlockedColors = new(savesYG.UnlockedColors.ToDictionary(pair => new CustomColorViewModel(pair.Key, pair.Value), pair => pair.Value)),
        };

        return state;
    }
}