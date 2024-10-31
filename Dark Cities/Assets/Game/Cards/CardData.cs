using UnityEngine;

[CreateAssetMenu(fileName = "New Card Data", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Card Identity")]
    public string cardName;
    
    [Header("Artwork")]
    public Sprite cardTemplate;  // Added this field
    public Sprite cardArtwork;
    public Sprite monsterEffectArtwork;
    
    [Header("Card Type")]
    public bool isMonsterCard;
    
    [Header("Effects")]
    public CardEffect villageEffect;
    public CardEffect attackEffect;
    public CardEffect monsterEffect;
    
    [Header("Text Display")]
    [TextArea(2,4)]
    public string flavorText;
    
    // Helper methods
    public bool HasVillageEffect => villageEffect != null;
    public bool HasAttackEffect => attackEffect != null;
    public bool HasMonsterEffect => monsterEffect != null;
}