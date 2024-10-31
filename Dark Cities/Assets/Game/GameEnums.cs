using UnityEngine;

namespace GameEnums
{
    [System.Flags]
    public enum AvailableMonsters
    {
        All = ~0,
        Werewolf = 1 << 0,
        Vampire = 1 << 1
    }

    public enum ConstructionType
    {
        Armory,
        Laboratory,
        Church,
        Brothel,
        Cemetery
    }

    public enum EffectTiming
    {
        Immediate,
        EndOfTurn,
        StartOfTurn,
        Triggered
    }

    public enum EffectTrigger
    {
        None,
        OnVillagerDeath,
        OnMonsterAscension,
        OnMoonPhaseChange,
        OnConstruction
    }
}