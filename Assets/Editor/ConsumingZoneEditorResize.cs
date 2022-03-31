using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConsumingZone))]
public class ConsumingZoneEditorResize : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Resize zone to sprite size"))
        {
            ((ConsumingZone)target).resizeZoneOfSprite();
        }
    }
}
