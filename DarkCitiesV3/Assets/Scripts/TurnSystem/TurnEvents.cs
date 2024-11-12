using UnityEngine;
using System;

public static class TurnEvents
{
    // Turn progression events
    public static event Action<int> OnTurnStart;
    public static event Action<int> OnTurnEnd;
    public static event Action<PlayerType> OnPlayerTurnStart;
    public static event Action<PlayerType> OnPlayerTurnEnd;
    public static event Action<TurnPhase> OnPhaseChanged;
    public static event Action<string> OnTurnError;

    public static void TriggerTurnStart(int turnNumber)
    {
        Debug.Log($"Turn {turnNumber} started");
        OnTurnStart?.Invoke(turnNumber);
    }

    public static void TriggerTurnEnd(int turnNumber)
    {
        Debug.Log($"Turn {turnNumber} ended");
        OnTurnEnd?.Invoke(turnNumber);
    }

    public static void TriggerPlayerTurnStart(PlayerType player)
    {
        Debug.Log($"{player}'s turn started");
        OnPlayerTurnStart?.Invoke(player);
    }

    public static void TriggerPlayerTurnEnd(PlayerType player)
    {
        Debug.Log($"{player}'s turn ended");
        OnPlayerTurnEnd?.Invoke(player);
    }

    public static void TriggerPhaseChanged(TurnPhase newPhase)
    {
        Debug.Log($"Phase changed to {newPhase}");
        OnPhaseChanged?.Invoke(newPhase);
    }

    public static void TriggerTurnError(string error)
    {
        Debug.LogError($"Turn Error: {error}");
        OnTurnError?.Invoke(error);
    }
}