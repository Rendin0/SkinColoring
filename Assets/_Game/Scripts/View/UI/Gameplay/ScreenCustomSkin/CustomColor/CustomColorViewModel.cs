
using R3;
using UnityEngine;

public class CustomColorViewModel
{
    public Color Color { get; }
    public ReactiveProperty<bool> IsObtained { get; }

    public Subject<int> BuyColorRequest { get; } = new();

    public CustomColorViewModel(Color color, bool isObtained)
    { 
        Color = color;
        IsObtained = new(isObtained);
    }

    public void BuyColor(int colorPrice)
    {
        BuyColorRequest.OnNext(colorPrice);
    }
}