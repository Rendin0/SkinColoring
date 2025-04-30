
using R3;
using UnityEngine;

public interface IColoringViewModel
{
    Camera SkinCamera { get; }
    Subject<bool> IsHolding { get; }
    Subject<Vector2> RotateAxis { get; }
}