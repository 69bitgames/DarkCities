using UnityEngine;
public interface ICard
{
    string Name { get; }
    Sprite Image { get; }
    CardType Type { get; }
    Effect[] GetEffects();
}