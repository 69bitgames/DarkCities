using UnityEngine;
using System.Collections.Generic;
using GameEnums;

public abstract class BaseEffect : ScriptableObject, IEffect
{
    [Header("Effect Information")]
    [SerializeField] protected string effectName;  // Changed to protected
    [TextArea(2,4)]
    [SerializeField] protected string effectDescription;  // Changed to protected
    public string EffectDescription => effectDescription;  // Public property to access it

    [Header("Effect Timing")]
    [SerializeField] protected EffectTiming timing;  // Changed to protected
    [SerializeField] protected EffectTrigger trigger;  // Changed to protected
    
    [Header("Effect Requirements")]
    [SerializeField] protected int villagerCost;
    [SerializeField] protected bool requiresMonsterAscended;
    [Space(10)]
    [SerializeField] protected bool requiresConstruction = false;
    [Tooltip("Only used if Requires Construction is checked")]
    [SerializeField] protected ConstructionType constructionType;
    
    [SerializeField] 
    protected List<ScriptableObject> componentsList = new List<ScriptableObject>();
    
    protected List<IEffect> activeComponents = new List<IEffect>();

    public virtual string EffectName => effectName;
    public virtual EffectTiming Timing => timing;
    public virtual EffectTrigger Trigger => trigger;
    
    public virtual void Initialize()
    {
        activeComponents.Clear();
        foreach (var component in componentsList)
        {
            if (component is IEffect effectComponent)
            {
                effectComponent.Initialize();
                activeComponents.Add(effectComponent);
            }
        }
    }
    
    public virtual bool CanExecute(GameState state)
    {
        if (state == null) return false;
        if (villagerCost > 0 && state.CurrentVillagers < villagerCost) return false;
        if (requiresMonsterAscended && !state.IsMonsterAscended) return false;
        if (requiresConstruction && !state.HasConstruction(constructionType)) return false;
        
        return activeComponents.TrueForAll(comp => comp.CanExecute(state));
    }
    
    public virtual void Execute(GameState state)
    {
        if (!CanExecute(state)) return;
        
        foreach (var component in activeComponents)
        {
            component.Execute(state);
        }
    }
}