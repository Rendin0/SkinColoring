using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EditableModel : MonoBehaviour
{
    [SerializeField] private Head _head;

    [SerializeField] private Chest _chest;
    
    [SerializeField] private Arm _leftArm;
    [SerializeField] private Arm _rightArm;

    [SerializeField] private Leg _leftLeg;
    [SerializeField] private Leg _rightLeg;

    private readonly List<Texture2D> _textures = new();

    public void SetModelTexture(Texture2D texture)
    {
        _textures.Clear();

        _textures.AddRange(_head.SetTexture(texture).Cast<Texture2D>());
        _textures.AddRange(_chest.SetTexture(texture).Cast<Texture2D>());

        _textures.AddRange(_rightLeg.SetTexture(texture).Cast<Texture2D>());
        _textures.AddRange(_leftLeg.SetTexture(texture, 16, -32).Cast<Texture2D>());

        _textures.AddRange(_rightArm.SetTexture(texture).Cast<Texture2D>());
        _textures.AddRange(_leftArm.SetTexture(texture, -8, -32).Cast<Texture2D>());
    }

    public List<Color> GetAllPixels()
    {
        var pixels = new List<Color>();
        _textures.ForEach(texture => pixels.AddRange(texture.GetPixels()));
        return pixels;
    }
}