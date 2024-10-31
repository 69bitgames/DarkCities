using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VillageCard))]
public class VillageCardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        VillageCard card = (VillageCard)target;
        
        EditorGUILayout.LabelField("Card Identity", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardName"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Artwork", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardTemplate"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardArtwork"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("villageEffect"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackEffect"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effect Descriptions", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("villageEffectDescription"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackEffectDescription"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Flavor", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flavorText"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Monster Compatibility", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("compatibleMonsters"));
        
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomEditor(typeof(MonsterCard))]
public class MonsterCardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        MonsterCard card = (MonsterCard)target;
        
        EditorGUILayout.LabelField("Card Identity", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("monsterName"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Artwork", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardTemplate"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("monsterArtwork"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effect", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("monsterEffect"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effect Description", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("monsterEffectDescription"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Flavor", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flavorText"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Monster Type", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("monsterType"));
        
        serializedObject.ApplyModifiedProperties();
    }
}