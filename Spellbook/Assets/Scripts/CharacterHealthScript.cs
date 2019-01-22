using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// code from https://www.youtube.com/watch?v=GfuxWs6UAJQ
public class CharacterHealthScript : MonoBehaviour
{
    public float fCurrentHealth { get; set; }
    private float fMaxHealth { get; set; }

    public Enemy enemy;

    [SerializeField] private Slider Slider_healthbar;
    [SerializeField] private Text Text_healthtext;

    // for the enemy
    [SerializeField] private Slider Slider_enemyHealthBar;
    [SerializeField] private Text Text_enemyHealthText;

    CollectItemScript collectItemScript;

    // Start is called before the first frame update
    void Start()
    {
        // setting player's health to 20
        fMaxHealth = 20f;

        // on start, health bar will be full
        fCurrentHealth = fMaxHealth;

        // setting the slider and text value to max health
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = fCurrentHealth.ToString();

        // instantiating enemy with 20 health
        enemy = new Enemy(20f);

        enemy.fCurrentHealth = enemy.fMaxHealth;
        Slider_enemyHealthBar.value = CalculateEnemyHealth();
        Text_enemyHealthText.text = enemy.fCurrentHealth.ToString();

        // referencing collect item script
        GameObject eventSystem = GameObject.Find("EventSystem");
        collectItemScript = eventSystem.GetComponent<CollectItemScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // deal damage if health is above 0
        if(Input.GetKeyDown(KeyCode.X) && fCurrentHealth > 0)
            HitPlayer(6);
        
        if (Input.GetKeyDown(KeyCode.C) && enemy.fCurrentHealth > 0)
            HitEnemy(6);
    }

    private float CalculatePlayerHealth()
    {
        return fCurrentHealth / fMaxHealth;
    }

    private float CalculateEnemyHealth()
    {
        return enemy.fCurrentHealth / enemy.fMaxHealth;
    }

    // to actually show that damage has been dealt
    public void HitPlayer(float damageValue)
    {
        // Deduct damage dealt from player's health
        fCurrentHealth -= damageValue;
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = fCurrentHealth.ToString();

        // if health goes below 0, set to 0
        if (fCurrentHealth <= 0)
        {
            fCurrentHealth = 0;
            Text_healthtext.text = "0";
        } 
    }

    public void HitEnemy(float damageValue)
    {
        // Deduct damage dealt from enemy's health
        enemy.fCurrentHealth -= damageValue;
        Slider_enemyHealthBar.value = CalculateEnemyHealth();
        Text_enemyHealthText.text = enemy.fCurrentHealth.ToString();

        // if health goes below 0, set to 0
        if (enemy.fCurrentHealth <= 0)
        {
            enemy.fCurrentHealth = 0;
            Text_enemyHealthText.text = "0";

            // notify player that spell piece was collected
            collectItemScript.CollectSpellPiece();
        }
    }
}
