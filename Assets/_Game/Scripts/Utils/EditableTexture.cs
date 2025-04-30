using UnityEngine;

public class EditableTexture : MonoBehaviour
{
    [SerializeField] private Vector2Int _textureSize;

    private Material _sharedMaterial;
    private Texture2D _texture;

    private void Awake()
    {
        _sharedMaterial = GetComponent<Renderer>().sharedMaterial;
        SetTexture();
    }

    public void SetTextureSize(Vector2Int size)
    {
        _textureSize = size;
    }

    private void SetTexture()
    {
        if (_sharedMaterial.mainTexture == null)
        {
            _texture = new Texture2D(_textureSize.x, _textureSize.y, TextureFormat.RGBA32, false)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };
            _sharedMaterial.SetTexture("_BaseMap", _texture);
        }
        else
        {
            _texture = (Texture2D)_sharedMaterial.mainTexture;
        }
    }

    public void ChangePixelColor(RaycastHit hit, Color color)
    {
        var clickPoint = CalculateClickPoint(hit);
        ColorPixel(clickPoint, color);
    }
    public void ColorAllPixels(Color32 color)
    {
        Color32[] pixels = _texture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        _texture.SetPixels32(pixels);
        _texture.Apply();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Пиксель, на который указывает луч, в диапазоне _textureSize</returns>
    private Vector2Int CalculateClickPoint(RaycastHit hit)
    {
        var localHitPoint = hit.transform.InverseTransformPoint(hit.point);

        // Смещение в диапазон от 0 до 1,
        // где 0;0 это левый нижний угол, а 1;1 - правый верхний
        localHitPoint += new Vector3(.5f, .5f, 0f);
        var pointInTextureSize = (Vector2)localHitPoint * _textureSize;

        return new Vector2Int(Mathf.FloorToInt(pointInTextureSize.x), Mathf.FloorToInt(pointInTextureSize.y));
    }

    private void ColorPixel(Vector2Int point, Color color)
    {
        _texture.SetPixel(point.x, point.y, color);
        _texture.Apply();
    }

}
