
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Leg
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _bottom;
    [SerializeField] private List<Renderer> _sides;
}