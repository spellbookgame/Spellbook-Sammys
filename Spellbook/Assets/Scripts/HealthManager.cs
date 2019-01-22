using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// code from https://www.youtube.com/watch?v=GfuxWs6UAJQ
public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider Slider_healthbar;
    [SerializeField] private Text Text_healthtext;
    public Player player;

    // for the enemy
    [SerializeField] private Slider Slider_enemyHealthBar;
    [SerializeField] private Text Text_enemyHealthText;
    private Enemy enemy;

    private CollectItemScript collectItemScript;

    // Start is called before the first frame update
    void Start()
    {
        // on start, health bar will be full
        player = new Player(20f);
        player.fCurrentHealth = player.fMaxHealth;
        // setting the slider and text value to max health
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = player.fCurrentHealth.ToString();

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
        // deal damage to player if health is above 0
        if(Input.GetKeyDown(KeyCode.X) && player.fCurrentHealth > 0)
            HitPlayer(6);
    }

    // calculating health to decrease from slider
    private float CalculatePlayerHealth()
    {
        return player.fCurrentHealth / player.fMaxHealth;
    }
    private float CalculateEnemyHealth()
    {
        return enemy.fCurrentHealth / enemy.fMaxHealth;
    }

    // deduct health and change slider values
    public void HitPlayer(float damageValue)
    {
        // Deduct damage dealt from player's health
        player.fCurrentHealth -= damageValue;
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = player.fCurrentHealth.ToString();

        // if health goes below 0, set to 0
        if (player.fCurrentHealth <= 0)
        {
            player.fCurrentHealth = 0;
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

    // if attack button is clicked, deal 6 damage to enemy
    public void attackClick()
    {
        if (enemy.fCurrentHealth > 0)
            HitEnemy(6);
    }
}
