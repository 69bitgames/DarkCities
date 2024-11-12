using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Effect", menuName = "Cards/Effects/Attack Effect")]
public class AttackEffect : CardEffect
{
    [Header("Attack Specific")]
    public bool canTargetHidden;
    public int damageAmount;
}