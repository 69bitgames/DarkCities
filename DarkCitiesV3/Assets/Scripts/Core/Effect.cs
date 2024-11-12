using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Effect", menuName = "Card Game/Effect")]
public class Effect : ScriptableObject
{
    [SerializeField] private string effectName;
    [SerializeField] private EffectType effectType;
    [SerializeField] private string description;
    [SerializeField] private List<Cost> costs = new List<Cost>();
    
    [SerializeField] private UnityEvent onEffectActivated;
    [SerializeField] private UnityEvent onEffectFailed;

    public string EffectName => effectName;
    public EffectType EffectType => effectType;
    public string Description => description;

    // Returns whether the effect can be used
    public bool CanUseEffect()
    {
        Debug.Log($"Checking if effect '{effectName}' can be used");
        
        if (costs.Count == 0)
        {
            Debug.Log($"Effect '{effectName}' has no costs - can be used");
            return true;
        }

        foreach (var cost in costs)
        {
            if (!cost.CanPayCost())
            {
                Debug.LogWarning($"Effect '{effectName}' cannot be used - cost {cost.CostType} cannot be paid");
                return false;
            }
        }

        Debug.Log($"Effect '{effectName}' can be used - all costs can be paid");
        return true;
    }

    public EffectData UseEffect()
    {
        Debug.Log($"Attempting to use effect: {effectName}");

        if (!CanUseEffect())
        {
            Debug.LogWarning($"Effect '{effectName}' failed - cannot pay costs");
            onEffectFailed?.Invoke();
            return new EffectData(effectName, effectType, description, false);
        }

        try
        {
            // Pay all costs
            foreach (var cost in costs)
            {
                cost.PayCost();
            }

            // Activate the effect
            Debug.Log($"Activating effect: {effectName}");
            onEffectActivated?.Invoke();

            // Return successful effect data for history
            return new EffectData(effectName, effectType, description, true);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error executing effect '{effectName}': {e.Message}");
            onEffectFailed?.Invoke();
            return new EffectData(effectName, effectType, description, false);
        }
    }
}