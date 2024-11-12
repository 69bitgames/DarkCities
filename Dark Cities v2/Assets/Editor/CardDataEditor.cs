using UnityEngine;
using DarkCities.Effects;
using System;

namespace DarkCities.Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Dark Cities/Card")]
    public class CardData : ScriptableObject
    {
        [Header("Card Information")]
        [SerializeField] private string cardName;
        [SerializeField] private string cardTitle;
        [SerializeField][TextArea(2, 4)] private string description;
        [SerializeField][TextArea(2, 3)] private string flavorText;
        [SerializeField] private CardType cardType;
        
        [Header("Resources")]
        [SerializeField] private Sprite artwork;
        
        [Header("Effects")]
        [SerializeField] private Effect[] effects = new Effect[0];

        // Get required effect count based on card type
        private int RequiredEffectCount => cardType switch
        {
            CardType.Village => 2,
            CardType.Monster => 1,
            CardType.Construction => 0,
            _ => throw new ArgumentException($"Unhandled card type: {cardType}")
        };

        public Card CreateCard()
        {
            // Validate effect count matches card type requirement
            if (effects.Length != RequiredEffectCount)
            {
                Debug.LogError($"Card {name} of type {cardType} must have exactly {RequiredEffectCount} effects");
                return null;
            }

            // Validate effects are assigned (if any are required)
            if (RequiredEffectCount > 0)
            {
                for (int i = 0; i < effects.Length; i++)
                {
                    if (effects[i] == null)
                    {
                        Debug.LogError($"Card {name} is missing effect at slot {i}");
                        return null;
                    }
                }
            }

            // Create and return new Card instance
            try
            {
                return new Card(
                    cardName,
                    cardTitle,
                    description,
                    cardType,
                    effects,
                    artwork,
                    flavorText
                );
            }
            catch (ArgumentException e)
            {
                Debug.LogError($"Failed to create card {name}: {e.Message}");
                return null;
            }
        }

#if UNITY_EDITOR
        // Editor validation - made protected virtual so it can be called from editor
        protected virtual void OnValidate()
        {
            // Resize effects array based on card type
            int requiredCount = RequiredEffectCount;
            if (effects.Length != requiredCount)
            {
                Array.Resize(ref effects, requiredCount);
                Debug.Log($"Adjusted effect slots to {requiredCount} for {cardType} card type");
            }
        }

        // Ensure proper array size when the asset is loaded in editor
        protected virtual void OnEnable()
        {
            OnValidate();
        }
#endif
    }
}