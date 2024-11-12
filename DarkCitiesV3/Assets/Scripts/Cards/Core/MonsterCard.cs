using UnityEngine;

[CreateAssetMenu(fileName = "New Monster Card", menuName = "Card Game/Cards/Monster Card")]
public class MonsterCard : BaseCard
{
    [SerializeField] private Effect monsterEffect;

    private void OnEnable()
    {
        cardType = CardType.Monster;
    }

    public override Effect[] GetEffects()
    {
        Debug.Log($"Getting effects for monster card: {cardName}");
        return new Effect[] { monsterEffect };
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        cardType = CardType.Monster; // Ensure type cannot be changed
        if (monsterEffect == null)
        {
            Debug.LogWarning($"Monster effect is missing on {cardName}");
        }
    }
}