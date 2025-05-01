using System;
using UnityEditor.Overlays;
using YG;

[Serializable]
public class GameState
{
    public int LevelId = 0;

    public static explicit operator GameState(SavesYG savesYG)
    {
        GameState state = new()
        {
            LevelId = savesYG.LevelId,
        };

        return state;
    }
}