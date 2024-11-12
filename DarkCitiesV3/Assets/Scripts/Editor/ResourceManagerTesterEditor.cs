using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ResourceManagerTester))]
public class ResourceManagerTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ResourceManagerTester tester = (ResourceManagerTester)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Test Controls", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Add 3 Villagers"))
        {
            tester.TestAddVillagers();
        }
        
        if (GUILayout.Button("Process Breeding"))
        {
            tester.TestBreeding();
        }
        
        if (GUILayout.Button("Change Status (2 to Busy)"))
        {
            tester.TestStatusChange();
        }
        
        if (GUILayout.Button("Increase Capacity"))
        {
            tester.TestCapacityIncrease();
        }
        
        EditorGUILayout.Space(5);
        if (GUILayout.Button("Print Current State"))
        {
            tester.PrintCurrentState();
        }
    }
}
#endif