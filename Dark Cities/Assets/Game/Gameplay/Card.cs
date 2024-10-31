using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [Header("Card Visual Elements")]
    [SerializeField] private Image cardArtworkImage;
    [SerializeField] private Image monsterEffectArtworkImage;
    [SerializeField] private TextMeshProUGUI cardNameText;
    
    [Header("Effect Text Elements")]
    [SerializeField] private TextMeshProUGUI villageEffectText;
    [SerializeField] private TextMeshProUGUI attackEffectText;
    [SerializeField] private TextMeshProUGUI monsterEffectText;
    
    [Header("Game State")]
    [SerializeField] private GameState gameState;
    
    private CardData cardData;
    
    public void InitializeCard(CardData data)
    {
        cardData = data;
        UpdateVisuals();
        UpdateEffectTexts();
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
        }
        
        // Update monster effect artwork if it exists
        if (monsterEffectArtworkImage != null)
        {
            monsterEffectArtworkImage.sprite = cardData.monsterEffectArtwork;
            monsterEffectArtworkImage.gameObject.SetActive(cardData.monsterEffectArtwork != null);
        }
        
        // Update card name
        if (cardNameText != null)
        {
            cardNameText.text = cardData.cardName;
        }
    }
    
    private void UpdateEffectTexts()
    {
        // Update village effect text
        if (villageEffectText != null)
        {
            villageEffectText.text = cardData.villageEffect?.effectDescription ?? "No effect";
        }
        
        // Update attack effect text
        if (attackEffectText != null)
        {
            attackEffectText.text = cardData.attackEffect?.effectDescription ?? "No effect";
        }
        
        // Update monster effect text
        if (monsterEffectText != null)
        {
            monsterEffectText.text = cardData.monsterEffect?.effectDescription ?? "No effect";
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