using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UICardTester))]
public class UICardTesterEditor : Editor
{
    SerializedProperty testCard;
    SerializedProperty showDebugInfo;
    SerializedProperty currentCardName;
    SerializedProperty currentCardType;

    private void OnEnable()
    {
        testCard = serializedObject.FindProperty("testCard");
        showDebugInfo = serializedObject.FindProperty("showDebugInfo");
        currentCardName = serializedObject.FindProperty("currentCardName");
        currentCardType = serializedObject.FindProperty("currentCardType");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("UI Card Tester", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        // Draw test card field
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(testCard);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            ((UICardTester)target).UpdateDebugInfo();
        }

        EditorGUILayout.Space(10);
        
        // Preview button
        if (GUILayout.Button("Preview Card"))
        {
            ((UICardTester)target).PreviewCard();
        }

        EditorGUILayout.Space(5);
        
        // Debug info section
        EditorGUILayout.PropertyField(showDebugInfo);
        
        if (showDebugInfo.boolValue)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(currentCardName);
            EditorGUILayout.PropertyField(currentCardType);
            EditorGUI.EndDisabledGroup();

            UICardTester tester = (UICardTester)target;
            if (tester.TestCard != null)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("Effects:", EditorStyles.boldLabel);
                Effect[] effects = tester.TestCard.GetEffects();
                EditorGUI.indentLevel++;
                foreach (Effect effect in effects)
                {
                    if (effect != null)
                    {
                        EditorGUILayout.LabelField($"- {effect.EffectName} ({effect.EffectType})");
                    }
                    else
                    {
                        EditorGUILayout.LabelField("- Null Effect");
                    }
                }
                EditorGUI.indentLevel--;
            }
        }

        serializedObject.ApplyModifiedProperties();

        // Help box
        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox(
            "How to use:\n" +
            "1. Assign a card to 'Test Card'\n" +
            "2. Click 'Preview Card' to update the UI\n" +
            "3. Enable 'Show Debug Info' to see card details",
            MessageType.Info);
    }
}