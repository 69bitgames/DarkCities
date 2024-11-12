using UnityEngine;

[RequireComponent(typeof(UICard))]
public class UICardTester : MonoBehaviour
{
    [Header("Test Data")]
    [SerializeField] private BaseCard testCard;
    
    [Header("Debug Info")]
    [SerializeField] private bool showDebugInfo;
    [SerializeField] private string currentCardName;
    [SerializeField] private CardType currentCardType;
    
    private UICard uiCard;

    private void Awake()
    {
        uiCard = GetComponent<UICard>();
        UpdateDebugInfo();
    }

    public void UpdateDebugInfo()
    {
        if (testCard != null)
        {
            currentCardName = testCard.Name;
            currentCardType = testCard.Type;
        }
        else
        {
            currentCardName = "No Card Assigned";
            currentCardType = CardType.None;
        }
    }

    public void PreviewCard()
    {
        if (uiCard != null && testCard != null)
        {
            uiCard.SetupCard(testCard);
            Debug.Log($"Previewing card: {testCard.Name}");
        }
        else if (uiCard == null)
        {
            Debug.LogError("No UICard component found!");
        }
        else
        {
            Debug.LogError("No test card assigned!");
        }
    }

    public BaseCard TestCard => testCard;
}