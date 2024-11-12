using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [Header("Card Visual Elements")]
    [SerializeField] public Image cardArtworkImage;
    [SerializeField] public Image monsterEffectImage;
    [SerializeField] public Image cardBackground;
    [SerializeField] public TextMeshProUGUI cardNameText;
    
    [Header("Effect Text Elements")]
    [SerializeField] public TextMeshProUGUI villageEffectText;
    [SerializeField] public TextMeshProUGUI attackEffectText;
    [SerializeField] public TextMeshProUGUI monsterEffectText;
    
    [Header("Game State")]
    [SerializeField] private GameState gameState;

    [SerializeField] private GameObject cardSelectCanvas;


    public CardData cardData;
    
    public void InitializeCard(CardData data, GameState state = null)
    {
        cardData = data;
        if (state != null) gameState = state;
        
        UpdateVisuals();
    }
    
    private void UpdateVisuals()
    {
        if (cardData == null)
        {
            Debug.LogError("Card: No card data assigned");
            return;
        }
        
        // Update card artwork
        if (cardArtworkImage != null)
        {
            cardArtworkImage.sprite = cardData.cardArtwork;
            cardArtworkImage.enabled = cardData.cardArtwork != null;
        }
        
        // Update monster effect artwork
        if (monsterEffectImage != null)
        {
            Debug.Log("MonsEf");
            monsterEffectImage.sprite = cardData.monsterEffectArtwork;
            monsterEffectImage.enabled = cardData.monsterEffectArtwork != null;
        }

        // Update card background if it exists
        if (cardBackground != null)
        {
            cardBackground.sprite = cardData.cardTemplate;
            cardBackground.enabled = cardData.cardTemplate != null;
        }
        
        // Update card name
        if (cardNameText != null)
        {
            cardNameText.text = cardData.cardName;
        }
        
        // Update effect texts
        UpdateEffectTexts();
    }

    public void OpenSelectCanvas(){
        cardSelectCanvas = GameObject.Find("FixMe");
        foreach (Transform child in cardSelectCanvas.transform)
        {
            child.gameObject.SetActive(true);
        }
        CardSelectScript cardSelectScript = cardSelectCanvas.GetComponent<CardSelectScript>();
        if (cardSelectScript != null)
        {
            Debug.Log("Card Data Before Initialize");
            Debug.Log(this.cardData.cardName);
            Debug.Log(this.cardData.villageEffect.EffectDescription);

            cardSelectScript.Initialize(this);
        }
    }
    
    private void UpdateEffectTexts()
    {
        // Update village effect text
        if (villageEffectText != null)
        {
            villageEffectText.text = cardData.HasVillageEffect 
                ? cardData.villageEffect.EffectDescription 
                : "No village effect";
        }
        
        // Update attack effect text
        if (attackEffectText != null)
        {
            attackEffectText.text = cardData.HasAttackEffect 
                ? cardData.attackEffect.EffectDescription 
                : "No attack effect";
        }
        
        Debug.Log(cardData);
        // Update monster effect text
        if (monsterEffectText != null)
        {
            monsterEffectText.text = cardData.HasMonsterEffect 
                ? cardData.monsterEffect.EffectDescription 
                : "No monster effect";
        }
    }
    
    // Methods to trigger effects
    public void TriggerVillageEffect()
    {
        if (cardData.HasVillageEffect && gameState != null)
        {
            cardData.villageEffect.Execute(gameState);
        }
        else
        {
            Debug.Log("No village effect available or GameState not assigned");
        }
    }
    
    public void TriggerAttackEffect()
    {
        if (cardData.HasAttackEffect && gameState != null)
        {
            cardData.attackEffect.Execute(gameState);
        }
        else
        {
            Debug.Log("No attack effect available or GameState not assigned");
        }
    }
    
    public void TriggerMonsterEffect()
    {
        if (cardData.HasMonsterEffect && gameState != null)
        {
            cardData.monsterEffect.Execute(gameState);
        }
        else
        {
            Debug.Log("No monster effect available or GameState not assigned");
        }
    }
    
    // Debug method to test all effects
    [ContextMenu("Test All Effects")]
    public void TestAllEffects()
    {
        Debug.Log($"Testing all effects for card: {cardData?.cardName ?? "No Card Data"}");
        TriggerVillageEffect();
        TriggerAttackEffect();
        TriggerMonsterEffect();
    }
}