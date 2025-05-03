
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Leg
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _bottom;
    [SerializeField] private List<Renderer> _sides;

    public List<Texture> SetTexture(Texture2D texture, int xOffset = 0, int yOffset = 0)
    {
        _top.material.mainTexture = texture.CutTexture(4 + xOffset, 44 + yOffset, 4, 4);
        _bottom.material.mainTexture = texture.CutTexture(8 + xOffset, 44 + yOffset, 4, 4);

        _sides[0].sharedMaterial.mainTexture = texture.CutTexture(4 + xOffset, 32 + yOffset, 4, 12);

        List<Texture> result = new()
        {
            //_top.material.mainTexture,
            _bottom.material.mainTexture,
            _sides[0].material.mainTexture
        };

        return result;
    }
}