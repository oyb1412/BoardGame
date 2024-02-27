using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Sample))]
public class SampleEditor : Editor
{
    Sample _Sample;

    private void OnEnable() {
        _Sample = (Sample)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        _Sample.number = EditorGUILayout.IntField("NUMBER", _Sample.number);
    }
}
#endif
