using UnityEngine;
using System.Collections.Generic;
using GameEnums;
public class GameState : MonoBehaviour
{
    // Player state
    public int CurrentVillagers { get; private set; } = 4; // Start with 4 villagers
    public bool IsMonsterAscended { get; private set; }
    public AvailableMonsters PlayerMonsterType { get; private set; }
    
    // Game state
    public int CurrentTurn { get; private set; } = 1;
    public int MoonPhase { get; private set; } = 1;  // 1-7, full moon at 7
    
    // Constructions
    private Dictionary<ConstructionType, bool> constructions = new Dictionary<ConstructionType, bool>();
    
    // Events for effects to hook into
    public delegate void GameStateChanged();
    public event GameStateChanged OnVillagerCountChanged;
    public event GameStateChanged OnMonsterAscended;
    public event GameStateChanged OnMoonPhaseChanged;
    public event GameStateChanged OnTurnChanged;
    
    private void Start()
    {
        InitializeConstructions();
    }
    
    private void InitializeConstructions()
    {
        foreach (ConstructionType type in System.Enum.GetValues(typeof(ConstructionType)))
        {
            constructions[type] = false;
        }
    }
    
    // State modification methods
    public void AddVillagers(int amount)
    {
        CurrentVillagers += amount;
        OnVillagerCountChanged?.Invoke();
    }
    
    public bool SpendVillagers(int amount)
    {
        if (CurrentVillagers >= amount)
        {
            CurrentVillagers -= amount;
            OnVillagerCountChanged?.Invoke();
            return true;
        }
        return false;
    }
    
    public void SetMonsterAscended(bool ascended, AvailableMonsters monsterType)
    {
        IsMonsterAscended = ascended;
        PlayerMonsterType = monsterType;
        OnMonsterAscended?.Invoke();
    }
    
    public void AdvanceMoonPhase()
    {
        MoonPhase = (MoonPhase % 7) + 1;
        OnMoonPhaseChanged?.Invoke();
    }
    
    public void AdvanceTurn()
    {
        CurrentTurn++;
        OnTurnChanged?.Invoke();
        
        // Auto-breed villagers (1 for every 2)
        AddVillagers(CurrentVillagers / 2);
    }
    
    public void AddConstruction(ConstructionType type)
    {
        constructions[type] = true;
    }
    
    public bool HasConstruction(ConstructionType type)
    {
        return constructions.TryGetValue(type, out bool exists) && exists;
    }
}

public enum ConstructionType
{
    Armory,
    Laboratory,
    Church,
    Brothel,
    Cemetery
}