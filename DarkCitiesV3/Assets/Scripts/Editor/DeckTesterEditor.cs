using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(DeckTester))]
public class DeckTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        DeckTester tester = (DeckTester)target;
        
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Initialize Deck"))
        {
            tester.deckManager.InitializeDeck();
        }
        
        if (GUILayout.Button("Draw Starting Hand"))
        {
            tester.handManager.DrawStartingHand();
        }
        
        if (GUILayout.Button("Draw One Card"))
        {
            var card = tester.deckManager.DrawCard();
            if (card != null)
            {
                tester.handManager.AddCard(card);
            }
        }

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField($"Cards in deck: {tester.deckManager.RemainingCards}");
        EditorGUILayout.LabelField($"Madness count: {tester.deckManager.MadnessCount}");
    }
}
#endif