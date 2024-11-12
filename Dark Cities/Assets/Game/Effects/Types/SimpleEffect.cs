using UnityEngine;

[CreateAssetMenu(fileName = "New Simple Effect", menuName = "Cards/Effects/Simple Effect")]
public class SimpleEffect : CardEffect
{
    [Header("Simple Effect Settings")]
    public bool isRepeatable = false;
}