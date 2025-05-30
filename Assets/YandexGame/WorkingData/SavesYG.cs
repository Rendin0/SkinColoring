﻿
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public int LevelId = 0;
        public int Score = 0;
        public int Coins = 0;
        public SerializableDictionary<Color, bool> UnlockedColors = new();
        public bool HasSeenGeneralTip = false;
        public bool HasSeenCustomSkinTip = false;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG(/*CustomSkinColors colors*/)
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            

            openLevels[1] = true;
        }

        public void SetData(GameState gameState)
        {
            LevelId = gameState.LevelId.Value;
            Score = gameState.Score.Value;
            Coins = gameState.Coins.Value;
            HasSeenGeneralTip = gameState.HasSeenGeneralTip.Value;
            HasSeenCustomSkinTip = gameState.HasSeenCustomSkinTip.Value;

            UnlockedColors = new();
            foreach (var color in gameState.UnlockedColors)
            {
                UnlockedColors[color.Key.Color] = color.Value;
            }
        }
    }
}
