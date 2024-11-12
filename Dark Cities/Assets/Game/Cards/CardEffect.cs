using UnityEngine;

[CreateAssetMenu(fileName = "New Card Effect", menuName = "Cards/Effects/Card Effect")]
public class CardEffect : BaseEffect 
{
    [Header("Card Effect Specific")]
    public bool isHidden = false;
    public bool targetRequired = false;
}