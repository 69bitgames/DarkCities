#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Effect))]
public class EffectInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Effect effect = (Effect)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Effect Inspector", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        // Add custom inspector elements
        if (GUILayout.Button("Test Can Use Effect"))
        {
            Debug.Log($"Testing if effect can be used: {effect.CanUseEffect()}");
        }

        if (GUILayout.Button("Test Use Effect"))
        {
            EffectData data = effect.UseEffect();
            Debug.Log($"Effect test result: {(data.wasSuccessful ? "Success" : "Failed")}");
        }

        EditorGUILayout.Space(10);
        
        // Draw the default inspector
        DrawDefaultInspector();
    }
}
#endif