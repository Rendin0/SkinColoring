
using System;
using UnityEngine;

[Serializable]
public class Chest
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _bottom;
    [SerializeField] private Renderer _back;
    [SerializeField] private Renderer _front;
    [SerializeField] private Renderer _left;
    [SerializeField] private Renderer _right;
}