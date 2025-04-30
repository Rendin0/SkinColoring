
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Arm
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _bottom;
    [SerializeField] private List<Renderer> _sides;
}
