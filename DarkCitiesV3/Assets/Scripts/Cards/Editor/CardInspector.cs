#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseCard), true)]
public class CardInspector : Editor
{
    SerializedProperty cardName;
    SerializedProperty cardImage;
    SerializedProperty cardType;
    SerializedProperty monsterEffect;
    SerializedProperty villageEffects;
    SerializedProperty customEffects;
    SerializedProperty monsterCard;
    SerializedProperty villageCard;

    protected virtual void OnEnable()
    {
        cardName = serializedObject.FindProperty("cardName");
        cardImage = serializedObject.FindProperty("cardImage");
        cardType = serializedObject.FindProperty("cardType");
        
        // Try to find all possible properties
        monsterEffect = serializedObject.FindProperty("monsterEffect");
        villageEffects = serializedObject.FindProperty("villageEffects");
        customEffects = serializedObject.FindProperty("customEffects");
        monsterCard = serializedObject.FindProperty("monsterCard");
        villageCard = serializedObject.FindProperty("villageCard");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(cardName);
        EditorGUILayout.PropertyField(cardImage);
        EditorGUILayout.PropertyField(cardType);

        EditorGUILayout.Space(10);

        // Draw different properties based on card type
        CardType currentType = (CardType)cardType.enumValueIndex;
        switch (currentType)
        {
            case CardType.Monster:
                if (monsterEffect != null)
                {
                    EditorGUILayout.PropertyField(monsterEffect);
                }
                break;

            case CardType.Village:
                if (villageEffects != null)
                {
                    EditorGUILayout.PropertyField(villageEffects);
                }
                break;

            case CardType.MainDeck:
                if (monsterCard != null && villageCard != null)
                {
                    EditorGUILayout.PropertyField(monsterCard);
                    EditorGUILayout.PropertyField(villageCard);
                }
                break;

            case CardType.Custom:
                if (customEffects != null)
                {
                    EditorGUILayout.PropertyField(customEffects);
                }
                break;
        }

        // Debug section
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Debug Information", EditorStyles.boldLabel);
        
        BaseCard card = (BaseCard)target;
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.LabelField("Effects Count:", card.GetEffects().Length.ToString());
        EditorGUI.EndDisabledGroup();

        if (GUILayout.Button("Log Card Details"))
        {
            Debug.Log($"Card Name: {card.Name}");
            Debug.Log($"Card Type: {card.Type}");
            Debug.Log($"Effects Count: {card.GetEffects().Length}");
            foreach (var effect in card.GetEffects())
            {
                Debug.Log($"Effect: {effect.EffectName}");
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif