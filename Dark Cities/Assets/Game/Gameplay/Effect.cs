using UnityEngine;
using System.Collections.Generic;
using GameEnums;

[CreateAssetMenu(fileName = "New Effect", menuName = "Cards/Effects/Base Effect")]
public abstract class BaseEffect : ScriptableObject
{
    [Header("Effect Information")]
    public string effectName;
    [TextArea(2,4)]
    public string effectDescription;
    
    [Header("Effect Timing")]
    public EffectTiming timing;
    public EffectTrigger trigger;
    
    [Header("Effect Requirements")]
    public int villagerCost;
    public bool requiresMonsterAscended;
    [Space(10)]
    public bool requiresConstruction = false;
    [Tooltip("Only used if Requires Construction is checked")]
    public ConstructionType constructionType;
    
    [SerializeField] 
    protected List<ScriptableObject> _componentsList;
    
    private List<IEffectComponent> _activeComponents;
    
    public virtual void Initialize()
    {
        _activeComponents = new List<IEffectComponent>();
        foreach (var component in _componentsList)
        {
            if (component is IEffectComponent effectComponent)
            {
                effectComponent.Initialize();
                _activeComponents.Add(effectComponent);
            }
        }
    }
    
    public virtual bool CanExecute(GameState state)
    {
        if (villagerCost > 0 && state.CurrentVillagers < villagerCost) return false;
        if (requiresMonsterAscended && !state.IsMonsterAscended) return false;
        if (requiresConstruction && !state.HasConstruction(constructionType)) return false;
        
        return _activeComponents.TrueForAll(comp => comp.CanExecute(state));
    }
    
    public virtual void Execute(GameState state)
    {
        if (!CanExecute(state)) return;
        
        foreach (var component in _activeComponents)
        {
            component.Execute(state);
        }
    }
}