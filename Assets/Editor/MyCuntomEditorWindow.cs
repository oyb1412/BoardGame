using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyCuntomEditorWindow : EditorWindow
{
    GUILayoutOption[] size100x50 = { GUILayout.Width(100f), GUILayout.Height(50f) };
    Object _target;
    [MenuItem("Window/MyCustomEditorWindow")]
    public static void ShowWindow() {
        MyCuntomEditorWindow window = GetWindow<MyCuntomEditorWindow>();
        window.Show();
    }
    private void OnGUI() {
        DrawTitle();
        DrawTestButton();
        DrawObjectField();
    }

    void DrawTitle() {
        GUILayout.Label("My CustomWindow", EditorStyles.label);
    }

    void DrawTestButton() {
        if(GUILayout.Button("Do Text", size100x50)) {
            Debug.Log("button");
        }
    }

    void DrawObjectField() {
        _target = EditorGUILayout.ObjectField(_target, typeof(Object), true);
    }
}
