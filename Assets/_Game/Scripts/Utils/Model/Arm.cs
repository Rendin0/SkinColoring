
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Arm
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _bottom;
    [SerializeField] private Renderer _front;
    [SerializeField] private Renderer _back;
    [SerializeField] private Renderer _left;
    [SerializeField] private Renderer _right;

    public List<Texture> SetTexture(Texture2D texture, int xOffset = 0, int yOffset = 0, bool isLeft = false)
    {
        _top.material.mainTexture = texture.CutTexture(44+ xOffset, 44 + yOffset, 4, 4);
        _bottom.material.mainTexture = texture.CutTexture(48 + xOffset, 44 + yOffset, 4, 4);


        _left.material.mainTexture = texture.CutTexture(40 + xOffset, 32 + yOffset, 4, 12);
        _front.material.mainTexture = texture.CutTexture(44 + xOffset, 32 + yOffset, 4, 12);
        _right.material.mainTexture = texture.CutTexture(48 + xOffset, 32 + yOffset, 4, 12);
        _back.material.mainTexture = texture.CutTexture(52 + xOffset, 32 + yOffset, 4, 12);

        List<Texture> result = new()
        {
            _top.material.mainTexture,
            _bottom.material.mainTexture,
            _front.material.mainTexture,
            _back.material.mainTexture,
        };

        if (isLeft)
            result.Add(_left.material.mainTexture);
        else
            result.Add(_right.material.mainTexture);


            return result;
    }

}
