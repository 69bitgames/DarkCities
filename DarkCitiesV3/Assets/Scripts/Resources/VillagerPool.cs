using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class VillagerPool
{
    private Dictionary<VillagerStatus, int> villagersByStatus;
    private int baseCapacity = 10;
    private int additionalCapacity = 0;

    public int TotalCapacity => baseCapacity + additionalCapacity;
    public int TotalVillagers => GetTotalVillagers();

    public VillagerPool()
    {
        Debug.Log("Initializing VillagerPool");
        villagersByStatus = new Dictionary<VillagerStatus, int>();
        foreach (VillagerStatus status in System.Enum.GetValues(typeof(VillagerStatus)))
        {
            villagersByStatus[status] = 0;
        }
        // Start with some normal villagers
        villagersByStatus[VillagerStatus.Normal] = 5;
    }

    public bool CanAddVillagers(int count)
    {
        return TotalVillagers + count <= TotalCapacity;
    }

    public bool AddVillagers(int count, VillagerStatus status = VillagerStatus.Normal)
    {
        Debug.Log($"Attempting to add {count} villagers with status {status}");
        
        if (!CanAddVillagers(count))
        {
            ResourceEvents.TriggerResourceError($"Cannot add {count} villagers: Exceeds capacity");
            return false;
        }

        villagersByStatus[status] += count;
        ResourceEvents.TriggerVillagerCountChanged(TotalVillagers);
        ResourceEvents.TriggerVillagerStatusChanged(status, villagersByStatus[status]);
        return true;
    }

    public bool RemoveVillagers(int count, VillagerStatus status = VillagerStatus.Normal)
    {
        Debug.Log($"Attempting to remove {count} villagers with status {status}");
        
        if (villagersByStatus[status] < count)
        {
            ResourceEvents.TriggerResourceError($"Cannot remove {count} villagers: Insufficient villagers of status {status}");
            return false;
        }

        villagersByStatus[status] -= count;
        ResourceEvents.TriggerVillagerCountChanged(TotalVillagers);
        ResourceEvents.TriggerVillagerStatusChanged(status, villagersByStatus[status]);
        return true;
    }

    public bool ChangeVillagerStatus(int count, VillagerStatus fromStatus, VillagerStatus toStatus)
    {
        Debug.Log($"Attempting to change {count} villagers from {fromStatus} to {toStatus}");
        
        if (villagersByStatus[fromStatus] < count)
        {
            ResourceEvents.TriggerResourceError($"Cannot change status of {count} villagers: Insufficient villagers of status {fromStatus}");
            return false;
        }

        if (!CanAddVillagers(count))
        {
            ResourceEvents.TriggerResourceError($"Cannot change status of {count} villagers: Would exceed capacity");
            return false;
        }

        villagersByStatus[fromStatus] -= count;
        villagersByStatus[toStatus] += count;
        
        ResourceEvents.TriggerVillagerStatusChanged(fromStatus, villagersByStatus[fromStatus]);
        ResourceEvents.TriggerVillagerStatusChanged(toStatus, villagersByStatus[toStatus]);
        return true;
    }

    public void IncreaseCapacity(int amount)
    {
        Debug.Log($"Increasing capacity by {amount}");
        additionalCapacity += amount;
        ResourceEvents.TriggerVillagerCapacityChanged(TotalCapacity);
    }

    public int GetVillagersByStatus(VillagerStatus status)
    {
        return villagersByStatus[status];
    }

    private int GetTotalVillagers()
    {
        int total = 0;
        foreach (var count in villagersByStatus.Values)
        {
            total += count;
        }
        return total;
    }
}