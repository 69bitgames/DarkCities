using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("ResourceManager not found in scene!");
            }
            return instance;
        }
    }

    [SerializeField] private VillagerPool villagerPool;

    private void Awake()
    {
        Debug.Log("ResourceManager Awake");
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        villagerPool = new VillagerPool();
    }

    public void ProcessBreeding()
    {
        Debug.Log("Processing breeding phase");
        int currentVillagers = villagerPool.GetVillagersByStatus(VillagerStatus.Normal);
        int newVillagers = Mathf.FloorToInt(currentVillagers * 0.5f);
        
        if (villagerPool.CanAddVillagers(newVillagers))
        {
            villagerPool.AddVillagers(newVillagers);
            ResourceEvents.TriggerVillagersBred(newVillagers);
        }
        else
        {
            ResourceEvents.TriggerResourceError("Cannot breed villagers: Village at capacity");
        }
    }

    // Villager Management Methods
    public bool AddVillagers(int count, VillagerStatus status = VillagerStatus.Normal)
    {
        return villagerPool.AddVillagers(count, status);
    }

    public bool RemoveVillagers(int count, VillagerStatus status = VillagerStatus.Normal)
    {
        return villagerPool.RemoveVillagers(count, status);
    }

    public bool ChangeVillagerStatus(int count, VillagerStatus fromStatus, VillagerStatus toStatus)
    {
        return villagerPool.ChangeVillagerStatus(count, fromStatus, toStatus);
    }

    public void IncreaseVillageCapacity(int amount)
    {
        villagerPool.IncreaseCapacity(amount);
    }

    public int GetVillagersByStatus(VillagerStatus status)
    {
        return villagerPool.GetVillagersByStatus(status);
    }

    public int GetTotalVillagers()
    {
        return villagerPool.TotalVillagers;
    }

    public int GetVillageCapacity()
    {
        return villagerPool.TotalCapacity;
    }
}