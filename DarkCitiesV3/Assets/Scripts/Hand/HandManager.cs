using UnityEngine;
using System.Collections.Generic;
public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform handContainer;
    [SerializeField] private UICard uiCardPrefab;
    [SerializeField] private float cardSpacing = 10f;
    
    private List<UICard> cardsInHand = new();

    public void AddCard(MainDeckCard card)
    {
        Debug.Log($"Adding card to hand: {card.Name}");
        var uiCard = Instantiate(uiCardPrefab, handContainer);
        uiCard.SetupCard(card);
        cardsInHand.Add(uiCard);
        ArrangeHand();
    }

    public void RemoveCard(UICard card)
    {
        Debug.Log($"Removing card from hand");
        cardsInHand.Remove(card);
        Destroy(card.gameObject);
        ArrangeHand();
    }

    private void ArrangeHand()
    {
        float totalWidth = (cardsInHand.Count - 1) * cardSpacing;
        float startX = -totalWidth / 2;

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            var card = cardsInHand[i];
            card.transform.localPosition = new Vector3(startX + i * cardSpacing, 0, 0);
        }
    }

    public void DrawStartingHand()
    {
        Debug.Log("Drawing starting hand");
        for (int i = 0; i < DeckManager.Instance.startingHandSize; i++)
        {
            var card = DeckManager.Instance.DrawCard();
            if (card != null)
            {
                AddCard(card);
            }
        }
    }
}