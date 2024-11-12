using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DarkCities.Cards;
namespace DarkCities.UI
{
    [RequireComponent(typeof(Image))]
    public class CardUIHandler : MonoBehaviour, IPointerClickHandler
    {
        [Header("Card Data")]
        [SerializeField] private CardData cardData;
        
        [Header("References")]
        [Tooltip("Reference to the main card display in the canvas")]
        [SerializeField] private CardDisplay mainCardDisplay;
        
        [Header("Settings")]
        [SerializeField] private bool highlightOnHover = true;
        
        // Cached references
        [SerializeField] private Image cardImage;
        private Color originalColor;

        private void Awake()
        {
            // Cache the image component
            if (cardImage == null)
            cardImage = GetComponent<Image>();
            originalColor = cardImage.color;
            
            // Set up the initial card image
            if (cardData != null && cardData.CreateCard()?.Artwork != null)
            {
                cardImage.sprite = cardData.CreateCard().Artwork;
            }
        }

        private void OnValidate()
        {
            // Auto-find the main card display if not set
            if (mainCardDisplay == null)
            {
                mainCardDisplay = FindObjectOfType<CardDisplay>();
                if (mainCardDisplay == null)
                {
                    Debug.LogWarning("No CardDisplay found in scene. Please assign manually.");
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (cardData != null && mainCardDisplay != null)
            {
                Card card = cardData.CreateCard();
                if (card != null)
                {
                    mainCardDisplay.DisplayCard(card);
                    Debug.Log(card.CardTitle);
                }
                else
                {
                    Debug.LogError($"Failed to create card from CardData: {cardData.name}");
                }
            }
            else
            {
                Debug.LogError("Missing required references: CardData or MainCardDisplay");
            }
        }

        // Optional: Add hover effects
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (highlightOnHover)
            {
                cardImage.color = new Color(originalColor.r * 1.2f, originalColor.g * 1.2f, originalColor.b * 1.2f, originalColor.a);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (highlightOnHover)
            {
                cardImage.color = originalColor;
            }
        }

        // Method to set card data at runtime if needed
        public void SetCardData(CardData newCardData)
        {
            cardData = newCardData;
            if (cardData != null && cardImage != null)
            {
                Card card = cardData.CreateCard();
                if (card?.Artwork != null)
                {
                    cardImage.sprite = card.Artwork;
                }
            }
        }
    }
}