#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UICard))]
public class UICardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UICard uiCard = (UICard)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("UI Card Inspector", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        DrawDefaultInspector();

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox(
            "Required Components:\n" +
            "- Card Artwork (Image)\n" +
            "- Card Name Text (TMP)\n" +
            "- Name Container (CanvasGroup)",
            MessageType.Info);
    }
}
#endif