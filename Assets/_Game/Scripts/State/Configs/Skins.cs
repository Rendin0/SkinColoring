
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skins", menuName = "Configs/New Skins file")]
public class Skins : ScriptableObject
{
    public Texture2D BlankSkin;
    public List<Texture2D> SkinTextures;
}