using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FactoryStack))]
public class ZoneEditorResize : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Resize sprite"))
        {
            ((FactoryStack)target).spriteResize();
        }
    }
}
