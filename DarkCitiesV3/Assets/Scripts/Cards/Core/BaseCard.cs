using UnityEngine;

public abstract class BaseCard : ScriptableObject, ICard
{
    [SerializeField] protected string cardName;
    [SerializeField] protected Sprite cardImage;
    [SerializeField] protected CardType cardType;

    public string Name => cardName;
    public Sprite Image => cardImage;
    public CardType Type => cardType;

    public abstract Effect[] GetEffects();

    protected virtual void OnValidate()
    {
        Debug.Log($"Validating card: {cardName}");
        if (string.IsNullOrEmpty(cardName))
        {
            Debug.LogWarning($"Card name is empty on {name}");
        }
        if (cardImage == null)
        {
            Debug.LogWarning($"Card image is missing on {cardName}");
        }
    }
}