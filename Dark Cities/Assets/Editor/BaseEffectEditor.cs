using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseEffect), true)]
public class BaseEffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Effect Information", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("effectName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("effectDescription"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Timing", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("timing"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("trigger"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Requirements", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("villagerCost"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("requiresMonsterAscended"));
        
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("requiresConstruction"));
        
        if (serializedObject.FindProperty("requiresConstruction").boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("constructionType"));
            EditorGUI.indentLevel--;
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Components", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("componentsList"));
        
        DrawChildProperties();
        
        serializedObject.ApplyModifiedProperties();
    }
    
    protected virtual void DrawChildProperties() { }
}