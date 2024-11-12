using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using DarkCities.Effects;
using DarkCities.Cards;

namespace DarkCities.UI
{
    [Serializable]
    public class CardTypeEvent : UnityEvent<CardType> { }

    public class CardDisplay : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image cardArtwork;
        [SerializeField] private TextMeshProUGUI cardNameText;
        [SerializeField] private TextMeshProUGUI cardTitleText;
        [SerializeField] private TextMeshProUGUI villageEffectText;
        [SerializeField] private TextMeshProUGUI attackEffectText;
        [SerializeField] private TextMeshProUGUI monsterEffectText;

        [Header("Card Settings")]
        [SerializeField] private CardType effectTypeToPlay;
        [SerializeField] private bool preserveArtworkAspectRatio = true;

        [Header("Events")]
        public CardTypeEvent onPlayCard = new CardTypeEvent();

        private Card selectedCard;

        private EffectType ConvertCardTypeToEffectType(CardType cardType)
        {
            return cardType switch
            {
                CardType.Village => EffectType.Village,
                CardType.Monster => EffectType.Monster,
                CardType.Construction => EffectType.Attack,
                CardType.Attack => EffectType.Attack,
                _ => throw new ArgumentException($"Unexpected card type: {cardType}")
            };
        }

        public void DisplayCard(Cards.Card card)
        {
            this.gameObject.SetActive(true);
            if (card == null)
            {
                Debug.LogError("Attempted to display null card");
                return;
            }
            selectedCard = card;

            // Update artwork
            if (cardArtwork != null)
            {
                cardArtwork.sprite = card.Artwork;
                cardArtwork.preserveAspect = preserveArtworkAspectRatio;
            }
            else
            {
                Debug.LogError("Card artwork Image component not assigned");
            }

            // Update card name
            if (cardNameText != null)
            {
                cardNameText.text = card.CardName;
            }
            else
            {
                Debug.LogError("Card name TextMeshProUGUI component not assigned");
            }

            // Update card title
            if (cardTitleText != null)
            {
                cardTitleText.text = card.CardTitle; // Note: You'll need to add this property to your Card class
            }
            else
            {
                Debug.LogError("Card title TextMeshProUGUI component not assigned");
            }

            // Update village effect
            if (villageEffectText != null)
            {
                villageEffectText.text = card.Effects[0].Description; // Note: You'll need to add this property to your Card class
            }
            else
            {
                Debug.LogError("Village effect TextMeshProUGUI component not assigned");
            }

            // Update attack effect
            if (attackEffectText != null)
            {   
                attackEffectText.text = card.Effects[1].Description; // Note: You'll need to add this property to your Card class
            }
            else
            {
                Debug.LogError("Attack effect TextMeshProUGUI component not assigned");
            }

            // Update monster effect
            if (monsterEffectText != null)
            {
                monsterEffectText.text = card.Effects[2].Description; // Note: You'll need to add this property to your Card class
            }
            else
            {
                Debug.Log("Monster effect TextMeshProUGUI component not assigned");
            }
        }

        // Optional: Method to clear the display
        public void ClearDisplay()
        {
            if (cardArtwork != null)
                cardArtwork.sprite = null;
            
            if (cardNameText != null)
                cardNameText.text = string.Empty;
            
            if (cardTitleText != null)
                cardTitleText.text = string.Empty;
        }
        public void PlayVillage()
        {
            if (selectedCard != null)
            {
                Debug.Log("Playing Village effect");
                onPlayCard.Invoke(CardType.Village);
                var effects = selectedCard.GetEffectsByType(EffectType.Village);
                Debug.Log($"Found {effects.Length} Village effects");
                foreach (var effect in effects)
                {
                    effect.ExecuteEffect();
                }
            }
            else
            {
                Debug.LogWarning("Attempted to play card but no card is selected");
            }
        }

        public void PlayAttack()
        {
            if (selectedCard != null)
            {
                onPlayCard.Invoke(CardType.Attack);
                selectedCard.ExecuteEffectsByType(ConvertCardTypeToEffectType(CardType.Attack));
            }
            else
            {
                Debug.LogWarning("Attempted to play card but no card is selected");
            }
        }

        public void PlayMonster()
        {
            if (selectedCard != null)
            {
                onPlayCard.Invoke(CardType.Monster);
                selectedCard.ExecuteEffectsByType(ConvertCardTypeToEffectType(CardType.Monster));
            }
            else
            {
                Debug.LogWarning("Attempted to play card but no card is selected");
            }
        }
    }
}