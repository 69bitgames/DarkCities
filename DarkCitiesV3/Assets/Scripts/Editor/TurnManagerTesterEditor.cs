#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TurnManagerTester))]
public class TurnManagerTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TurnManagerTester tester = (TurnManagerTester)target;
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Turn Controls", EditorStyles.boldLabel);

        if (GUILayout.Button("Start Game"))
        {
            TurnManager.Instance.StartGame();
        }

        EditorGUILayout.Space(5);
        if (GUILayout.Button("Next Phase"))
        {
            TurnManager.Instance.NextPhase();
        }

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Current State", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.LabelField("Current Turn:", TurnManager.Instance?.CurrentTurn.ToString() ?? "No game in progress");
        EditorGUILayout.LabelField("Current Player:", TurnManager.Instance?.CurrentPlayer.ToString() ?? "No game in progress");
        EditorGUILayout.LabelField("Current Phase:", TurnManager.Instance?.CurrentPhase.ToString() ?? "No game in progress");
        EditorGUI.EndDisabledGroup();
    }
}
#endif