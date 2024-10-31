using UnityEngine;
using GameEnums;
[CreateAssetMenu(fileName = "New Village Card", menuName = "Cards/Village Card")]
public class VillageCard : ScriptableObject
{
    [Header("Card Identity")]
    public string cardName;
    
    [Header("Artwork")]
    public Sprite cardTemplate;
    public Sprite cardArtwork;
    
    [Header("Effects")]
    public CardEffect villageEffect;
    public CardEffect attackEffect;
    
    // Effect descriptions for display
    [Header("Effect Descriptions")]
    [TextArea(2, 4)]
    public string villageEffectDescription;
    [TextArea(2, 4)]
    public string attackEffectDescription;
    
    [Header("Flavor")]
    [TextArea(2, 4)]
    public string flavorText;
    
    [Header("Monster Compatibility")]
    [Tooltip("Select which monsters can use this card")]
    public AvailableMonsters compatibleMonsters = AvailableMonsters.All;
    
    public bool IsCompatibleWith(AvailableMonsters monster)
    {
        if (compatibleMonsters == AvailableMonsters.All) return true;
        return (compatibleMonsters & monster) == monster;
    }
}

[CreateAssetMenu(fileName = "New Monster Card", menuName = "Cards/Monster Card")]
public class MonsterCard : ScriptableObject
{
    [Header("Card Identity")]
    public string monsterName;
    
    [Header("Artwork")]
    public Sprite cardTemplate;
    public Sprite monsterArtwork;
    
    [Header("Effect")]
    public CardEffect monsterEffect;
    
    // Effect description for display
    [Header("Effect Description")]
    [TextArea(2, 4)]
    public string monsterEffectDescription;
    
    [Header("Flavor")]
    [TextArea(2, 4)]
    public string flavorText;
    
    public AvailableMonsters monsterType;
}