using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Head
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _bottom;
    [SerializeField] private Renderer _back;
    [SerializeField] private Renderer _front;
    [SerializeField] private Renderer _left;
    [SerializeField] private Renderer _right;

    public List<Texture> SetTexture(Texture2D texture)
    {
        _top.material.mainTexture = texture.CutTexture(8, 56, 8, 8);
        _bottom.material.mainTexture = texture.CutTexture(16, 56, 8, 8);
        _left.material.mainTexture = texture.CutTexture(0, 48, 8, 8);
        _front.material.mainTexture = texture.CutTexture(8, 48, 8, 8);
        _right.material.mainTexture = texture.CutTexture(16, 48, 8, 8);
        _back.material.mainTexture = texture.CutTexture(24, 48, 8, 8);

        List<Texture> result = new()
        {
            _top.material.mainTexture,
            //_bottom.material.mainTexture,
            _back.material.mainTexture,
            _front.material.mainTexture,
            _left.material.mainTexture,
            _right.material.mainTexture,
        };

        return result;
    }
}