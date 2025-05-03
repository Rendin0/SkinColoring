
using System;
using System.Collections.Generic;
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

    public List<Texture> SetTexture(Texture2D texture)
    {
        _top.material.mainTexture = texture.CutTexture(20, 44, 4, 8);
        _bottom.material.mainTexture = texture.CutTexture(28, 44, 4, 8);
        _back.material.mainTexture = texture.CutTexture(32, 32, 8, 12);
        _front.material.mainTexture = texture.CutTexture(20, 32, 8, 12);
        _left.material.mainTexture = texture.CutTexture(16, 32, 4, 12);
        _right.material.mainTexture = texture.CutTexture(28, 32, 4, 12);

        List<Texture> result = new()
        {
            //_top.material.mainTexture,
            //_bottom.material.mainTexture,
            _back.material.mainTexture,
            _front.material.mainTexture,
            //_left.material.mainTexture,
            //_right.material.mainTexture,
        };

        return result;
    }
}