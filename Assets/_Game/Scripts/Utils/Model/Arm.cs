
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Arm
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _bottom;
    [SerializeField] private List<Renderer> _sides;

    public List<Texture> SetTexture(Texture2D texture)
    {
        _top.material.mainTexture = texture.CutTexture(44, 44, 4, 4);
        _bottom.material.mainTexture = texture.CutTexture(48, 44, 4, 4);

        _sides[0].sharedMaterial.mainTexture = texture.CutTexture(44, 32, 4, 12);

        List<Texture> result = new()
        {
            _top.material.mainTexture,
            _bottom.material.mainTexture,
            _sides[0].material.mainTexture
        };


        return result;
    }

}
