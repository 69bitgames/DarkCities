using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Main Deck Card", menuName = "Card Game/Cards/Main Deck Card")]
public class MainDeckCard : BaseCard
{
    [SerializeField] private MonsterCard monsterCard;
    [SerializeField] private VillageCard villageCard;

    private void OnEnable()
    {
        cardType = CardType.MainDeck;
    }

    public MonsterCard MonsterCard => monsterCard;
    public VillageCard VillageCard => villageCard;

    public override Effect[] GetEffects()
    {
        Debug.Log($"Getting combined effects for main deck card: {cardName}");
        var monsterEffects = monsterCard?.GetEffects() ?? new Effect[0];
        var villageEffects = villageCard?.GetEffects() ?? new Effect[0];
        
        var allEffects = new List<Effect>();
        if (monsterEffects != null) allEffects.AddRange(monsterEffects);
        if (villageEffects != null) allEffects.AddRange(villageEffects);
        return allEffects.ToArray();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        cardType = CardType.MainDeck; // Ensure type cannot be changed
        if (monsterCard == null)
        {
            Debug.LogWarning($"Monster card is missing on {cardName}");
        }
        if (villageCard == null)
        {
            Debug.LogWarning($"Village card is missing on {cardName}");
        }
    }
}