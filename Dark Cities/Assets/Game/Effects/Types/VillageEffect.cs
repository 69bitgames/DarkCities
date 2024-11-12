using UnityEngine;

[CreateAssetMenu(fileName = "New Village Effect", menuName = "Cards/Effects/Village Effect")]
public class VillageEffect : CardEffect
{
    [Header("Village Specific")]
    public bool affectsAllVillagers;
    public int villagerCount;
}