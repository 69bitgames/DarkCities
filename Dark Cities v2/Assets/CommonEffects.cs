using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkCities.Effects;
using TMPro;
public class CommonEffects : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugEffect(){
        Debug.Log("Debug Effect");
    }

    public void IncreaseVillagerCount(int amount){
        GameObject.Find("VillagerCounter").GetComponent<TextMeshProUGUI>().text = "Villager Count: " + amount.ToString();
    }
}
