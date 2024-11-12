using UnityEngine;

public interface IEffect
{
    string EffectName { get; }
    string EffectDescription { get; }
    GameEnums.EffectTiming Timing { get; }
    GameEnums.EffectTrigger Trigger { get; }
    bool CanExecute(GameState state);
    void Execute(GameState state);
    void Initialize();
}