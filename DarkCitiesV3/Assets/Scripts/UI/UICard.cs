// Scripts/UI/UICard.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Image))]
public class UICard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Card References")]
    [SerializeField] private Image cardArtwork;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private CanvasGroup nameContainer;
    
    [Header("Animation Settings")]
    [SerializeField] private float fadeSpeed = 5f;
    
    private BaseCard cardData;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        Debug.Log("UICard Awake");
        // Ensure the name starts hidden
        if (nameContainer != null)
        {
            nameContainer.alpha = 0f;
        }
    }

    public void SetupCard(BaseCard card)
    {
        Debug.Log($"Setting up UICard with {card.Name}");
        cardData = card;
        
        // Set the artwork
        if (cardArtwork != null && card.Image != null)
        {
            cardArtwork.sprite = card.Image;
        }
        
        // Set the name text
        if (cardNameText != null)
        {
            cardNameText.text = card.Name;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Hovering over card: {cardData.Name}");
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeNameContainer(1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Stopping hover over card: {cardData.Name}");
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeNameContainer(0f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardData is MainDeckCard mainDeckCard)
        {            
            CardDetailPopup.Instance.ShowCard(mainDeckCard);
        }
    }

    private IEnumerator FadeNameContainer(float targetAlpha)
    {
        if (nameContainer == null) yield break;

        while (!Mathf.Approximately(nameContainer.alpha, targetAlpha))
        {
            nameContainer.alpha = Mathf.MoveTowards(nameContainer.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void ShowDetailPopup()
    {
        // Placeholder for detail popup functionality
        Debug.Log($"Show detail popup for card: {cardData.Name}");
        Debug.Log($"Card Type: {cardData.Type}");
        Debug.Log($"Effects Count: {cardData.GetEffects().Length}");
    }
}