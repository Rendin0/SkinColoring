
using R3;
using System;
using UnityEngine;
using YG;

public static class YGUtils
{
    public static Subject<Rewards> Rewarded = new();

    public static void ShowRewarded(Rewards reward)
    {
        YandexGame.RewVideoShow((int)reward);
    }

    public static void Rew(int id)
    {
        Rewarded.OnNext((Rewards)id); 
    }
}
