using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    private static DeckManager instance;
    public static DeckManager Instance => instance;

    [SerializeField] private List<MonsterCard> monsterCards = new();
    [SerializeField] private List<VillageCard> villageCards = new();
    [SerializeField] public int startingHandSize = 5;

    private List<MainDeckCard> mainDeck = new();
    private List<MainDeckCard> discardPile = new();
    private int madnessCount = 0;

    public int MadnessCount => madnessCount;
    public int RemainingCards => mainDeck.Count;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void InitializeDeck()
    {
        Debug.Log("Initializing deck");
        mainDeck.Clear();
        discardPile.Clear();
        madnessCount = 0;

        // Create main deck by combining monster and village cards
        for (int i = 0; i < Mathf.Min(monsterCards.Count, villageCards.Count); i++)
        {
            var mainCard = ScriptableObject.CreateInstance<MainDeckCard>();
            mainCard.InitializeCard(monsterCards[i], villageCards[i]);
            mainDeck.Add(mainCard);
        }

        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        Debug.Log("Shuffling deck");
        for (int i = mainDeck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (mainDeck[i], mainDeck[j]) = (mainDeck[j], mainDeck[i]);
        }
    }

    public MainDeckCard DrawCard()
    {
        if (mainDeck.Count == 0)
        {
            madnessCount++;
            Debug.Log($"Deck empty! Madness count: {madnessCount}");
            return null;
        }

        var card = mainDeck[mainDeck.Count - 1];
        mainDeck.RemoveAt(mainDeck.Count - 1);
        Debug.Log($"Drew card: {card.Name}");
        return card;
    }

    public void DiscardCard(MainDeckCard card)
    {
        Debug.Log($"Discarding card: {card.Name}");
        discardPile.Add(card);
    }
}