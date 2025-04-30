using UnityEngine;

public static class Texture2DUtils
{
    public static Texture2D CutTexture(this Texture2D inputTexture, int x, int y, int width, int height)
    {
        Texture2D texture = new(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        Color[] pixels = inputTexture.GetPixels(x, y, width, height);

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}