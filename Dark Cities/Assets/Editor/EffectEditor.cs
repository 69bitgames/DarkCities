using UnityEditor;

[CustomEditor(typeof(CardEffect))]
public class CardEffectEditor : BaseEffectEditor
{
    protected override void DrawChildProperties()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Card Effect Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isHidden"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("targetRequired"));
    }
}