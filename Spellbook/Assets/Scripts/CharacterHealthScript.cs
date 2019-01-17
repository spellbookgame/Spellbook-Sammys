using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// code from https://www.youtube.com/watch?v=GfuxWs6UAJQ
public class CharacterHealthScript : MonoBehaviour
{
    private float fCurrentHealth { get; set; }
    private float fMaxHealth { get; set; }

    [SerializeField]
    private Slider Slider_healthbar;

    [SerializeField]
    private Text Text_healthtext;

    // Start is called before the first frame update
    void Start()
    {
        fMaxHealth = 20f;

        // on start, health bar will be full
        fCurrentHealth = fMaxHealth;

        // setting the slider and text value to max health
        Slider_healthbar.value = CalculateHealth();
        Text_healthtext.text = fCurrentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // to test damage being dealt
        if(Input.GetKeyDown(KeyCode.X))
            DealDamage(6);
    }

    private float CalculateHealth()
    {
        return fCurrentHealth / fMaxHealth;
    }

    public void DealDamage(float damageValue)
    {
        // Deduct damage dealt from player's health
        fCurrentHealth -= damageValue;
        Slider_healthbar.value = CalculateHealth();
        Text_healthtext.text = fCurrentHealth.ToString();

        // if health goes below 0, set to 0
        if (fCurrentHealth <= 0)
        {
            fCurrentHealth = 0;
            Text_healthtext.text = "0";
        } 
    }
}
