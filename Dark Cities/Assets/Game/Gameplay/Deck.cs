using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    [Header("Deck Settings")]
    [SerializeField] private int maxHandSize = 7;
    [SerializeField] private int initialDrawCount = 4;
    
    [Header("Deck References")]
    [SerializeField] private List<VillageCard> villageDeck;  // Village card templates
    [SerializeField] private List<MonsterCard> monsterDeck;  // Monster card templates
    
    [Header("UI References")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handContainer;
    
    [Header("Game State")]
    [SerializeField] private GameState gameState;  // Reference to game state
    
    private List<CardData> gameDeck = new List<CardData>();
    private List<GameObject> handObjects = new List<GameObject>();
    
    void Start()
    {
        if (cardPrefab == null || handContainer == null)
        {
            Debug.LogError("Deck: Missing required references. Please assign Card Prefab and Hand Container.");
            return;
        }
        
        if (gameState == null)
        {
            Debug.LogError("Deck: Missing GameState reference. Please assign GameState.");
            return;
        }
        
        CreateGameDeck();
        ShuffleDeck();
        DrawInitialHand();
    }
    
    private void CreateGameDeck()
    {
        gameDeck.Clear();
        
        // Create a working copy of monster cards that we'll remove from as we use them
        List<MonsterCard> availableMonsterCards = new List<MonsterCard>(monsterDeck);
        
        // Convert village cards to CardData and add to game deck
        foreach (VillageCard villageCard in villageDeck)
        {
            CardData newCard = ScriptableObject.CreateInstance<CardData>();
            newCard.cardName = villageCard.cardName;
            newCard.cardArtwork = villageCard.cardArtwork;
            newCard.cardTemplate = villageCard.cardTemplate;
            newCard.isMonsterCard = false;
            newCard.flavorText = villageCard.flavorText;
            
            // Convert effect descriptions to CardEffects (you'll need to implement this based on your effect system)
            newCard.villageEffect = CreateEffectFromDescription(villageCard.villageEffectDescription);
            newCard.attackEffect = CreateEffectFromDescription(villageCard.attackEffectDescription);
            
            gameDeck.Add(newCard);
        }
        
        // Add monster effects to village cards
        foreach (CardData card in gameDeck)
        {
            if (!card.isMonsterCard && availableMonsterCards.Count > 0)
            {
                // Randomly select a monster card
                int randomIndex = Random.Range(0, availableMonsterCards.Count);
                MonsterCard randomMonsterCard = availableMonsterCards[randomIndex];
                
                // Assign the monster effect
                card.monsterEffectArtwork = randomMonsterCard.monsterArtwork;
                card.monsterEffect = CreateEffectFromDescription(randomMonsterCard.monsterEffectDescription);
                
                // Remove the used monster card from the available pool
                availableMonsterCards.RemoveAt(randomIndex);
                
                Debug.Log($"Assigned {randomMonsterCard.monsterName}'s effect to {card.cardName}. " +
                         $"Remaining monster effects: {availableMonsterCards.Count}");
            }
            else if (!card.isMonsterCard && availableMonsterCards.Count == 0)
            {
                Debug.LogWarning("Ran out of monster effects to assign! Some village cards may not have monster effects.");
            }
        }
        
        Debug.Log($"Game deck created with {gameDeck.Count} cards");
    }
    
    // Temporary method to create CardEffect from description
    // This will need to be replaced with proper effect creation based on your effect system
    private CardEffect CreateEffectFromDescription(string description)
    {
        if (string.IsNullOrEmpty(description)) return null;
        
        CardEffect effect = ScriptableObject.CreateInstance<CardEffect>();
        effect.effectDescription = description;
        return effect;
    }
    
    private void ShuffleDeck()
    {
        int n = gameDeck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            CardData temp = gameDeck[k];
            gameDeck[k] = gameDeck[n];
            gameDeck[n] = temp;
        }
    }
    
    [ContextMenu("Draw Card")]
    public void DrawCardButton()
    {
        DrawCard();
    }
    
    private bool DrawCard()
    {
        if (handObjects.Count >= maxHandSize)
        {
            Debug.LogWarning("Cannot draw: Hand is full");
            return false;
        }
        
        if (gameDeck.Count == 0)
        {
            Debug.LogWarning("Cannot draw: Deck is empty");
            return false;
        }
        
        CardData cardToSpawn = gameDeck[0];
        gameDeck.RemoveAt(0);
        
        GameObject newCard = Instantiate(cardPrefab, handContainer);
        Card cardComponent = newCard.GetComponent<Card>();
        if (cardComponent != null)
        {
            cardComponent.InitializeCard(cardToSpawn);
        }
        
        handObjects.Add(newCard);
        Debug.Log($"Drew card: {cardToSpawn.cardName}. Hand size: {handObjects.Count}");
        return true;
    }
    
    private void DrawInitialHand()
    {
        for (int i = 0; i < initialDrawCount; i++)
        {
            DrawCard();
        }
    }
    
    public void ClearHand()
    {
        foreach (GameObject card in handObjects)
        {
            Destroy(card);
        }
        handObjects.Clear();
    }
    
    public void PrintDeckStatus()
    {
        Debug.Log($"Deck count: {gameDeck.Count}");
        Debug.Log($"Hand count: {handObjects.Count}");
    }
}