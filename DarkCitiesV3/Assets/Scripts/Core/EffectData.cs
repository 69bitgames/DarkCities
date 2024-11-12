using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectData
{
    public string effectName;
    public EffectType effectType;
    public string description;
    public System.DateTime timestamp;
    public bool wasSuccessful;
    public string targetInfo; // For history tracking

    public EffectData(string name, EffectType type, string desc, bool success, string target = "")
    {
        effectName = name;
        effectType = type;
        description = desc;
        timestamp = System.DateTime.Now;
        wasSuccessful = success;
        targetInfo = target;
    }
}
