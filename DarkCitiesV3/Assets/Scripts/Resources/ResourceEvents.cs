using UnityEngine;
using System;

public static class ResourceEvents
{
    // Villager events
    public static event Action<int> OnVillagerCountChanged;
    public static event Action<int> OnVillagerCapacityChanged;
    public static event Action<VillagerStatus, int> OnVillagerStatusChanged;
    public static event Action<int> OnVillagersBred;
    public static event Action<string> OnResourceError;

    public static void TriggerVillagerCountChanged(int newCount)
    {
        Debug.Log($"Villager count changed: {newCount}");
        OnVillagerCountChanged?.Invoke(newCount);
    }

    public static void TriggerVillagerCapacityChanged(int newCapacity)
    {
        Debug.Log($"Villager capacity changed: {newCapacity}");
        OnVillagerCapacityChanged?.Invoke(newCapacity);
    }

    public static void TriggerVillagerStatusChanged(VillagerStatus status, int count)
    {
        Debug.Log($"Villager status changed: {status} - Count: {count}");
        OnVillagerStatusChanged?.Invoke(status, count);
    }

    public static void TriggerVillagersBred(int newVillagers)
    {
        Debug.Log($"Villagers bred: {newVillagers}");
        OnVillagersBred?.Invoke(newVillagers);
    }

    public static void TriggerResourceError(string error)
    {
        Debug.LogError($"Resource Error: {error}");
        OnResourceError?.Invoke(error);
    }
}