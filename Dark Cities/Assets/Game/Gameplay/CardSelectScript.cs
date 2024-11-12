using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CardSelectScript : MonoBehaviour
{
    [SerializeField] private Image spriteImage;
    [SerializeField] private TextMeshProUGUI effect1Text;
    [SerializeField] private TextMeshProUGUI effect2Text;
    [SerializeField] private TextMeshProUGUI effect3Text;

    [SerializeField] private Button effect1btn;
    [SerializeField] private Button effect2btn;
    [SerializeField] private Button effect3btn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Card card){
        Debug.Log(card.cardData);
        Debug.Log(card.cardData.villageEffect);

        spriteImage.sprite = card.cardData.cardArtwork;
        effect1Text.text = card.cardData.villageEffect?.EffectDescription ?? "No effect";
        effect2Text.text = card.cardData.attackEffect?.EffectDescription ?? "No effect";
        effect3Text.text = card.cardData.monsterEffect?.EffectDescription ?? "No effect";
        effect1btn.onClick.AddListener(() => {
            card.cardData.villageEffect.Execute(GameObject.Find("GameState").GetComponent<GameState>());
        });
        effect2btn.onClick.AddListener(() => {
            card.cardData.attackEffect.Execute(GameObject.Find("GameState").GetComponent<GameState>());
        });
        effect3btn.onClick.AddListener(() => {
            card.cardData.monsterEffect.Execute(GameObject.Find("GameState").GetComponent<GameState>());
        });
    }
}
