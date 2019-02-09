using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// code from https://www.youtube.com/watch?v=GfuxWs6UAJQ
public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider Slider_healthbar;
    [SerializeField] private Text Text_healthtext;

    // for the enemy
    [SerializeField] private Slider Slider_enemyHealthBar;
    [SerializeField] private Text Text_enemyHealthText;

    Player localPlayer;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // setting player's current health to equal max health
        localPlayer.Spellcaster.fCurrentHealth = localPlayer.Spellcaster.fMaxHealth;

        // setting the slider and text value to max health
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = localPlayer.Spellcaster.fCurrentHealth.ToString();

        Slider_enemyHealthBar.value = CalculateEnemyHealth();
        Text_enemyHealthText.text = enemy.fCurrentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // deal damage to player if health is above 0
        if(Input.GetKeyDown(KeyCode.X) && localPlayer.Spellcaster.fCurrentHealth > 0)
            HitPlayer(6);

        if (Input.GetKeyDown(KeyCode.C) && enemy.fCurrentHealth > 0)
            enemy.HitEnemy(6);

        UpdateEnemyStats();
    }

    // calculating health to decrease from slider
    private float CalculatePlayerHealth()
    {
        return localPlayer.Spellcaster.fCurrentHealth / localPlayer.Spellcaster.fMaxHealth;
    }
    private float CalculateEnemyHealth()
    {
        return enemy.fCurrentHealth / enemy.fMaxHealth;
    }

    // deduct health and change slider values
    public void HitPlayer(float damageValue)
    {
        // Deduct damage dealt from player's health
        localPlayer.Spellcaster.fCurrentHealth -= damageValue;
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = localPlayer.Spellcaster.fCurrentHealth.ToString();

        // if health goes below 0, set to 0
        if (localPlayer.Spellcaster.fCurrentHealth <= 0)
        {
            localPlayer.Spellcaster.fCurrentHealth = 0;
            Text_healthtext.text = "0";
        } 
    }

    public void UpdateEnemyStats()
    {
        Slider_enemyHealthBar.value = CalculateEnemyHealth();
        Text_enemyHealthText.text = enemy.fCurrentHealth.ToString();
    }
}
