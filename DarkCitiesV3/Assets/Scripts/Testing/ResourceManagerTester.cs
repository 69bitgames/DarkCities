using UnityEngine;

// Add buttons to the inspector
public class ResourceManagerTester : MonoBehaviour 
{
    [SerializeField] private bool showDebugButtons;

    // Add public buttons that will show up in the inspector
    public void TestAddVillagers()
    {
        Debug.Log("Testing: Adding 3 villagers");
        ResourceManager.Instance.AddVillagers(3);
    }

    public void TestBreeding()
    {
        Debug.Log("Testing: Processing breeding phase");
        ResourceManager.Instance.ProcessBreeding();
    }

    public void TestStatusChange()
    {
        Debug.Log("Testing: Changing 2 villagers to Busy status");
        ResourceManager.Instance.ChangeVillagerStatus(2, VillagerStatus.Normal, VillagerStatus.Busy);
    }

    public void TestCapacityIncrease()
    {
        Debug.Log("Testing: Increasing capacity by 5");
        ResourceManager.Instance.IncreaseVillageCapacity(5);
    }

    public void PrintCurrentState()
    {
        Debug.Log("Current Game State:");
        Debug.Log($"Total villager count: {ResourceManager.Instance.GetTotalVillagers()}");
        Debug.Log($"Village capacity: {ResourceManager.Instance.GetVillageCapacity()}");
        foreach (VillagerStatus status in System.Enum.GetValues(typeof(VillagerStatus)))
        {
            Debug.Log($"{status} villagers: {ResourceManager.Instance.GetVillagersByStatus(status)}");
        }
    }

    private void Start()
    {
        ResourceEvents.OnVillagerCountChanged += HandleVillagerCountChanged;
        ResourceEvents.OnVillagerCapacityChanged += HandleVillagerCapacityChanged;
        ResourceEvents.OnVillagerStatusChanged += HandleVillagerStatusChanged;
        ResourceEvents.OnVillagersBred += HandleVillagersBred;
        ResourceEvents.OnResourceError += HandleResourceError;
    }

    private void OnDestroy()
    {
        ResourceEvents.OnVillagerCountChanged -= HandleVillagerCountChanged;
        ResourceEvents.OnVillagerCapacityChanged -= HandleVillagerCapacityChanged;
        ResourceEvents.OnVillagerStatusChanged -= HandleVillagerStatusChanged;
        ResourceEvents.OnVillagersBred -= HandleVillagersBred;
        ResourceEvents.OnResourceError -= HandleResourceError;
    }

    // Event Handlers
    private void HandleVillagerCountChanged(int newCount)
    {
        Debug.Log($"Event: Villager count changed to {newCount}");
    }

    private void HandleVillagerCapacityChanged(int newCapacity)
    {
        Debug.Log($"Event: Village capacity changed to {newCapacity}");
    }

    private void HandleVillagerStatusChanged(VillagerStatus status, int count)
    {
        Debug.Log($"Event: {status} villager count changed to {count}");
    }

    private void HandleVillagersBred(int newVillagers)
    {
        Debug.Log($"Event: {newVillagers} new villagers bred");
    }

    private void HandleResourceError(string error)
    {
        Debug.LogError($"Event: Resource error - {error}");
    }
}