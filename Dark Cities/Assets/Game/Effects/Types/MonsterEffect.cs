using UnityEngine;
using GameEnums;  // Add this line

[CreateAssetMenu(fileName = "New Monster Effect", menuName = "Cards/Effects/Monster Effect")]
public class MonsterEffect : CardEffect
{
    [Header("Monster Specific")]
    public AvailableMonsters requiredMonsterType;
    public bool requiresTransformation;
}