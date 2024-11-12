using UnityEngine;
using UnityEngine.Events;
using System;

namespace DarkCities.Effects
{
    public enum EffectType
    {
        Village,
        Attack,
        Monster
    }

    // Custom UnityEvent that we can serialize in the inspector
    [Serializable]
    public class EffectAction : UnityEvent { }

    [CreateAssetMenu(fileName = "New Effect", menuName = "Dark Cities/Effect")]
    public class Effect : ScriptableObject
    {
        [Header("Effect Information")]
        [SerializeField] private string effectName;
        [SerializeField][TextArea(2,4)] private string description;
        [SerializeField] private EffectType effectType;

        [Header("Actions")]
        [SerializeField] private EffectAction[] actions;

        public string EffectName => effectName;
        public string Description => description;
        public EffectType Type => effectType;

        public void ExecuteEffect()
        {
            Debug.Log($"Executing effect: {effectName} of type: {effectType}");
    
    if (actions == null || actions.Length == 0)
    {
        Debug.LogWarning($"Effect {name} has no actions assigned");
        return;
    }

    foreach (var action in actions)
    {
        if (action == null)
        {
            Debug.LogWarning($"Null action found in effect {name}");
            continue;
        }
        action.Invoke();
    }
}
    }
}