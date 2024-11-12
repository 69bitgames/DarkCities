using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cost
{
    [SerializeField] private CostType costType;
    [SerializeField] private int amount;
    [SerializeField] private string customCostDescription; // For custom costs

    public CostType CostType => costType;
    public int Amount => amount;

    public bool CanPayCost()
    {
        Debug.Log($"Checking if cost can be paid: {costType} - Amount: {amount}");
        // This will be implemented based on your resource management system
        switch (costType)
        {
            case CostType.None:
                return true;
            case CostType.Villagers:
                // Example: Check if player has enough villagers
                return true; // Implement actual check
            // Add other cost type checks
            default:
                Debug.LogWarning($"Cost type {costType} not implemented in CanPayCost check");
                return false;
        }
    }

    public void PayCost()
    {
        Debug.Log($"Paying cost: {costType} - Amount: {amount}");
        // Implement cost payment logic
    }
}

