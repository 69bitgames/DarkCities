using UnityEngine;

public class TurnManagerTester : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to turn events
        TurnEvents.OnTurnStart += HandleTurnStart;
        TurnEvents.OnTurnEnd += HandleTurnEnd;
        TurnEvents.OnPlayerTurnStart += HandlePlayerTurnStart;
        TurnEvents.OnPlayerTurnEnd += HandlePlayerTurnEnd;
        TurnEvents.OnPhaseChanged += HandlePhaseChanged;
        TurnEvents.OnTurnError += HandleTurnError;
    }

    private void OnDestroy()
    {
        // Unsubscribe from turn events
        TurnEvents.OnTurnStart -= HandleTurnStart;
        TurnEvents.OnTurnEnd -= HandleTurnEnd;
        TurnEvents.OnPlayerTurnStart -= HandlePlayerTurnStart;
        TurnEvents.OnPlayerTurnEnd -= HandlePlayerTurnEnd;
        TurnEvents.OnPhaseChanged -= HandlePhaseChanged;
        TurnEvents.OnTurnError -= HandleTurnError;
    }

    // Event Handlers
    private void HandleTurnStart(int turnNumber)
    {
        Debug.Log($"Test: Turn {turnNumber} started");
    }

    private void HandleTurnEnd(int turnNumber)
    {
        Debug.Log($"Test: Turn {turnNumber} ended");
    }

    private void HandlePlayerTurnStart(PlayerType player)
    {
        Debug.Log($"Test: {player}'s turn started");
    }

    private void HandlePlayerTurnEnd(PlayerType player)
    {
        Debug.Log($"Test: {player}'s turn ended");
    }

    private void HandlePhaseChanged(TurnPhase newPhase)
    {
        Debug.Log($"Test: Phase changed to {newPhase}");
    }

    private void HandleTurnError(string error)
    {
        Debug.LogError($"Test: Turn error - {error}");
    }
}