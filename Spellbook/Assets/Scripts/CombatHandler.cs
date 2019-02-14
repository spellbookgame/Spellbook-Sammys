using UnityEngine;
using UnityEngine.UI;

// script to manage UI in CombatScene
// slider from https://www.youtube.com/watch?v=GfuxWs6UAJQ
public class CombatHandler : MonoBehaviour
{
    // serializefield private variables
    [SerializeField] private GameObject Panel_help;
    [SerializeField] private Text Text_mana;

    // for the player
    [SerializeField] private Slider Slider_healthbar;
    [SerializeField] private Text Text_healthtext;

    // for the enemy
    [SerializeField] private Slider Slider_enemyHealthBar;
    [SerializeField] private Text Text_enemyHealthText;

    // private variables
    private bool bHelpOpen = false;

    Player localPlayer;
    Enemy enemy;

    private void Start()
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

        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();
    }

    private void Update()
    {
        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();

        // collect spell pieces
        if (Input.GetKeyDown(KeyCode.Alpha1) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Alchemy A Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha2) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Alchemy B Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha3) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Alchemy C Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha4) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Alchemy D Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha5) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Arcane A Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha6) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Arcane B Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha7) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Arcane C Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha8) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Arcane D Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha9) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Elemental A Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Alpha0) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Elemental B Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Q) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Elemental C Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.W) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Elemental D Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.E) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Illusion A Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.R) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Illusion B Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.T) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Illusion C Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.Y) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Illusion D Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.U) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Summoning A Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.I) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Summoning B Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.O) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Summoning C Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.P) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Summoning D Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.A) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Time A Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.S) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Time B Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.D) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Time C Spell Piece", localPlayer.Spellcaster);
        if (Input.GetKeyDown(KeyCode.F) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectSpellPiece("Time D Spell Piece", localPlayer.Spellcaster);

        UpdateEnemyStats();
    }

// ------------------------------------------------------ COMBAT/HEALTH -----------------------------------------------------
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
        if(localPlayer.Spellcaster.fCurrentHealth > 0)
        {
            // Deduct damage dealt from player's health if they have more than 0 health
            localPlayer.Spellcaster.fCurrentHealth -= damageValue;
        }
        
        // adjust slider
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


// ---------------------------------------------- BUTTONS ----------------------------------------------------
    // when help button is clicked
    public void helpClick()
    {
        if (bHelpOpen == false)
        {
            Panel_help.SetActive(true);
            bHelpOpen = true;
        }
        else if (bHelpOpen == true)
        {
            Panel_help.SetActive(false);
            bHelpOpen = false;
        }
    }

    // player's basic attack
    public void attackClick()
    {
        localPlayer.Spellcaster.fBasicAttackStrength = Random.Range(1, 6);
        if(GameObject.FindGameObjectWithTag("Enemy") != null)
            enemy.HitEnemy(localPlayer.Spellcaster.fBasicAttackStrength);
    }
}
