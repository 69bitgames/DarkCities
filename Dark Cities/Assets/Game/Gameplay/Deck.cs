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
        
        // Now we're explicitly requesting the correct type
        newCard.villageEffect = villageCard.villageEffect as VillageEffect;
        newCard.attackEffect = villageCard.attackEffect as AttackEffect;
        int index = Random.Range(0, availableMonsterCards.Count);
        Debug.Log(availableMonsterCards[index].monsterEffect.EffectDescription);
        newCard.monsterEffect = availableMonsterCards[index].monsterEffect as MonsterEffect;
        availableMonsterCards.RemoveAt(index);
        gameDeck.Add(newCard);
    }
}
    // Temporary method to create CardEffect from description
    // This will need to be replaced with proper effect creation based on your effect system
private T CreateEffectFromDescription<T>(string description) where T : CardEffect
{
    if (string.IsNullOrEmpty(description)) return null;
    
    // Create the appropriate effect type based on the type parameter
    T effect = null;
    
    if (typeof(T) == typeof(VillageEffect))
    {
        Debug.Log("Creating Village Effect");
        Debug.Log(effect);
        effect = ScriptableObject.CreateInstance<VillageEffect>() as T;
        Debug.Log(effect);
    }
    else if (typeof(T) == typeof(AttackEffect))
    {
        effect = ScriptableObject.CreateInstance<AttackEffect>() as T;
    }
    else if (typeof(T) == typeof(MonsterEffect))
    {
        effect = ScriptableObject.CreateInstance<MonsterEffect>() as T;
    }
    else
    {
        effect = ScriptableObject.CreateInstance<SimpleEffect>() as T;
    }
    
    effect?.Initialize();
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