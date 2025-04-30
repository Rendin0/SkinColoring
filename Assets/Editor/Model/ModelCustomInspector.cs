using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditableModel))]
public class ModelCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set Values"))
        {
            var model = target as EditableModel;

            SetSizes(model);
        }
    }

    public void SetSizes(EditableModel model)
    {
        List<EditableTexture> textures = new();

        //model.BodyParts.ForEach(limb => textures.AddRange(limb.GetComponentsInChildren<EditableTexture>()));

        foreach (var texture in textures)
        {
            texture.SetTextureSize(model.PixelsPerScale * texture.transform.localScale.FloorToVector2Int());
            EditorUtility.SetDirty(texture);
        }
    }
}
