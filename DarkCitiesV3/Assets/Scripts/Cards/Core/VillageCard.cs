using UnityEngine;
[CreateAssetMenu(fileName = "New Village Card", menuName = "Card Game/Cards/Village Card")]
public class VillageCard : BaseCard
{
    [SerializeField] private Effect[] villageEffects = new Effect[2];

    private void OnEnable()
    {
        cardType = CardType.Village;
    }

    public override Effect[] GetEffects()
    {
        Debug.Log($"Getting effects for village card: {cardName}");
        return villageEffects;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        cardType = CardType.Village; // Ensure type cannot be changed
        if (villageEffects.Length != 2)
        {
            Debug.LogError($"Village card {cardName} must have exactly 2 effects");
            villageEffects = new Effect[2];
        }
        foreach (var effect in villageEffects)
        {
            if (effect == null)
            {
                Debug.LogWarning($"Village effect is missing on {cardName}");
            }
        }
    }
}