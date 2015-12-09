using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Floor))]
public class FloorUI : Editor {

    public int RoomCount = 16;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RoomCount = EditorGUILayout.IntField("Generated Room Count",RoomCount);
        if (GUILayout.Button("Generate")) (target as Floor).GenerateFloor(RoomCount);
        if (GUILayout.Button("Clear")) (target as Floor).ClearFloor();
    }
}
