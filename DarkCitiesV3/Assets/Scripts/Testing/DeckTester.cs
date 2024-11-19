using UnityEngine;

public class DeckTester : MonoBehaviour
{
    public DeckManager deckManager;
    public HandManager handManager;

    private void Awake()
    {
        if (deckManager == null)
            Debug.LogError("DeckManager reference missing!");
        if (handManager == null)
            Debug.LogError("HandManager reference missing!");
    }
}