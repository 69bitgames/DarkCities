using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDetailPopup : MonoBehaviour
{
    [Header("Card Information")]
    [SerializeField] private TextMeshProUGUI cardNameText;        // Village card name
    [SerializeField] private TextMeshProUGUI cardMonsterNameText; // Monster card name
    [SerializeField] private Image cardArtwork;

    [Header("Description Panel")]
    [SerializeField] private TextMeshProUGUI descriptionTitle;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image descriptionPanel;

    [Header("Effect Buttons")]
    [SerializeField] private Button villageEffectButton;
    [SerializeField] private Button attackEffectButton;
    [SerializeField] private Button monsterEffectButton;

    private MainDeckCard currentCard;
    private Effect[] villageEffects;
    private Effect monsterEffect;

    private static CardDetailPopup instance;
    public static CardDetailPopup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardDetailPopup>();
                if (instance == null)
                {
                    Debug.LogError("No CardDetailPopup found in scene!");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        Debug.Log("CardDetailPopup Awake");
        SetupEffectButtons();
        gameObject.SetActive(false);
    }

    private void SetupEffectButtons()
    {
        // Setup hover listeners for each effect button
        SetupEffectButton(villageEffectButton, () => ShowEffectDetails(0));
        SetupEffectButton(attackEffectButton, () => ShowEffectDetails(1));
        SetupEffectButton(monsterEffectButton, () => ShowEffectDetails(2));
    }

    private void SetupEffectButton(Button button, System.Action onHover)
    {
        // Add hover listeners to button
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() 
            ?? button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        enterEntry.callback.AddListener((data) => onHover());
        trigger.triggers.Add(enterEntry);
    }

    public void ShowCard(MainDeckCard card)
    {
        Debug.Log($"Showing detail popup for card: {card.Name}");
        currentCard = card;
        villageEffects = card.VillageCard?.GetEffects();
        monsterEffect = card.MonsterCard?.GetEffects()[0];

        // Set initial information
        cardNameText.text = card.VillageCard?.Name ?? "Unknown";
        cardMonsterNameText.text = card.MonsterCard?.Name ?? "Unknown";
        cardArtwork.sprite = card.VillageCard?.Image; // Default to village card art

        // Show first effect by default
        ShowEffectDetails(0);

        gameObject.SetActive(true);
    }

    private void ShowEffectDetails(int effectIndex)
    {
        Debug.Log($"Showing effect details for index: {effectIndex}");

        switch (effectIndex)
        {
            case 0: // Village Effect 1
                if (villageEffects != null && villageEffects.Length > 0)
                {
                    UpdateDetailPanel(
                        villageEffects[0].EffectName,
                        villageEffects[0].Description,
                        currentCard.VillageCard.Image
                    );
                }
                break;

            case 1: // Village Effect 2
                if (villageEffects != null && villageEffects.Length > 1)
                {
                    UpdateDetailPanel(
                        villageEffects[1].EffectName,
                        villageEffects[1].Description,
                        currentCard.VillageCard.Image
                    );
                }
                break;

            case 2: // Monster Effect
                if (monsterEffect != null)
                {
                    UpdateDetailPanel(
                        monsterEffect.EffectName,
                        monsterEffect.Description,
                        currentCard.MonsterCard.Image
                    );
                }
                break;
        }
    }

    private void UpdateDetailPanel(string title, string description, Sprite artwork)
    {
        descriptionTitle.text = title;
        descriptionText.text = description;
        cardArtwork.sprite = artwork;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}