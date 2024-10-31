using UnityEngine;

public interface IEffectComponent
{
    void Initialize();
    bool CanExecute(GameState state);
    void Execute(GameState state);
}