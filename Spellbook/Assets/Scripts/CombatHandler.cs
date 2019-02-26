using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// script to manage UI in CombatScene
// slider from https://www.youtube.com/watch?v=GfuxWs6UAJQ
public class CombatHandler : MonoBehaviour
{
    // serializefield private variables
    [SerializeField] private GameObject Panel_help;
    [SerializeField] private Text Text_mana;

    // buttons
    [SerializeField] private Button attackButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button spellbookButton;

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
    public static CombatHandler instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        setUpCombatHandler();    
    }

    private void Update()
    {
        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();

        // collect spell pieces
        if (Input.GetKeyDown(KeyCode.Alpha1) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Alchemy A Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha2) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Alchemy B Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha3) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Alchemy C Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha4) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Alchemy D Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha5) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Arcane A Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha6) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Arcane B Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha7) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Arcane C Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha8) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Arcane D Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha9) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Elemental A Glyph");
        if (Input.GetKeyDown(KeyCode.Alpha0) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Elemental B Glyph");
        if (Input.GetKeyDown(KeyCode.Q) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Elemental C Glyph");
        if (Input.GetKeyDown(KeyCode.W) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Elemental D Glyph");
        if (Input.GetKeyDown(KeyCode.E) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Illusion A Glyph");
        if (Input.GetKeyDown(KeyCode.R) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Illusion B Glyph");
        if (Input.GetKeyDown(KeyCode.T) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Illusion C Glyph");
        if (Input.GetKeyDown(KeyCode.Y) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Illusion D Glyph");
        if (Input.GetKeyDown(KeyCode.U) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Summoning A Glyph");
        if (Input.GetKeyDown(KeyCode.I) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Summoning B Glyph");
        if (Input.GetKeyDown(KeyCode.O) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Summoning C Glyph");
        if (Input.GetKeyDown(KeyCode.P) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Summoning D Glyph");
        if (Input.GetKeyDown(KeyCode.A) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Time A Glyph");
        if (Input.GetKeyDown(KeyCode.S) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Time B Glyph");
        if (Input.GetKeyDown(KeyCode.D) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Time C Glyph");
        if (Input.GetKeyDown(KeyCode.F) && enemy.fCurrentHealth > 0)
            localPlayer.Spellcaster.CollectGlyph("Time D Glyph");

        UpdatePlayerStats();
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

    // change slider values
    public void UpdatePlayerStats()
    {
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = localPlayer.Spellcaster.fCurrentHealth.ToString();
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
        if (localPlayer.Spellcaster.hasAttacked == false)
        {
            localPlayer.Spellcaster.fBasicAttackStrength = Random.Range(1, 6);
            if (GameObject.FindGameObjectWithTag("Enemy") != null)
                enemy.HitEnemy(localPlayer.Spellcaster.fBasicAttackStrength);
            localPlayer.Spellcaster.hasAttacked = true;
        }
        else
        {
            PanelHolder.instance.displayNotify("You already attacked this turn.");
        }
    }

    public void setUpCombatHandler()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        if (localPlayer == null)
        {
            Debug.Log("local player is null");
        }

        // check to see if enemy exists
        if(GameObject.FindGameObjectWithTag("Enemy"))
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // setting player's current health to equal max health
        localPlayer.Spellcaster.fCurrentHealth = localPlayer.Spellcaster.fMaxHealth;

        // setting the slider and text value to max health
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = localPlayer.Spellcaster.fCurrentHealth.ToString();

        Slider_enemyHealthBar.value = CalculateEnemyHealth();
        Text_enemyHealthText.text = enemy.fCurrentHealth.ToString();

        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();

        // adding onclick listeners to buttons
        attackButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            attackClick();
        });
        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        spellbookButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });
    }
}
