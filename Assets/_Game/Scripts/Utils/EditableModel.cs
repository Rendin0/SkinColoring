using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EditableModel : MonoBehaviour
{
    public int PixelsPerScale = 4;


    [SerializeField] private Head _head;

    [SerializeField] private Chest _chest;
    
    [SerializeField] private Arm _leftArm;
    [SerializeField] private Arm _rightArm;

    [SerializeField] private Leg _leftLeg;
    [SerializeField] private Leg _rightLeg;

    private readonly List<Texture2D> _textures = new();

    private void Awake()
    {
        // Список всех текстур у всех объектов модели
        //foreach (var part in BodyParts)
        //{
        //    part.GetComponentsInChildren<Renderer>().ToList().ForEach(renderer => _textures.Add(renderer.sharedMaterial.mainTexture as Texture2D));
        //}
    }

    public List<Color> GetAllPixels()
    {
        var pixels = new List<Color>();
        _textures.ForEach(texture => pixels.AddRange(texture.GetPixels().ToList()));
        return pixels;
    }
}