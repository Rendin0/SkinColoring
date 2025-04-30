using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditableModel))]
public class ModelCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
