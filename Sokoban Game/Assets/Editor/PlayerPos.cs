using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerPos : Editor {
    public Player selected;
    private void OnEnable() {
        selected = (Player)target;
    }

    public override void OnInspectorGUI() {
        if (selected == null)
            return;
        selected.x = EditorGUILayout.FloatField("목표 x", selected.x);
        selected.y = EditorGUILayout.FloatField("목표 y", selected.y);

        if (GUILayout.Button("Pos")) {
            selected.transform.position = new Vector2(selected.x + 0.5f, selected.y + 0.5f);
        }
        base.OnInspectorGUI();
    }
}
