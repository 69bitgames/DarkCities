using UnityEngine;
using System;
using DarkCities.Effects;
using System.Linq;

namespace DarkCities.Cards
{
        public enum CardType
    {
        Village,
        Construction,
        Monster,
        Attack
    }
    public class Card
    {
        private readonly string cardName;
        private readonly string cardTitle;
        private readonly string description;
        private readonly string flavorText;
        private readonly Sprite artwork;
        private readonly Effect[] effects;
        private readonly CardType cardType;

        public string CardName => cardName;
        public string CardTitle => cardTitle;
        public string Description => description;
        public string FlavorText => flavorText;
        public Sprite Artwork => artwork;
        public Effect[] Effects => effects;
        public CardType CardType => cardType;

        public Card(string cardName, string cardTitle, string description, CardType cardType,
            Effect[] effects, Sprite artwork, string flavorText = null)
        {
            if (string.IsNullOrEmpty(cardName))
                throw new ArgumentException("Card name cannot be null or empty");
            
            if (string.IsNullOrEmpty(cardTitle))
                throw new ArgumentException("Card title cannot be null or empty");
            
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Card description cannot be null or empty");
            
            // Validate effect count based on card type
            int requiredEffects = cardType switch
            {
                CardType.Village => 2,
                CardType.Monster => 1,
                CardType.Construction => 0,
                _ => throw new ArgumentException($"Unhandled card type: {cardType}")
            };

            if (effects == null && requiredEffects > 0)
                throw new ArgumentException("Effects array cannot be null when effects are required");

            if (effects?.Length != requiredEffects)
                throw new ArgumentException($"Card of type {cardType} must have exactly {requiredEffects} effects");

            this.cardName = cardName;
            this.cardTitle = cardTitle;
            this.description = description;
            this.flavorText = flavorText;
            this.artwork = artwork;
            this.effects = effects ?? new Effect[0];
            this.cardType = cardType;
        }

        // Helper method to get effects by type
        public Effect[] GetEffectsByType(EffectType type)
        {
            if (effects == null) return new Effect[0];
    
            // Add logging to debug effect filtering
            Debug.Log($"Filtering effects for type: {type}");
            foreach (var effect in effects)
            {
                Debug.Log($"Effect: {effect.EffectName}, Type: {effect.Type}");
            }
            
            return effects.Where(e => e.Type == type).ToArray();
        }

        

        // Execute all effects
        public void ExecuteAllEffects()
        {
            if (effects == null || effects.Length == 0) return;
            
            foreach (var effect in effects)
            {
                effect?.ExecuteEffect();
            }
        }

        // Execute effects of a specific type
        public void ExecuteEffectsByType(EffectType type)
        {
            var typeEffects = GetEffectsByType(type);
            foreach (var effect in typeEffects)
            {
                effect?.ExecuteEffect();
            }
        }
    }
}