using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Custom Card", menuName = "Card Game/Cards/Custom Card")]
public class CustomCard : BaseCard
{
    [SerializeField] private Effect[] customEffects;

    private void OnEnable()
    {
        cardType = CardType.Custom;
    }

    public override Effect[] GetEffects()
    {
        Debug.Log($"Getting effects for custom card: {cardName}");
        return customEffects;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        cardType = CardType.Custom; // Ensure type cannot be changed
        if (customEffects == null || customEffects.Length == 0)
        {
            Debug.LogWarning($"Custom card {cardName} has no effects");
        }
    }
}