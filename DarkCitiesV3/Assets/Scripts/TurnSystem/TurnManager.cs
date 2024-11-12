using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    public static TurnManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("TurnManager not found in scene!");
            }
            return instance;
        }
    }

    [SerializeField] private PlayerType currentPlayer;
    [SerializeField] private TurnPhase currentPhase;
    private int currentTurn = 1;
    private bool isGameInProgress = false;

    public PlayerType CurrentPlayer => currentPlayer;
    public TurnPhase CurrentPhase => currentPhase;
    public int CurrentTurn => currentTurn;
    public bool IsGameInProgress => isGameInProgress;

    private void Awake()
    {
        Debug.Log("TurnManager Awake");
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        Debug.Log("Starting new game");
        isGameInProgress = true;
        currentTurn = 1;
        currentPlayer = PlayerType.Player1;
        StartTurn();
    }

    public void StartTurn()
    {
        Debug.Log($"Starting turn {currentTurn}");
        TurnEvents.TriggerTurnStart(currentTurn);
        TurnEvents.TriggerPlayerTurnStart(currentPlayer);
        SetPhase(TurnPhase.Start);
    }

    public void EndTurn()
    {
        Debug.Log($"Ending turn {currentTurn}");
        if (currentPhase != TurnPhase.End)
        {
            TurnEvents.TriggerTurnError("Cannot end turn: Not in End Phase");
            return;
        }

        TurnEvents.TriggerPlayerTurnEnd(currentPlayer);
        TurnEvents.TriggerTurnEnd(currentTurn);

        // Switch players
        currentPlayer = currentPlayer == PlayerType.Player1 ? PlayerType.Player2 : PlayerType.Player1;
        
        if (currentPlayer == PlayerType.Player1)
        {
            currentTurn++;
        }

        StartTurn();
    }

    public void NextPhase()
    {
        Debug.Log("Attempting to move to next phase");
        switch (currentPhase)
        {
            case TurnPhase.Start:
                SetPhase(TurnPhase.Breeding);
                ProcessBreeding();
                break;
            case TurnPhase.Breeding:
                SetPhase(TurnPhase.Draw);
                ProcessDraw();
                break;
            case TurnPhase.Draw:
                SetPhase(TurnPhase.Main);
                break;
            case TurnPhase.Main:
                SetPhase(TurnPhase.End);
                break;
            case TurnPhase.End:
                EndTurn();
                break;
        }
    }

    private void SetPhase(TurnPhase newPhase)
    {
        Debug.Log($"Setting phase to {newPhase}");
        currentPhase = newPhase;
        TurnEvents.TriggerPhaseChanged(newPhase);
    }

    private void ProcessBreeding()
    {
        Debug.Log("Processing breeding phase");
        ResourceManager.Instance.ProcessBreeding();
    }

    private void ProcessDraw()
    {
        Debug.Log("Processing draw phase");
        // This will be implemented when we create the Deck Management System
        // For now we'll just log it
        Debug.Log("Draw phase - To be implemented with Deck System");
    }

    public bool CanTakeAction()
    {
        return isGameInProgress && currentPhase == TurnPhase.Main;
    }
}